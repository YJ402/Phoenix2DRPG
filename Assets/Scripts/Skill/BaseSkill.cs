using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public string skillName;    // ��ų �̸�
    public float coolDown = 60;      // ��ٿ� �ð�
    public float duration = 10;      // ��ų ȿ�� ���� �ð�
    public int level = 1;       // ��ų ���� (�⺻ 1)

    // ��ų �ߵ� �� ȣ��Ǵ� �Լ� (�÷��̾� ������Ʈ�� ���ڷ� ����)
    public abstract void Activate(GameObject user);

    // ��ų ���׷��̵� (���� ���)
    public virtual void UpgradeSkill()
    {
        level++;
    }
}
