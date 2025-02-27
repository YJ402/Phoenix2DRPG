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
    //UI매니저의 커렌트 state 받아오기.


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
    public void RoundClear()  //적이 0이 됬을때 호출   보상UI띄우기 추가필요
    {
        Debug.Log("적을 모두 처치하였습니다.");
    }
    private void LoadPlayerData()
    {
        CurrentStage = PlayerData.Instance.CurrentStage;
        CurrentRound = PlayerData.Instance.CurrentRound;

        // 라운드 전환시에 데이터 저장 클래스에서 정보 받아와서 Player, 스테이지, 라운드 입력해주기.
    }

    private void StartRound() // 스킬 선택 끝나고 라운드 시작시에 UIManager에서 실행하도록 하는 게 괜찮을듯. 
    {
        
        obstacleManager.SettingObstacle();                               //장애물 생성
        enenmyManager.SpawnEnemiesInMap(5);                              //적 생성
        PlayerData.Instance.RoundStartPlayerSetting();
                                                       //
                        //
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

        //보스 있는지 체크 후 있다면 필요 메서드 구독.(보류)
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