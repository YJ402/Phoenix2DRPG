using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    protected Animator animator;
    private static int IsMoving = Animator.StringToHash("IsMoving");
    private static int IsAttack = Animator.StringToHash("IsAttack");
    private static int IsDamaged = Animator.StringToHash("IsDamaged");
    private static int IsDead = Animator.StringToHash("IsDead");
    private static int MovingSpeed = Animator.StringToHash("MovingSpeed");
    private static int AttackSpeed = Animator.StringToHash("AttackSpeed");

    //private List<int> animParamList;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>(true);

        //animParamList = new List<int>();
        //foreach (AnimatorControllerParameter para in animator.parameters)
        //{
        //    animParamList.Add(para.nameHash);
        //}
        ////Init();
    }


    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void ChangeMovingSpeed(float speed)
    {
        animator.SetFloat(MovingSpeed, speed / 5);
    }

    public void Damage()
    {
        animator.SetBool(IsDamaged, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamaged, false);
    }

    public virtual void Attack(bool isattack) // attack º¯¼ö
    {
        animator.SetBool(IsAttack, isattack);
    }


    public void ChangeAttackSpeed(float speed)
    {
        animator.SetFloat(AttackSpeed, speed);
    }

    public void Die()
    {
        animator.SetTrigger(IsDead);
    }
}
