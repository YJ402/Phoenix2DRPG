using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlusAttackSkill : PassiveSkill
{
    public new string skillName = "���ݷ�����(�нú�)";

    public int attackIncrease = 1;

    private bool bonusApplied = false;
    private StatHandler statHandler;

    private void Awake()
    {
        skillName = "PlusAttack";
    }
    public override void ApplySkill()
    {
        statHandler = GetComponent<StatHandler>();
        if (statHandler != null && !bonusApplied)
        {
            statHandler.AttackPower += attackIncrease * level;
            bonusApplied = true;
        }
    }
    public override void UpgradeSkill()
    {
        base.UpgradeSkill();
        if (statHandler != null)
        {
            statHandler.AttackPower += attackIncrease;
        }
    }
}
