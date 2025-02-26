using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusHealthSkill : PassiveSkill
{
    public int healthIncrease = 100;
    private bool bonusApplied = false;
    private StatHandler statHandler;

    private void Awake()
    {
        skillName = "PlusHealthSkill";
    }
    public override void Activate(GameObject user)
    {
        
    }
    public override void ApplySkill()
    {
        statHandler = GetComponent<StatHandler>();
        if (statHandler != null && bonusApplied)
        {
            statHandler.MaxHealth += healthIncrease *level;
            bonusApplied = true;
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
