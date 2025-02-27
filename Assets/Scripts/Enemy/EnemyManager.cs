using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyManager : MonoBehaviour
{
    int[,] map;//�� ���� �ʵ�� �޾ƿ���
    int curStage; // �� ������������ ����.
    [SerializeField] public List<EnemyController> stage1EnemyPrefab = new();
    [SerializeField] public List<EnemyController> stage2EnemyPrefab = new();
    [SerializeField] public List<EnemyController> stage3EnemyPrefab = new();

    List<List<EnemyController>> stageEnemyPrefabs;

    List<EnemyController> restEnemy = new();

    //������, ���� ����, 
    public void Init(int[,] _map, int _currentStage) // 
    {
        map = _map;// �� ���� �ʵ�� �޾ƿ���
        curStage = _currentStage;// �� ������������ ����.
        stageEnemyPrefabs = new List<List<EnemyController>>() { stage1EnemyPrefab, stage2EnemyPrefab, stage3EnemyPrefab };
    }

    public List<EnemyController> SpawnEnemiesInMap(int numOfEnemies = 5) //�� ���� ����
    {
        if (numOfEnemies == 0)
        {
            Debug.Log("SpawnEnemies�� 0 �̻��� ���ڸ� �Է� �ؾ��մϴ�.");
            return null;
        }

        int x = Random.Range(0, map.GetLength(0));
        int y = Random.Range(0, map.GetLength(1));


        while (numOfEnemies > 0)
        {
            if (map[x, y] != 0)
            {
                continue;
            }
            int randNum = Random.Range(0, stageEnemyPrefabs[curStage].Count);
            restEnemy.Add(Instantiate(stageEnemyPrefabs[curStage][randNum].gameObject, new Vector2(x, y), Quaternion.identity).GetComponent<EnemyController>());
            
            if (numOfEnemies == 1)
                Debug.Log("���� ���� �Ϸ�");
            numOfEnemies--;
        }

        return restEnemy;
    }

    public void SpawnEnemy() // ���� ��ȯ��
    {
        int numOfEnemies = 5;

        int x = Random.Range(0, map.GetLength(0));
        int y = Random.Range(0, map.GetLength(1));


        while (numOfEnemies > 0)
        {
            if (map[x, y] != 0)
            {
                continue;
            }
            int randNum = Random.Range(0, stageEnemyPrefabs[curStage].Count);
            restEnemy.Add(Instantiate(stageEnemyPrefabs[curStage][randNum].gameObject, new Vector2(x, y), Quaternion.identity).GetComponent<EnemyController>());

            if (numOfEnemies == 1)
                Debug.Log("���� ���� �Ϸ�");
            numOfEnemies--;
        }
    }

    //public void markTarget(PlayerController target) 
    //{
    //}

    //public void RemoveEnemyOnDeath(EnemyController enemy)
    //{

    //}
}
