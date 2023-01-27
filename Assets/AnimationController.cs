using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetMovementBlendTreeValue(float value)
    {
        animator.SetFloat("Speed", value);
    }
}
