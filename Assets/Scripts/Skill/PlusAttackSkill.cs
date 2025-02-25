using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlusAttackSkill : PassiveSkill
{
    public string skillName = "PlusAttack";

    public int attackIncrease = 1;

    private bool bonusApplied = false;
    private StatHandler statHandler;

    public override void Activate(GameObject user)
    {
        
    }
    private void OnEnable()
    {
        statHandler = GetComponent<StatHandler>();
        if (statHandler != null && !bonusApplied)
        {
            statHandler.AttackPower += attackIncrease;
            bonusApplied = true;
        }
    }
    private void OnDestroy()
    {
        if (statHandler != null && bonusApplied)
        {
            statHandler.AttackPower -= attackIncrease;
            bonusApplied = false;
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
