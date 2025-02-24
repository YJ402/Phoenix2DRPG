using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlusAttackSkill : PassiveSkill
{
    public string skillName = "PlusAttack";

    public int attackIncrease = 1;

    private bool bonusApplied = false;
    private StateHandler stateHandler;

    public override void Activate(GameObject user)
    {
        
    }
    private void OnEnable()
    {
        stateHandler = GetComponent<StateHandler>();
        if (stateHandler != null && !bonusApplied)
        {
            stateHandler.Attack += attackIncrease;
            bonusApplied = true;
        }
    }
    private void OnDestroy()
    {
        if (stateHandler != null && bonusApplied)
        {
            stateHandler.Attack -= attackIncrease;
            bonusApplied = false;
        }
    }
    public override void UpgradeSkill()
    {
        base.UpgradeSkill();
        if (stateHandler != null)
        {
            stateHandler.Attack += attackIncrease;
        }
    }
}
