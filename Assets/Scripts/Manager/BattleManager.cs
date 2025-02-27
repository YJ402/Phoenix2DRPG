using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] public EnemyManager enenmyManager;
    PlayerController playerController;
    ResourceController playerResourceController;
    public ObstacleManager obstacleManager;
    //UI매니저의 커렌트 state 받아오기.


    public GameObject player;



    private int[,] map; public int[,] Map { get { return map; } set { map = value; } }
    private int currentStage = 0;
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

    private void StartRound()
    {

        obstacleManager.SettingObstacle();                               //장애물 생성
        LoadPlayerData();
        enenmyManager.Init(Map, CurrentStage);
        enenmyManager.SpawnEnemiesInMap(5);                              //적 생성
        restEnemy = enenmyManager.restEnemy;

        PlayerData.Instance.RoundStartPlayerSetting();
        //
        //

        player.transform.position = new Vector3(0.5f, -10f, player.transform.position.z);
        PlayerData.Instance.RoundStartPlayerSetting();


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
                Debug.Log(" " + passiveSkill.skillName + "" + passiveSkill.level);
            }
        }
        else
        {
            Debug.Log("");
        }


        //foreach (EnemyController enemy in restEnemy)
        //{
        //    if (enemy is BossEnemyController)
        //    {
        //        boss = enemy as BossEnemyController;
        //        SubscribeBossEvent();
        //    }
        //}
    }

    public void SubscribeBossEvent()
    {
        //boss.bossEvent[1] += enenmyManager.SpawnEnemy; //
    }
    public void UpdateEnemyDeath(EnemyController enemy)
    {
        restEnemy.Remove(enemy);
        if (restEnemy.Count <= 0)
        {
            RoundClear();
        }
    }
    public void RoundClear()
    {
        obstacleManager.BlockRemove();
        Time.timeScale = 0;
        if (CurrentRound == 10)
        {
            StageClear();
        }
        else
        {
            //보상 스킬선택 ui 호출
        }

        Debug.Log("적을 모두 처치하였습니다.");
    }
    public void StageClear()
    {
        PlayerData.Instance.PlayerExp += (int)(5 * Mathf.Pow(2, CurrentStage - 1));
        //클리어 ui 호출
    }
    public void GoNextRound()
    {
        PlayerData.Instance.RoundEndSetting();  //플레이어 체력 저장, 라운드증가
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddMonsterToList(List<EnemyController> enemylist)
    {
        foreach (EnemyController enemy in enemylist)
        {
            restEnemy.Add(enemy);
        }
    }
}