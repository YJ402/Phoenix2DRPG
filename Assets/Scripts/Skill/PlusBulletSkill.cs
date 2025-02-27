using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusBulletSkill : PassiveSkill
{
    public int bulletincrease = 1;
    public new string skillName = "źȯ 1�� �߰�(�нú�)";
    private bool bonusApplied = false;
    private RangeStatHandler rangeStatHandler;

    private void Awake()
    {
        skillName = "PlusBulletSkill";
    }
    public override void ApplySkill()
    {
        rangeStatHandler = GetComponent<RangeStatHandler>();
        if (rangeStatHandler != null && bonusApplied)
        {
            rangeStatHandler.BulletCount += bulletincrease * level;
            bonusApplied = true;
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
