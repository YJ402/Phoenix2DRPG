using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int currentStage = 0; 
    public int CurrentStage
    {
        get { return currentStage; }
        private set { currentStage = value; }
    }
    private int currentRound = 1;
    public int CurrentRound
    {
        get { return currentRound; }
        private set { currentRound = value; }
    }

    private int playerLevel;
    public int PlayerLevel
    {
        get { return playerLevel; }
        set { playerLevel = value; }
    }

    
    public Transform player;
    private int clearStage = 0;
    public int ClearStage
    {
        get { return clearStage; }
        set { clearStage = value; }
    }

    private float currentHP;
    public float CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }
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
    

    public void RoundStartPlayerSetting()
    {
        ApplyPassiveSkill();
        player.GetComponent<ResourceController>().CurrentHealth = CurrentHP;
        player.transform.position = new Vector3(0.5f, -10f, player.transform.position.z);
    }

    public void RoundEndSetting()
    {
        currentHP = player.GetComponent<ResourceController>().CurrentHealth;
        CurrentRound++;
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
    public void AddSkill(BaseSkill skill)
    {

    }
    public void UpGradeSkill(BaseSkill skill)
    {
        foreach(BaseSkill hasSkill in transform)
        {
            if(hasSkill == skill)
                skill.UpgradeSkill();
        }
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
