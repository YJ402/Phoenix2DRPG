using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusAttackSpeedSkill : PassiveSkill
{
    public float attckSpeedIncrease = 0.5f;
    private bool bonusApplied = false;
    private StatHandler statHandler;

    private void Start()
    {
        skillName = "발사속도 증가(패시브)";
    }
    public override void ApplySkill()
    {
        statHandler = GetComponent<StatHandler>();
        if (statHandler != null && !bonusApplied)  // !bonusApplied 로 변경하여 한 번만 적용
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
