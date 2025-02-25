using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsDamaged = Animator.StringToHash("IsDamaged");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int MovingSpeed = Animator.StringToHash("MovingSpeed");
    private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>(true);
    }

    
    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void ChangeMovingSpeed(float speed)
    {
            animator.SetFloat(MovingSpeed, speed);
    }

    public void Damage()
    {
        animator.SetBool(IsDamaged, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamaged, false);
    }

    public void Attack(bool isattack) // attack º¯¼ö
    {
        animator.SetBool(IsAttack, isattack);
    }

   
    public void ChangeAttackSpeed(float speed)
    {
        animator.SetFloat(AttackSpeed,speed);
    }

    public void Die()
    {
        animator.SetTrigger(IsDead);
    }
}
