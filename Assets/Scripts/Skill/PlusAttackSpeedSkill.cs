using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusAttackSpeedSkill : PassiveSkill
{
    public new string name = "PlusAttackSpeedSkill";
    public float attckSpeedIncrease = 0.5f;
    private bool bonusApplied = false;
    private StatHandler statHandler;

    public override void Activate(GameObject user)
    {
        
    }

    private void OnEnable()
    {
        statHandler = GetComponent<StatHandler>();
        if (statHandler != null && bonusApplied)
        {
            statHandler.AttackSpeed += attckSpeedIncrease;
            bonusApplied = true;
        }

    }
    private void OnDestroy()
    {
        if (statHandler != null && bonusApplied)
        {
            statHandler.AttackSpeed -= attckSpeedIncrease;
            bonusApplied = false;
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
