using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    EnemyManager enenmyManager;
    PlayerController playerController;
    ObstacleManager obstacleManager;
    //UI�Ŵ����� Ŀ��Ʈ state �޾ƿ���.


    public GameObject player;

    int[,] map;
    int stage;
    int round;

    public static Transform PlayerTransform;


    List<EnemyController> restEnemy = new();
    BossEnemyController boss;

    public void Awake()
    {
        enenmyManager = GetComponent<EnemyManager>();
        playerController = GetComponent<PlayerController>();
        obstacleManager = GetComponent<ObstacleManager>();

        LoadPlayerData();

        enenmyManager.Init(map, stage);

    }

    private void LoadPlayerData()
    {
        // ���� ��ȯ�ÿ� ������ ���� Ŭ�������� ���� �޾ƿͼ� Player, ��������, ���� �Է����ֱ�.
    }

    private void StartRound() // ��ų ���� ������ ���� ���۽ÿ� UIManager���� �����ϵ��� �ϴ� �� ��������. 
    {
        //�� ��ֹ� ��ġ
        //���� ��ȯ �� ����Ʈ�� �߰�
        restEnemy = enenmyManager.SpawnEnemiesInMap(5);
        //�ʿ��ϴٸ� �÷��̾� ��ȯ or ����

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

        //���� �ִ��� üũ �� �ִٸ� �ʿ� �޼��� ����.
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
