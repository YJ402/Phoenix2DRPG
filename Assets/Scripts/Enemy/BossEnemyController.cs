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
        base.Attack(isAttack); // Attack �ִϸ��̼� Ʈ���� // �߰� ���� �ۼ� ���ҰŸ� ������ ����.
    }

    private void Attack1()
    {
        //�� ��ȯ
    }

    private void Attack2()
    {
        //���� ���� ����
    }

    private void Attack3()
    {
        //���� �̵�
    }
}
