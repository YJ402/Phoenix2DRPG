using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    EnemyManager enenmyManager;
    PlayerController playerController;
    ObstacleManager obstacleManager;
    //UI�Ŵ����� Ŀ��Ʈ state �޾ƿ���.

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
        // ���� ��ȯ�ÿ� ������ ���� Ŭ�������� ���� �޾ƿͼ� Player, ��������, ���� �Է����ֱ�.
    }

    private void StartRound() // ��ų ���� ������ ���� ���۽ÿ� UIManager���� �����ϵ��� �ϴ� �� ��������. 
    {
        //�� ��ֹ� ��ġ
        enenmyManager.SpawnEnemies(5);
        //�ʿ��ϴٸ� �÷��̾� ��ȯ or ����

        //���Ϳ��� �÷��̾ target���� �Է����ֱ�.
    }
}