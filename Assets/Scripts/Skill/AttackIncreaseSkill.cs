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
        StateHandler stateHandler = user.GetComponent<StateHandler>();
        if (stateHandler != null)
        {
            stateHandler.StartCoroutine(ApplyAttackIncrease(stateHandler));
        }
    }

    private IEnumerator ApplyAttackIncrease(StateHandler stateHandler)
    {
        float effectiveMultiplier = baseAttackMultiplier + (level - 1) * attackMultiplierPerLevel;

        // ���� ���ݷ� ���� �� ������ ���ݷ� ����
        int originalAttack = stateHandler.Attack;
        stateHandler.Attack = Mathf.RoundToInt(originalAttack * effectiveMultiplier);

        // ȿ�� ���� �ð� ���
        yield return new WaitForSeconds(duration);

        // ȿ�� ���� �� ���ݷ� ����
        stateHandler.Attack = originalAttack;
    }
}
