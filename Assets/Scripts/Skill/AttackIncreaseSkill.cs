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

        // 원래 공격력 저장 후 증가된 공격력 적용
        int originalAttack = statHandler.AttackPower;
        statHandler.AttackPower = Mathf.RoundToInt(originalAttack * effectiveMultiplier);

        // 효과 지속 시간 대기
        yield return new WaitForSeconds(duration);

        // 효과 종료 후 공격력 복원
        statHandler.AttackPower = originalAttack;
    }
}
