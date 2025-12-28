using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorHandler : MonoBehaviour
{
    [SerializeField] private string moveXParameter = "moveX";
    [SerializeField] private string moveYParameter = "moveY";
    [SerializeField] private float dampTime = 0.1f;

    private Animator animator;
    private int moveXHash;
    private int moveYHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        moveXHash = Animator.StringToHash(moveXParameter);
        moveYHash = Animator.StringToHash(moveYParameter);
    }

    public void UpdateMovement(Vector3 localMotion, float deltaTime)
    {
        Vector2 planar = new Vector2(localMotion.x, localMotion.z);
        planar = Vector2.ClampMagnitude(planar, 1f);

        animator.SetFloat(moveXHash, planar.x, dampTime, deltaTime);
        animator.SetFloat(moveYHash, planar.y, dampTime, deltaTime);
    }
}
