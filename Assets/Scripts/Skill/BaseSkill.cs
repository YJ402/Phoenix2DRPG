using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public string skillName;    // 스킬 이름  
    public int level = 1;       // 스킬 레벨 (기본 1)

    
    public abstract void Activate(GameObject user);

    public virtual void UpgradeSkill()
    {
        level++;
    }
}
public abstract class ActiveSkill : BaseSkill
{
    public float coolDown = 60f;
    public float duration = 10f;

}
public abstract class PassiveSkill : BaseSkill
{     

}

