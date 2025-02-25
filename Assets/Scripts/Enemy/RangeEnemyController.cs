using System;
using UnityEngine;

public class RanageEnemyController : EnemyController
{
    protected StatHandler rangeStatHandler; // StatHandler? RangeStatHandler? << 일단 통일성 있게 StatHandler로 설정.

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