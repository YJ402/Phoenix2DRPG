using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;
    private static int IsMoving = Animator.StringToHash("IsMoving");
    private static int IsAttack = Animator.StringToHash("IsAttack");
    private static int IsDamaged = Animator.StringToHash("IsDamaged");
    private static int IsDead = Animator.StringToHash("IsDead");
    private static int MovingSpeed = Animator.StringToHash("MovingSpeed");
    private static int AttackSpeed = Animator.StringToHash("AttackSpeed");

    private List<int> animParamList;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>(true);

        animParamList = new List<int>();
        foreach (AnimatorControllerParameter para in animator.parameters)
        {
            animParamList.Add(para.nameHash);
        }
        //Init();
    }

    //public void Init()
    //{
    //    for(int i = 0; i < animParamHashSet.Count; i++)
    //    {
    //        if (animParamHashSet.Contains(IsMoving))
    //        {

    //        } 

    //    }

    //IsMoving = Animator.StringToHash("IsMoving");
    //    IsAttack = Animator.StringToHash("IsAttack");
    //    IsDamaged = Animator.StringToHash("IsDamaged");
    //    IsDead = Animator.StringToHash("IsDead");
    //    MovingSpeed = Animator.StringToHash("MovingSpeed");
    //    AttackSpeed = Animator.StringToHash("AttackSpeed");
    //}

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void ChangeMovingSpeed(float speed)
    {
        if (!animParamList.Contains(MovingSpeed)) // 이 파라미터를 몬스터에도 적용해도 될까요?
            return;
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

    public void Attack(bool isattack) // attack 변수
    {
        animator.SetBool(IsAttack, isattack);
    }


    public void ChangeAttackSpeed(float speed)
    {
        if (!animParamList.Contains(MovingSpeed))
            return;
        animator.SetFloat(AttackSpeed, speed);
    }

    public void Die()
    {
        animator.SetTrigger(IsDead);
    }
}
