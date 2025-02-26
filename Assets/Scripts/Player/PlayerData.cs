using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private int clearStage=0;
    public int ClearStage
    {
        get {return clearStage;}
        set {clearStage = value;}
    }

    private int currentHP;
    public int CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }
    public void SaveCurrentHP(int currentHP)
    {
        CurrentHP = currentHP;
    }
    
    public bool HasSkill(BaseSkill skill)
    {
        foreach (BaseSkill hasSkill in transform)
        {
            if(hasSkill == skill)
                return true;
        }
        return false;
    }
    public void UpGradeSkill()
    {

    }
    public void ClearSkillList()
    {
        foreach (Transform skill in transform)
        {
            Destroy(skill);
        }
    }

    public void ApplyPassiveSkill()
    {
        foreach(PassiveSkill skill in transform)
        {
            skill.ApplySkill();
        }
    }

}
