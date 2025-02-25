using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusSpeedSkill : PassiveSkill
{
    public new string name = "PlusSpeedSkill";
    public float speedIncrease = 1f;
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
            statHandler.Speed += speedIncrease;
            bonusApplied &= true;
        }
    }
    private void OnDestroy()
    {
        if (statHandler != null && bonusApplied)
        {
            statHandler.Speed -= speedIncrease;
            bonusApplied = false ;
        }
    }
    public override void UpgradeSkill()
    {
        base.UpgradeSkill();
        if (statHandler != null)
        {
            statHandler.Speed += speedIncrease;
        }
    }
}
