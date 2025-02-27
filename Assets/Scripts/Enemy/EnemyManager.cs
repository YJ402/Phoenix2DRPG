using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyManager : MonoBehaviour
{
    int[,] map;//맵 정보 필드로 받아오기
    int curStage; // 몇 스테이지인지 정보.
    [SerializeField] public List<EnemyController> stage1EnemyPrefab = new();
    [SerializeField] public List<EnemyController> stage2EnemyPrefab = new();
    [SerializeField] public List<EnemyController> stage3EnemyPrefab = new();

    List<List<EnemyController>> stageEnemyPrefabs;

    List<EnemyController> restEnemy = new();

    //프리팹, 생성 영역, 
    public void Init(int[,] _map, int _currentStage) // 
    {
        map = _map;// 맵 정보 필드로 받아오기
        curStage = _currentStage;// 몇 스테이지인지 정보.
        stageEnemyPrefabs = new List<List<EnemyController>>() { stage1EnemyPrefab, stage2EnemyPrefab, stage3EnemyPrefab };
    }

    public List<EnemyController> SpawnEnemiesInMap(int numOfEnemies = 5) //맵 생성 로직
    {
        if (numOfEnemies == 0)
        {
            Debug.Log("SpawnEnemies에 0 이상의 숫자를 입력 해야합니다.");
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
                Debug.Log("몬스터 생성 완료");
            numOfEnemies--;
        }

        return restEnemy;
    }

    public void SpawnEnemy() // 보스 소환기
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
                Debug.Log("몬스터 생성 완료");
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
