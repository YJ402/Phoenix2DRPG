using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusHealthSkill : PassiveSkill
{
    public new string name = "PlusHealthSkill";
    public int healthIncrease = 100;
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
            statHandler.MaxHealth += healthIncrease;
            bonusApplied = true;
        }
    }
    private void OnDestroy()
    {
        if (statHandler != null && bonusApplied)
        {
            statHandler.MaxHealth -= healthIncrease;
            bonusApplied = false;
        }
    }
    public override void UpgradeSkill()
    {
        base.UpgradeSkill();
        if (statHandler != null)
        {
            statHandler.MaxHealth += healthIncrease;
        }

    }
}
