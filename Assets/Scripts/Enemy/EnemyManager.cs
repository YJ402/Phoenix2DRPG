using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyManager : MonoBehaviour
{
    int[,] map;//맵 정보 필드로 받아오기
    int curStage; // 몇 스테이지인지 정보.
    int curRound;

    [SerializeField] public List<EnemyController> stage1EnemyPrefab = new();
    [SerializeField] public List<EnemyController> stage2EnemyPrefab = new();
    [SerializeField] public List<EnemyController> stage3EnemyPrefab = new();

    List<List<EnemyController>> stageEnemyPrefabs;

    [SerializeField] public List<EnemyController> BossesPrefab = new();

    public List<EnemyController> restEnemy = new();

    //프리팹, 생성 영역, 
    public void Init(int[,] _map, int _currentStage) // 
    {
        map = _map;// 맵 정보 필드로 받아오기
        //curStage = _currentStage;// 몇 스테이지인지 정보.
        stageEnemyPrefabs = new List<List<EnemyController>>() { stage1EnemyPrefab, stage2EnemyPrefab, stage3EnemyPrefab };

        curStage = PlayerData.Instance.CurrentStage; // 몇 스테이지인지 정보.
        curRound = PlayerData.Instance.CurrentRound;
    }

    public void SpawnEnemiesInMap(int numOfEnemies = 5) //몹 소환 로직
    {
        if (curRound == 3)
            numOfEnemies = 1;
        if (numOfEnemies == 0)
        {
            Debug.Log("SpawnEnemies에 0 이상의 숫자를 입력 해야합니다.");
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
                Debug.Log("몬스터 생성 완료");
            numOfEnemies--;
        }

        return;
    }

    public void SpawnEnemiesByBoss(int numOfEnemies = 1) //보스 소환 로직
    {
        if (numOfEnemies == 0)
        {
            Debug.Log("SpawnEnemies에 0 이상의 숫자를 입력 해야합니다.");
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
                Debug.Log("몬스터 생성 완료");
            numOfEnemies--;
        }

        return;
    }

    //public void SpawnthingInMap(int numOfthing, List<GameObject> things, Transform _transform) // 보스의 소환 스킬
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
    //            Debug.Log("보스의 몬스터 생성 완료");
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
