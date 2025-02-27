using System.Collections.Generic;
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


    
    private int[,] map; public int[,] Map {  get { return map; } set { map = value; } }
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
    List<EnemyController> restEnemy = new();
    BossEnemyController boss;

    public void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }
    private void Start()
    {
        StartRound();
    }
    
    private void LoadPlayerData()
    {
        Map = obstacleManager.Grid;
        CurrentStage = PlayerData.Instance.CurrentStage;
        CurrentRound = PlayerData.Instance.CurrentRound;
        // 라운드 전환시에 데이터 저장 클래스에서 정보 받아와서 Player, 스테이지, 라운드 입력해주기.
    }

    private void StartRound() // ��ų ���� ������ ���� ���۽ÿ� UIManager���� �����ϵ��� �ϴ� �� ��������. 
    {
        
        obstacleManager.SettingObstacle();                               //장애물 생성
        LoadPlayerData();
        enenmyManager.Init(Map, CurrentStage);
        enenmyManager.SpawnEnemiesInMap(5);                              //적 생성
        restEnemy = enenmyManager.restEnemy;

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
    public void UpdateEnemyDeath(EnemyController enemy)
    {
        restEnemy.Remove(enemy);
        if (restEnemy.Count <= 0)
        {
            RoundClear();
        }
    }
    public void RoundClear()  //적이 0이 됬을때 호출   보상UI띄우기 추가필요
    {
        obstacleManager.BlockRemove();
        Time.timeScale = 0;

        Debug.Log("적을 모두 처치하였습니다.");
    }
    public void GoNextRound()
    {
        PlayerData.Instance.RoundEndSetting();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}