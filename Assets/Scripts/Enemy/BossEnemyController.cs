using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyController
{
    protected override void Start()
    {
         // ������ ����
    }

    protected override void Attack(bool isAttack)
    {
        base.Attack(isAttack); // Attack �ִϸ��̼� Ʈ���� // �߰� ���� �ۼ� ���ҰŸ� ������ ����.
    }

    public void Attack1()
    {
        //�� ��ȯ
    }

    public void Attack2()
    {
        //��ü ����
    }

    public void Attack3()
    {
        //���� �̵�
    }


    //�����̵�

    //������ ��� ������?
    //�ִϸ��̼��� ��� ������? =>BaseController.Attack���� �ִϸ��̼Ǹ� �����Ű��. �ִϸ��̼��� �̺�Ʈ�� ������ ����.

    //���� �Ұ��ΰ�?

}
