using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private List<BaseSkill> activeSkills;

    private void Awake()
    {
        activeSkills = new List<BaseSkill>(GetComponents<BaseSkill>());
    }

    public void ActivateSkill(string skillName, GameObject user)
    {
        BaseSkill skill = activeSkills.Find(s => s.skillName == skillName);
        if (skill != null)
        {
            skill.Activate(user);
        }
    }
    public List<BaseSkill> GetActiveSkills()
    {
        return activeSkills;
    }
}
