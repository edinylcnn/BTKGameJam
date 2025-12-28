using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(AnimatorHandler))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] float IkSensivity = 1;
    [SerializeField] float GunSensivity = 1;
    [SerializeField] private float minPitch = -40f;
    [SerializeField] private float maxPitch = 70f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform cameraTransform;
    [Header("Shooting")] [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform fireOrigin;
    [SerializeField] private float bulletSpeed = 25f;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private float maxShootDistance = 1000f;

    private CharacterController characterController;
    private InputHandler inputHandler;
    private AnimatorHandler animatorHandler;
    private ArmIKController armIKController;
    private float verticalVelocity;
    private float cameraPitch;
    private float IkPitch = 1.79f;
    private float gunPitch = 0f;
    private float nextFireTime;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponent<AnimatorHandler>();
        armIKController = GetComponent<ArmIKController>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        if (cameraTransform != null)
        {
            cameraPitch = NormalizeAngle(cameraTransform.localEulerAngles.x);
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        inputHandler.TickInput();

        HandleLook();
        Vector3 moveDirection = GetMoveDirection();
        MoveCharacter(moveDirection, delta);
        //UpdateAnimation(moveDirection, delta);
        HandleShooting();
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * inputHandler.MoveInput.y + right * inputHandler.MoveInput.x;
        if (move.sqrMagnitude > 1f)
        {
            move.Normalize();
        }

        return move;
    }

    private void MoveCharacter(Vector3 moveDirection, float deltaTime)
    {
        Vector3 horizontalVelocity = moveDirection * moveSpeed;

        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * deltaTime;
        Vector3 velocity = horizontalVelocity + Vector3.up * verticalVelocity;

        characterController.Move(velocity * deltaTime);
    }

    private void HandleLook()
    {
        Vector2 lookInput = inputHandler.LookInput;

        if (Mathf.Abs(lookInput.x) > Mathf.Epsilon)
        {
            float yaw = lookInput.x * mouseSensitivity;
            transform.Rotate(Vector3.up, yaw, Space.World);
        }

        if (cameraTransform != null && Mathf.Abs(lookInput.y) > Mathf.Epsilon)
        {
            cameraPitch -= lookInput.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, minPitch, maxPitch);
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
            // Vector3 lookPos = armIKController.lookTarget.transform.localPosition;
            // IkPitch += lookInput.y * IkSensivity;
            // armIKController.lookTarget.transform.localPosition = new Vector3(lookPos.x, IkPitch, lookPos.z);
            // gunPitch += -lookInput.y * GunSensivity;
            // Vector3 handRot = armIKController.rightHandTarget.transform.localRotation.eulerAngles;
            // armIKController.rightHandTarget.localRotation = Quaternion.Euler(gunPitch, handRot.y, handRot.z);
        }
    }

    private void UpdateAnimation(Vector3 moveDirection, float deltaTime)
    {
        Vector3 localMotion = transform.InverseTransformDirection(moveDirection);
        animatorHandler.UpdateMovement(localMotion, deltaTime);
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f)
        {
            angle -= 360f;
        }

        return angle;
    }

    private void HandleShooting()
    {
        if (BulletPrefab == null || cameraTransform == null)
        {
            return;
        }

        if (!Input.GetMouseButton(0) || Time.time < nextFireTime)
        {
            return;
        }

        nextFireTime = Time.time + 1f / Mathf.Max(fireRate, 0.01f);

        Vector3 origin = fireOrigin != null ? fireOrigin.position : cameraTransform.position;
        Vector3 direction = cameraTransform.forward;

        // Aim using the center of the camera; fall back to camera forward if nothing is hit.
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxShootDistance))
        {
            direction = (hitInfo.point - origin).normalized;
        }

        GameObject projectile = Instantiate(BulletPrefab, origin, Quaternion.LookRotation(direction));

        if (projectile.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            rigidbody.velocity = direction * bulletSpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}