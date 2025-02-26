using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public ActiveSkill activeSkill;

    private float lastSkillTime = -Mathf.Infinity;

    void Update()
    {
        // Z Ű�� ������, ��ٿ��� �������� ��ų Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= lastSkillTime + ((ActiveSkill)activeSkill).coolDown)
        {
            activeSkill.Activate(gameObject);
            lastSkillTime = Time.time;
        }
    }
    public void ActivateActiveSkill()
    {
        if (activeSkill == null)
        {
            return;
        }
        if (Time.time >= lastSkillTime + activeSkill.coolDown)
        {
            activeSkill.Activate(gameObject);
            lastSkillTime = Time.time;
        }
        else
        {
            Debug.Log("��ų ��Ÿ���Դϴ�.");
        }
    }
    public void SetorUpgradeActiveSkill(ActiveSkill newSkill)
    {
        if (activeSkill != null && activeSkill.GetType() == newSkill.GetType())
        {
            activeSkill.UpgradeSkill();
        }
        else
        {
            if (activeSkill != null)
            {
                Destroy(activeSkill);
            }
            activeSkill = gameObject.AddComponent(newSkill.GetType()) as ActiveSkill;
            activeSkill.skillName = newSkill.skillName;
            activeSkill.coolDown = newSkill.coolDown;
            activeSkill.duration = newSkill.duration;
        }      
    }
}
