using System;
using UnityEngine;

public class RanageEnemyController : EnemyController
{
    protected StatHandler rangeStatHandler; // StatHandler? RangeStatHandler? << �ϴ� ���ϼ� �ְ� StatHandler�� ����.

    private void Awake()
    {
        base.Awake();
        rangeStatHandler = GetComponent<RangeStatHandler>();
    }

    //protected void Attack()
    //{
    //    base.Attack();

    //}
}