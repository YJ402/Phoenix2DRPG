using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] public EnemyManager enenmyManager;
    PlayerController playerController;
    ResourceController playerResourceController;
    public ObstacleManager obstacleManager;
    public GameObject player;
    
    public bool IsBossCleared { get; private set; }
    
    
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
    //BossEnemyController boss;

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
        // ���� ��ȯ�ÿ� ������ ���� Ŭ�������� ���� �޾ƿͼ� Player, ��������, ���� �Է����ֱ�.
    }

    private void StartRound()
    {

        obstacleManager.SettingObstacle();                               //��ֹ� ����
        LoadPlayerData();
        enenmyManager.Init(Map, CurrentStage);
        enenmyManager.SpawnEnemiesInMap(5);                              //�� ����
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

    //public void SubscribeBossEvent()
    //{
    //    boss.bossEvent[1] += enenmyManager.SpawnEnemy; //
    //}
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
            //���� ��ų���� ui ȣ��
        }

        Debug.Log("���� ��� óġ�Ͽ����ϴ�.");
    }
    public void StageClear()
    {
        PlayerData.Instance.PlayerExp += (int)(5 * Mathf.Pow(2, CurrentStage - 1));
        //Ŭ���� ui ȣ��
    }
    public void GoNextRound()
    {
        PlayerData.Instance.RoundEndSetting();  //�÷��̾� ü�� ����, ��������
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