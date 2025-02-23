using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsDamaged = Animator.StringToHash("IsDamaged");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void Damage()
    {
        animator.SetBool(IsDamaged, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamaged, false);
    }
    public void Attack()
    {
        animator.SetBool(IsAttack, true);
    }
}
