using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArmIKController : MonoBehaviour
{
    [Header("Targets")]
    public Transform rightHandTarget;
    public Transform leftHandTarget;
    public Transform rightElbowHint;
    public Transform leftElbowHint;

    [Header("Weights")]
    [Range(0f, 1f)] public float rightHandWeight = 1f;
    [Range(0f, 1f)] public float leftHandWeight = 1f;
    [Range(0f, 1f)] public float rightElbowWeight = 0.5f;
    [Range(0f, 1f)] public float leftElbowWeight = 0.5f;

    [Header("Look At / Upper Body")]
    public Transform lookTarget;
    [Range(0f, 1f)] public float lookWeight = 1f;
    [Range(0f, 1f)] public float bodyLookWeight = 0.35f;
    [Range(0f, 1f)] public float headLookWeight = 1f;
    [Range(0f, 1f)] public float eyesLookWeight = 0f;
    [Range(0f, 1f)] public float clampLookWeight = 0.5f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        ApplyHandIK(AvatarIKGoal.RightHand, rightHandTarget, rightHandWeight);
        ApplyHandIK(AvatarIKGoal.LeftHand, leftHandTarget, leftHandWeight);

        ApplyHintIK(AvatarIKHint.RightElbow, rightElbowHint, rightElbowWeight);
        ApplyHintIK(AvatarIKHint.LeftElbow, leftElbowHint, leftElbowWeight);

        //ApplyLookAt();
    }

    private void ApplyHandIK(AvatarIKGoal goal, Transform target, float weight)
    {
        animator.SetIKPositionWeight(goal, weight);
        animator.SetIKRotationWeight(goal, weight);

        if (target != null && weight > 0f)
        {
            animator.SetIKPosition(goal, target.position);
            animator.SetIKRotation(goal, target.rotation);
        }
    }

    private void ApplyHintIK(AvatarIKHint hint, Transform target, float weight)
    {
        animator.SetIKHintPositionWeight(hint, weight);

        if (target != null && weight > 0f)
        {
            animator.SetIKHintPosition(hint, target.position);
        }
    }

    private void ApplyLookAt()
    {
        animator.SetLookAtWeight(lookWeight, bodyLookWeight, headLookWeight, eyesLookWeight, clampLookWeight);

        if (lookTarget != null && lookWeight > 0f)
        {
            animator.SetLookAtPosition(lookTarget.position);
        }
    }
}
