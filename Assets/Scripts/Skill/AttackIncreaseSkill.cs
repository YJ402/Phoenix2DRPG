using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIncreaseSkill : ActiveSkill
{
    public float baseAttackMultiplier = 2f;
    public new string skillName = "공격력 2배증가";

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
        int originalAttack = statHandler.AttackPower;
        statHandler.AttackPower = Mathf.RoundToInt(originalAttack * baseAttackMultiplier);

        // 효과 지속 시간 대기
        yield return new WaitForSeconds(duration);

        // 효과 종료 후 공격력 복원
        statHandler.AttackPower = originalAttack;
    }
}
