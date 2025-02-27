using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int enemyCount;
    public int EnemyCount { get { return enemyCount; } set { enemyCount = value; } }
    private int playerLevel;
    public int PlayerLevel
    {
        get { return playerLevel; }
        set { playerLevel = value; }
    }
    private int skillPoint;
    public int SkillPoint
    {
        get { return skillPoint; }
        set { skillPoint = value; }
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
    public void UpdateEnemyCount(bool isEnemyDead = false)
    {
        EnemyCount = BattleManager.Instance.enemys.transform.childCount - (isEnemyDead ? 1 : 0);
        if (EnemyCount <= 0)
        {
            skillPoint++;
            //������ �Լ� ȣ��
            BattleManager.Instance.RoundClear();
        }

    }

    public void SatgeStartPlayerSetting()
    {
        SkillPoint = PlayerLevel;
    }
    public void RoundStartPlayerSetting()
    {
        ApplyPassiveSkill();
        player.GetComponent<ResourceController>().CurrentHealth = CurrentHP;
    }

    public void RoundEndPlayerSetting()
    {
        currentHP = player.GetComponent<ResourceController>().CurrentHealth;
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
