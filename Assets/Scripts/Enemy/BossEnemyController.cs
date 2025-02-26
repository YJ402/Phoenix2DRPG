using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyController
{
    
    public List<Action> bossEvent = new();

    protected override void Start()
    {
        base.Start();
        bossEvent.Add(() => { });
        bossEvent.Add(() => { });
        bossEvent.Add(() => { });
        bossEvent[0] += Attack1;
        bossEvent[1] += Attack2;
        bossEvent[2] += Attack3;
    }

    public void TriggerBossEvent(int i)
    {
        bossEvent[i]?.Invoke();
    }

    protected override void Attack(bool isAttack)
    {
        base.Attack(isAttack); // Attack 애니메이션 트리거 // 추가 로직 작성 안할거면 지워도 무방.
    }

    private void Attack1()
    {
        //몹 소환
    }

    private void Attack2()
    {
        //범위 마법 공격
    }

    private void Attack3()
    {
        //순간 이동
    }
}
