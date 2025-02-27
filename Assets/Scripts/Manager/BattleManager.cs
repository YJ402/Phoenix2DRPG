using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] public EnemyManager enenmyManager;
    PlayerController playerController;
    ResourceController playerResourceController;
    public ObstacleManager obstacleManager;
    //UI�Ŵ����� Ŀ��Ʈ state �޾ƿ���.


    public GameObject player;



    private int[,] map; public int[,] Map { get { return map; } set { map = value; } }
    private int currentStage = 1;
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
    public List<EnemyController> restEnemy = new();
    BossEnemyController boss;

    public void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }
    private void Start()
    {
        StartRound();
        if (CurrentRound == 1 && PlayerData.Instance.SkillPoint > 0)
        {
            uIManager.ChangeState(UIState.SelectSkill);
        }
        else
            uIManager.ChangeState(UIState.Battle);
        
    }


    private void LoadPlayerData()
    {
        Map = obstacleManager.Grid;
        CurrentStage = PlayerData.Instance.CurrentStage;
        CurrentRound = PlayerData.Instance.CurrentRound;
        // ���� ��ȯ�ÿ� ������ ���� Ŭ�������� ���� �޾ƿͼ� Player, ��������, ���� �Է����ֱ�.
    }

    private void StartRound()
    {
        
        obstacleManager.SettingObstacle();                               //��ֹ� ����
        LoadPlayerData();
        enenmyManager.Init(Map, CurrentStage);
        enenmyManager.SpawnEnemiesInMap(1);                              //�� ����
        restEnemy = enenmyManager.restEnemy;
        UpdateUIStart();
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
    public void UpdateUIStart()
    {
        uIManager.UpdateEnemyCountInBattleUI(restEnemy.Count);
        uIManager.UpdateRoundTxtInBattleUI(currentRound);
        uIManager.UpdateStageTxtInBattleUI(currentStage);
    }
    public void SubscribeBossEvent()
    {
        //boss.bossEvent[1] += enenmyManager.SpawnEnemy; //
    }
    public void UpdateEnemyDeath(EnemyController enemy)
    {
        restEnemy.Remove(enemy);
        uIManager.UpdateEnemyCountInBattleUI(restEnemy.Count);
        if (restEnemy.Count <= 0)
        {
            RoundClear();
        }
        
    }
    public void RoundClear()
    {
        obstacleManager.BlockRemove();
        Time.timeScale = 0;
        if (CurrentRound == 2)
        {
            StageClear();
        }
        else
        {
            uIManager.ChangeState(UIState.SelectSkill);
        }

    }
    public void StageClear()
    {
        
        int frontLevel = PlayerData.Instance.PlayerLevel;
        int plusExp = (int)(5 * Mathf.Pow(2, CurrentStage - 1));
        PlayerData.Instance.PlayerExp += plusExp;
        int backLevel = PlayerData.Instance.PlayerLevel;
        bool isLevelUp = backLevel != frontLevel;
        uIManager.UpdateStageClearUI(isLevelUp,plusExp,backLevel,PlayerData.Instance.PlayerExp);
        PlayerData.Instance.resetSkillPoint();
        uIManager.ChangeState(UIState.StageClear);
    }
    public void GoNextRound()
    {
        PlayerData.Instance.GoNextRoundSetting();  //�÷��̾� ü�� ����, ��������
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