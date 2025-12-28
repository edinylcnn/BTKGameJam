using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandIK : MonoBehaviour
{
    public Animator animator;
    public Transform rightHandTarget;
    [Range(0f, 1f)] public float ikWeight = 1f;

    void OnAnimatorIK(int layerIndex)
    {
        if (animator == null || rightHandTarget == null) return;

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
    }
}
