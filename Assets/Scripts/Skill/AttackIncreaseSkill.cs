using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AttackIncreaseSkill : ActiveSkill
{
    public float baseAttackMultiplier = 2f;
    public float attackMultiplierPerLevel = 0.5f;

    public override void Activate(GameObject user)
    {
        StatHandler statHandler = user.GetComponent<StatHandler>();
        if (statHandler != null)
        {
            statHandler.StartCoroutine(ApplyAttackIncrease(statHandler));
        }
    }

    private IEnumerator ApplyAttackIncrease(StatHandler statHandler)
    {
        float effectiveMultiplier = baseAttackMultiplier + (level - 1) * attackMultiplierPerLevel;

        // ���� ���ݷ� ���� �� ������ ���ݷ� ����
        int originalAttack = statHandler.AttackPower;
        statHandler.AttackPower = Mathf.RoundToInt(originalAttack * effectiveMultiplier);

        // ȿ�� ���� �ð� ���
        yield return new WaitForSeconds(duration);

        // ȿ�� ���� �� ���ݷ� ����
        statHandler.AttackPower = originalAttack;
    }
}
