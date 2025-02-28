using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusSpeedSkill : PassiveSkill
{
    public float speedIncrease = 1f;
    private bool bonusApplied = false;
    private StatHandler statHandler;

    private void Start()
    {
        skillName = "이동속도 증가(패시브)";
    }
    public override void ApplySkill()
    {
        statHandler = GetComponent<StatHandler>();
        if (statHandler != null && !bonusApplied)
        {
            statHandler.Speed += speedIncrease*level;
            bonusApplied &= true;
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
