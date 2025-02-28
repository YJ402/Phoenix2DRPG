using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyManager : MonoBehaviour
{
    int[,] map;//�� ���� �ʵ�� �޾ƿ���
    int curStage; // �� ������������ ����.
    int curRound;

    [SerializeField] public List<EnemyController> stage1EnemyPrefab = new();
    [SerializeField] public List<EnemyController> stage2EnemyPrefab = new();
    [SerializeField] public List<EnemyController> stage3EnemyPrefab = new();

    List<List<EnemyController>> stageEnemyPrefabs;

    [SerializeField] public List<EnemyController> BossesPrefab = new();

    public List<EnemyController> restEnemy = new();

    //������, ���� ����, 
    public void Init(int[,] _map, int _currentStage) // 
    {
        map = _map;// �� ���� �ʵ�� �޾ƿ���
        //curStage = _currentStage;// �� ������������ ����.
        stageEnemyPrefabs = new List<List<EnemyController>>() { stage1EnemyPrefab, stage2EnemyPrefab, stage3EnemyPrefab };

        curStage = PlayerData.Instance.CurrentStage; // �� ������������ ����.
        curRound = PlayerData.Instance.CurrentRound;
    }

    public void SpawnEnemiesInMap(int numOfEnemies = 5) //�� ��ȯ ����
    {
        if (curRound == 3)
            numOfEnemies = 1;
        if (numOfEnemies == 0)
        {
            Debug.Log("SpawnEnemies�� 0 �̻��� ���ڸ� �Է� �ؾ��մϴ�.");
            return;
        }

        while (numOfEnemies > 0)
        {
            int x = Random.Range(0, map.GetLength(0));
            int y = Random.Range(0, map.GetLength(1));

            GameObject newEnemy;
            if (map[x, y] != 0 && map[x, y] != 2)
            {
                continue;
            }

            int randNum = Random.Range(0, stageEnemyPrefabs[curStage - 1].Count);

            if (curRound != 3)
            {
                newEnemy = Instantiate(stageEnemyPrefabs[curStage - 1][randNum].gameObject, new Vector2(x - map.GetLength(0) / 2, y - map.GetLength(0) / 3), Quaternion.identity);
            }
            else
            {
                newEnemy = Instantiate(BossesPrefab[curStage - 1].gameObject, new Vector2(x - map.GetLength(0) / 2, y - map.GetLength(0) / 3), Quaternion.identity);
            }

            newEnemy.transform.SetParent(transform, false);
            restEnemy.Add(newEnemy.GetComponent<EnemyController>());

            newEnemy.GetComponent<EnemyController>().Init(this);

            if (numOfEnemies == 1)
                Debug.Log("���� ���� �Ϸ�");
            numOfEnemies--;
        }

        return;
    }

    public void SpawnEnemiesByBoss(int numOfEnemies = 1) //���� ��ȯ ����
    {
        if (numOfEnemies == 0)
        {
            Debug.Log("SpawnEnemies�� 0 �̻��� ���ڸ� �Է� �ؾ��մϴ�.");
            return;
        }

        while (numOfEnemies > 0)
        {
            int x = Random.Range(0, map.GetLength(0));
            int y = Random.Range(0, map.GetLength(1));

            GameObject newEnemy;
            if (map[x, y] != 0 && map[x, y] != 2)
            {
                continue;
            }

            int randNum = Random.Range(0, stageEnemyPrefabs[curStage - 1].Count);

            newEnemy = Instantiate(stageEnemyPrefabs[curStage - 1][randNum].gameObject, new Vector2(x - map.GetLength(0) / 2, y - map.GetLength(0) / 3), Quaternion.identity);
            newEnemy.transform.SetParent(transform, false);
            restEnemy.Add(newEnemy.GetComponent<EnemyController>());

            newEnemy.GetComponent<EnemyController>().Init(this);

            if (numOfEnemies == 1)
                Debug.Log("���� ���� �Ϸ�");
            numOfEnemies--;
        }

        return;
    }

    //public void SpawnthingInMap(int numOfthing, List<GameObject> things, Transform _transform) // ������ ��ȯ ��ų
    //{

    //    //List<EnemyController> AddedEnemy = new();
    //    while (numOfthing > 0)
    //    {
    //        int x = Random.Range(0, map.GetLength(0));
    //        int y = Random.Range(0, map.GetLength(1));

    //        GameObject newEnemy;
    //        if (map[x, y] != 0 || map[x, y] != 2)
    //        {
    //            continue;
    //        }
    //        int randNum = Random.Range(0, things.Count);

    //        GameObject i = Instantiate(things[randNum], new Vector2(x, y), Quaternion.identity);
    //        i.transform.SetParent(_transform, false);

    //        if (numOfthing == 0)
    //            Debug.Log("������ ���� ���� �Ϸ�");
    //        numOfthing--;
    //    }
    //}

    //public void markTarget(PlayerController target) 
    //{
    //}

    //public void RemoveEnemyOnDeath(EnemyController enemy)
    //{

    //}
}
