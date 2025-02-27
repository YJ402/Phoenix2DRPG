using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] EnemyManager enenmyManager;
    PlayerController playerController;
    ResourceController playerResourceController;
    [SerializeField] ObstacleManager obstacleManager;
    //UI�Ŵ����� Ŀ��Ʈ state �޾ƿ���.


    public GameObject player;
    public GameObject enemys;


    
    int[,] map;
    private int currentStage=0;
    public int CurrentStage
    {
        get { return currentStage; }
        private set { currentStage = value; }
    }
    private int currentRound = 0;
    public int CurrentRound
    {
        get { return currentRound; }
        private set { currentRound = value; }
    }
    public static Transform PlayerTransform;

    List<EnemyController> restEnemy = new();
    BossEnemyController boss;

    public void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        playerController = player.GetComponent<PlayerController>();
        LoadPlayerData();

        enenmyManager.Init(map, CurrentStage);

    }
    private void Start()
    {
        StartRound();
    }
    public void RoundClear()  //���� 0�� ������ ȣ��   ����UI���� �߰��ʿ�
    {
        Debug.Log("���� ��� óġ�Ͽ����ϴ�.");
    }
    private void LoadPlayerData()
    {
        CurrentStage = PlayerData.Instance.CurrentStage;
        CurrentRound = PlayerData.Instance.CurrentRound;

        // ���� ��ȯ�ÿ� ������ ���� Ŭ�������� ���� �޾ƿͼ� Player, ��������, ���� �Է����ֱ�.
    }

    private void StartRound() // ��ų ���� ������ ���� ���۽ÿ� UIManager���� �����ϵ��� �ϴ� �� ��������. 
    {
        
        obstacleManager.SettingObstacle();                               //��ֹ� ����
        enenmyManager.SpawnEnemiesInMap(5);                              //�� ����
        PlayerData.Instance.RoundStartPlayerSetting();
                                                       //
                        //
        //���Ϳ��� �÷��̾ target���� �Է����ֱ�.

        PlayerSkill playerskill = player.GetComponent<PlayerSkill>();
        if (playerskill != null && playerskill.activeSkill != null)
        {
            ActiveSkill activeSkill = playerskill.activeSkill;
        }
        PassiveSkill[] passiveSkills = player.GetComponents<PassiveSkill>();
        if (passiveSkills.Length > 0)
        {
            foreach (PassiveSkill passiveSkill in passiveSkills)
            {
                Debug.Log("�нú� ��ų: " + passiveSkill.skillName + ", ����: " + passiveSkill.level);
            }
        }
        else
        {
            Debug.Log("�нú� ��ų�� ���õ��� �ʾҽ��ϴ�.");
        }

        //���� �ִ��� üũ �� �ִٸ� �ʿ� �޼��� ����.(����)
        foreach (EnemyController enemy in restEnemy)
        {
            if (enemy is BossEnemyController)
            {
                boss = enemy as BossEnemyController;
                SubscribeBossEvent();
            }
        }
    }

    public void SubscribeBossEvent()
    {
        boss.bossEvent[1] += enenmyManager.SpawnEnemy; // �ټ� ���� ��ȯ

        // ���� ���� ����

        // ���� �̵�
    }
    public void UpdateEnemyCount(EnemyController enemy)
    {
        restEnemy.Remove(enemy);
        if (restEnemy.Count <= 0)
        {
            RoundClear();
        }
    }
    public void GoNextRound()
    {
        PlayerData.Instance.RoundEndSetting();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}