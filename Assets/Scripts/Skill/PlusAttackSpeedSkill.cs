using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusAttackSpeedSkill : PassiveSkill
{
    public float attckSpeedIncrease = 0.5f;
    public new string skillName = "�߻�ӵ� ����(�нú�)";
    private bool bonusApplied = false;
    private StatHandler statHandler;

    private void Awake()
    {
        skillName = "PlusAttackSpeedSkill";
    }
    public override void ApplySkill()
    {
        statHandler = GetComponent<StatHandler>();
        if (statHandler != null && bonusApplied)
        {
            statHandler.AttackSpeed += attckSpeedIncrease * level;
            bonusApplied = true;
        }
    }

    public override void UpgradeSkill()
    {
        base.UpgradeSkill();
        if (statHandler != null)
        {
            statHandler.AttackSpeed += attckSpeedIncrease;
        }
    }

}
