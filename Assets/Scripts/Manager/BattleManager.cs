using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    EnemyManager enenmyManager;
    PlayerController playerController;
    ResourceController playerResourceController;
    ObstacleManager obstacleManager;
    //UI�Ŵ����� Ŀ��Ʈ state �޾ƿ���.


    public GameObject player;
    public GameObject enemys;

    int[,] map;
    int stage;
    int round;

    public static Transform PlayerTransform;


    List<EnemyController> restEnemy = new();
    BossEnemyController boss;

    public void Awake()
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
        enenmyManager = GetComponent<EnemyManager>();
        playerController = player.GetComponent<PlayerController>();
        playerController = player.GetComponent<PlayerController>();
        obstacleManager = GetComponent<ObstacleManager>();

        //LoadPlayerData();

        enenmyManager.Init(map, stage);

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
        // ���� ��ȯ�ÿ� ������ ���� Ŭ�������� ���� �޾ƿͼ� Player, ��������, ���� �Է����ֱ�.
    }

    private void StartRound() // ��ų ���� ������ ���� ���۽ÿ� UIManager���� �����ϵ��� �ϴ� �� ��������. 
    {
        //�� ��ֹ� ��ġ
        // enenmyManager.SpawnEnemies(5);
        //�ʿ��ϴٸ� �÷��̾� ��ȯ or ����
        player.transform.position = new Vector3(0.5f, -10f, player.transform.position.z);
        PlayerData.Instance.RoundStartPlayerSetting();
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
    
}