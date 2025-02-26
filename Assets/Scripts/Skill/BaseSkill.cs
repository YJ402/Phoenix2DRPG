using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public string skillName;    // ��ų �̸�  
    public int level = 1;       // ��ų ���� (�⺻ 1)

    
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

    public override void UpgradeSkill()
    {
        base.UpgradeSkill();
        duration += 3f;
    }

}
public abstract class PassiveSkill : BaseSkill
{
    public abstract void ApplySkill();
}

