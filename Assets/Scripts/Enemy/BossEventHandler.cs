using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEventHandler : MonoBehaviour
{
    BossEnemyController bossEnemyController;

    private void Awake()
    {
        bossEnemyController = GetComponentInParent<BossEnemyController>();
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
}
