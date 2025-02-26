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
    //UI매니저의 커렌트 state 받아오기.


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
        // 라운드 전환시에 데이터 저장 클래스에서 정보 받아와서 Player, 스테이지, 라운드 입력해주기.
    }

    private void StartRound() // 스킬 선택 끝나고 라운드 시작시에 UIManager에서 실행하도록 하는 게 괜찮을듯. 
    {
        //맵 장애물 배치
        //몬스터 소환 및 리스트에 추가
        restEnemy = enenmyManager.SpawnEnemiesInMap(5);
        //필요하다면 플레이어 소환 or 조정

        //몬스터에게 플레이어를 target으로 입력해주기.

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
                Debug.Log("패시브 스킬: " + passiveSkill.skillName + ", 레벨: " + passiveSkill.level);
            }
        }
        else
        {
            Debug.Log("패시브 스킬이 선택되지 않았습니다.");
        }

        //보스 있는지 체크 후 있다면 필요 메서드 구독.
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
        boss.bossEvent[1] += enenmyManager.SpawnEnemy; // 다섯 마리 소환

        // 범위 마법 공격

        // 순간 이동
    }
}
