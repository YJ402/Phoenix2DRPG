using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public bool isLevelSkillSelect = true; //�������� ���۽� �������ʽ��� ��ų����°����� ����
    private int currentStage = 1;

    private List<System.Type> acquiredPassiveSkillTypes = new List<System.Type>();

    // �нú� ��ų�� ȹ���� �� ȣ��
    public void ApplyPassiveSkill()
    {
        if (player != null)
        {
            // �÷��̾� GameObject�� ���� ��� PassiveSkill ������Ʈ�� �����ɴϴ�.
            PassiveSkill[] skills = player.GetComponents<PassiveSkill>();
            foreach (PassiveSkill skill in skills)
            {
                skill.ApplySkill();
            }
        }
        else
        {
            Debug.LogWarning("PlayerData: player ������ �����ϴ�!");
        }
    }


    public void ReapplyPassiveSkills(GameObject newPlayer)
    {
        foreach (var type in acquiredPassiveSkillTypes)
        {
            // �̹� �ش� Ÿ���� ������Ʈ�� ������ �߰�
            if (newPlayer.GetComponent(type) == null)
            {
                newPlayer.AddComponent(type);
                Debug.Log($"�нú� ��ų {type.Name} �������");
            }
        }
    }

    public int CurrentStage
    {
        get { return currentStage; }
        set { currentStage = value; }
    }
    private int currentRound = 1;
    public int CurrentRound
    {
        get { return currentRound; }
        private set { currentRound = value; }
    }

    [SerializeField] private int playerLevel=1;
    public int PlayerLevel
    {
        get { return playerLevel; }
        private set { playerLevel = value; }
    }
    private int playerExp;
    public int PlayerExp
    {
        get { return playerExp; }
        set
        {
            if (value >= MaxEXP)
            {
                PlayerLevel++;
                PlayerExp = value - 5 * (PlayerLevel - 1);
            }
            else { playerExp = value; }
        }
    }
    public int MaxEXP
    {
        get { return PlayerLevel*5; }
    }


    public Transform player;
    private int clearStage = 0;
    public int ClearStage
    {
        get { return clearStage; }
        set { clearStage = value; }
    }
    private int skillPoint=1;
    public int SkillPoint
    {
        get { return skillPoint; }
        set {  skillPoint = value; }
    }

    private int currentHP=1000;
    public int CurrentHP
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
    public void resetSkillPoint()
    {
        SkillPoint = PlayerLevel;
    }
    public void RoundStartPlayerSetting()
    {
        player = FindAnyObjectByType<PlayerController>().transform;
        ApplyPassiveSkill();
        player.GetComponent<ResourceController>().CurrentHealth = CurrentHP;
        player.GetComponent<PlayerController>().UpdateHpSlider((float)CurrentHP / player.GetComponent<StatHandler>().MaxHealth);
        player.transform.position = new Vector3(0.5f, -10f, player.transform.position.z);
    }

    public void GoNextRoundSetting()
    {
        currentHP = player.GetComponent<ResourceController>().CurrentHealth;
        CurrentRound++;
    }
    public void StageEndSetting()
    {
        CurrentHP = 1000;
        currentRound = 1;
        isLevelSkillSelect = true;
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


   
}
