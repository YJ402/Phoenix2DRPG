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
        bossEnemyController.TriggerBossEvent(0);
        Debug.Log("1실행");
    }

    private void AttackSpoke2()
    {
        bossEnemyController.TriggerBossEvent(1);
        Debug.Log("2실행");

    }

    private void AttackSpoke3()
    {
        bossEnemyController.TriggerBossEvent(2);
        Debug.Log("3실행");

    }

    private void ExitAttack()
    {
        baseController.isAttacking = false;
        animator.SetBool(BossAnimationHandler.attackSpoke_1, false);
        animator.SetBool(BossAnimationHandler.attackSpoke_2, false);
        animator.SetBool(BossAnimationHandler.attackSpoke_3, false);
    }
}
