using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AttackIncreaseSkill : BaseSkill
{
    public float baseAttackMultiplier = 2f;
    public float attackMultiplierPerLevel = 0.5f;

    public override void Activate(GameObject user)
    {
        PlayerStatus playerStatus = user.GetComponent<PlayerStatus>();
        if (playerStatus != null)
        {
            playerStatus.StartCoroutine(ApplyAttackIncrease(playerStatus));
        }
    }

    private IEnumerator ApplyAttackIncrease(PlayerStatus playerStatus)
    {
        float effectiveMultiplier = baseAttackMultiplier + (level - 1) * attackMultiplierPerLevel;

        // ���� ���ݷ� ���� �� ������ ���ݷ� ����
        float originalAttack = playerStatus.attack;
        playerStatus.attack *= effectiveMultiplier;

        // ȿ�� ���� �ð� ���
        yield return new WaitForSeconds(duration);

        // ȿ�� ���� �� ���ݷ� ����
        playerStatus.attack = originalAttack;
    }
}
