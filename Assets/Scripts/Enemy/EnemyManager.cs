using System.Collections.Generic;
using Unity.VisualScripting;
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

    public List<EnemyController> restEnemy = new();

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

        


        while (numOfEnemies > 0)
        {
            int x = Random.Range(0, map.GetLength(0));
            int y = Random.Range(0, map.GetLength(1));

            if (map[x, y] != 0)
            {
                continue;
            }
            
            int randNum = Random.Range(0, stageEnemyPrefabs[curStage-1].Count);

            GameObject newEnemy = Instantiate(stageEnemyPrefabs[curStage-1][randNum].gameObject, new Vector2(x- map.GetLength(0)/2, y - map.GetLength(0)/3), Quaternion.identity);
            newEnemy.transform.SetParent(transform, false);
            restEnemy.Add(newEnemy.GetComponent<EnemyController>());

            newEnemy.GetComponent<EnemyController>().Init(this);

            if (numOfEnemies == 1)
                Debug.Log("���� ���� �Ϸ�");
            numOfEnemies--;
        }

        return restEnemy;
    }

    public void SpawnEnemy(int numOfEnemies) // ���� ��ȯ��
    {
        int x = Random.Range(0, map.GetLength(0));
        int y = Random.Range(0, map.GetLength(1));

        List<EnemyController> AddedEnemy = new();
        while (numOfEnemies > 0)
        {
            if (map[x, y] != 0)
            {
                continue;
            }
            int randNum = Random.Range(0, stageEnemyPrefabs[curStage].Count);
            restEnemy.Add(Instantiate(stageEnemyPrefabs[curStage][randNum].gameObject, new Vector2(x, y), Quaternion.identity).GetComponent<EnemyController>());
            numOfEnemies--;
            if (numOfEnemies == 0)
                Debug.Log("������ ���� ���� �Ϸ�");
        }
    }

    //public void markTarget(PlayerController target) 
    //{
    //}

    //public void RemoveEnemyOnDeath(EnemyController enemy)
    //{

    //}
}
