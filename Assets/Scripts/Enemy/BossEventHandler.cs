using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEventHandler : MonoBehaviour
{
    BossEnemyController bossEnemyController;
    BaseController baseController;
    Animator animator;

    private void Awake()
    {
        bossEnemyController = GetComponentInParent<BossEnemyController>();
        baseController = GetComponentInParent<BaseController>();
        animator = GetComponentInParent<Animator>();
    }

    private void AttackSpoke1()
    {
        bossEnemyController.Attack1();
    }

    private void AttackSpoke2()
    {
        bossEnemyController.Attack2();

    }

    private void AttackSpoke3()
    {
        bossEnemyController.Attack3();
    }

    private void ExitAttack()
    {
        baseController.isAttacking = false;
        animator.SetBool(BossAnimationHandler.attackSpoke_1, false);
        animator.SetBool(BossAnimationHandler.attackSpoke_2, false);
        animator.SetBool(BossAnimationHandler.attackSpoke_3, false);
    }
}
