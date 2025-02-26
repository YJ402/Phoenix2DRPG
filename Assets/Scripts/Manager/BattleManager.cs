using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    EnemyManager enenmyManager;
    PlayerController playerController;
    ObstacleManager obstacleManager;
    //UI매니저의 커렌트 state 받아오기.

    int[,] map;
    int stage;
    int round;

    public static Transform PlayerTransform;

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
        enenmyManager.SpawnEnemies(5);
        //필요하다면 플레이어 소환 or 조정

        //몬스터에게 플레이어를 target으로 입력해주기.
    }
}