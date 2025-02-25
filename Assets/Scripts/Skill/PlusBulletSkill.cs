using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusBulletSkill : PassiveSkill
{
    public new string name = "PlusBulletSkill";
    public int bulletincrease = 1;
    private bool bonusApplied = false;
    private RangeStatHandler rangeStatHandler;

    public override void Activate(GameObject user)
    {
        
    }
    private void OnEnable()
    {
        rangeStatHandler = GetComponent<RangeStatHandler>();
        if (rangeStatHandler != null && bonusApplied)
        {
            rangeStatHandler.BulletCount += bulletincrease;
            bonusApplied = true;
        }
    }
    private void OnDestroy()
    {
        if (rangeStatHandler != null && bonusApplied)
        {
            rangeStatHandler.BulletCount -= bulletincrease;
            bonusApplied = false;
        }
    }

    public override void UpgradeSkill()
    {
        base.UpgradeSkill();
        if (rangeStatHandler != null)
        {
            rangeStatHandler.BulletCount += bulletincrease;
        }
    }
}
