using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] EnemyManager enenmyManager;
    PlayerController playerController;
    ResourceController playerResourceController;
    [SerializeField] ObstacleManager obstacleManager;


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
        // ¶ó¿îµå ÀüÈ¯½Ã¿¡ µ¥ÀÌÅÍ ÀúÀå Å¬·¡½º¿¡¼­ Á¤º¸ ¹Þ¾Æ¿Í¼­ Player, ½ºÅ×ÀÌÁö, ¶ó¿îµå ÀÔ·ÂÇØÁÖ±â.
    }

    private void StartRound() 
    {
        
        obstacleManager.SettingObstacle();                               //Àå¾Ö¹° »ý¼º
        LoadPlayerData();
        enenmyManager.Init(Map, CurrentStage);
        enenmyManager.SpawnEnemiesInMap(5);                              //Àû »ý¼º
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
                Debug.Log("ï¿½Ð½Ãºï¿½ ï¿½ï¿½Å³: " + passiveSkill.skillName + ", ï¿½ï¿½ï¿½ï¿½: " + passiveSkill.level);
            }
        }
        else
        {
            Debug.Log("ï¿½Ð½Ãºï¿½ ï¿½ï¿½Å³ï¿½ï¿½ ï¿½ï¿½ï¿½Ãµï¿½ï¿½ï¿½ ï¿½Ê¾Ò½ï¿½ï¿½Ï´ï¿½.");
        }

        //ï¿½ï¿½ï¿½ï¿½ ï¿½Ö´ï¿½ï¿½ï¿½ Ã¼Å© ï¿½ï¿½ ï¿½Ö´Ù¸ï¿½ ï¿½Ê¿ï¿½ ï¿½Þ¼ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½.(ï¿½ï¿½ï¿½ï¿½)
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
        boss.bossEvent[1] += enenmyManager.SpawnEnemy; // ï¿½Ù¼ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½È¯

        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½

        // ï¿½ï¿½ï¿½ï¿½ ï¿½Ìµï¿½
    }
    public void UpdateEnemyDeath(EnemyController enemy)
    {
        restEnemy.Remove(enemy);
        if (restEnemy.Count <= 0)
        {
            RoundClear();
        }
    }
    public void RoundClear()  //ÀûÀÌ 0ÀÌ ‰çÀ»¶§ È£Ãâ   º¸»óUI¶ç¿ì±â Ãß°¡ÇÊ¿ä
    {
        obstacleManager.BlockRemove();
        Time.timeScale = 0;

        Debug.Log("ÀûÀ» ¸ðµÎ Ã³Ä¡ÇÏ¿´½À´Ï´Ù.");
    }
    public void GoNextRound()
    {
        PlayerData.Instance.RoundEndSetting();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}