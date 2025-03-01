using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossEnemyController : EnemyController
{
    ObstacleManager obstacleManager;
    [SerializeField] Enemyskill enemyskill;
    //public List<Action> bossEvent = new();
    int[,] map;
    [SerializeField] private int skill2Number = 20;


    protected override void Awake()
    {
        base.Awake();
        obstacleManager = FindObjectOfType<ObstacleManager>();
        battleManager = FindObjectOfType<BattleManager>();

        map = battleManager.Map;
    }


    protected override void Update()
    {
        base.Update();

    }

    protected override void Attack(bool isAttack)
    {
        base.Attack(isAttack); // Attack 애니메이션 트리거 // 추가 로직 작성 안할거면 지워도 무방.
    }

    public void AttackSkill(int i)
    {
        switch (i)
        {
            case 1:
                Attack1();
                break;

            case 2:
                Attack2();
                break;

            case 3:
                Attack3();
                break;

            default:
                Debug.Log("잘못된 스킬번호입니다");
                break;
        }
    }
    private void Attack1() // 몹 2마리 소환
    {
        Debug.Log("1실행");

        enemyManager.SpawnEnemiesByBoss(1);
    }


    private void Attack2() // Boom
    {
        Debug.Log("2실행");
        for (int i = 0; i < skill2Number; i++)
        {
            Instantiate(enemyskill, randomSpotInMap(), Quaternion.identity);
            //Instantiate(enemyskill, Vector2.zero, Quaternion.identity);
        }
    }

    private void Attack3() // 순간이동
    {
        Debug.Log("3실행");
        Vector2 farPos = Vector2.zero;
        float farDistance = float.MinValue;

        List<Vector2> TeleportPos = new List<Vector2>() {
            new Vector2(-12, 5),
            new Vector2(14, 5),
            new Vector2(14, -10),
            new Vector2(-12, -10)
        };

        foreach (Vector2 pos in TeleportPos)
        {
            float curDistance = distanceToPlayer(pos);
            if (curDistance > farDistance)
            {
                farDistance = curDistance;
                farPos = pos;
            }
        }

        transform.position = farPos;
    }

    private float distanceToPlayer(Vector2 TeleportPos)
    {
        Vector2 distance = TeleportPos - (Vector2)battleManager.player.transform.position;
        return distance.magnitude;
    }

    private Vector2 randomSpotInMap() // 장애물이 없는 랜덤한 스팟 한곳 찾기.
    {
        while (true)
        {
            int x = Random.Range(0, map.GetLength(0));
            int y = Random.Range(0, map.GetLength(1));

            for (int i = x; i < map.GetLength(0); i++)
            {
                for (int j = y; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 0 || map[i, j] == 2)
                    {
                        return obstacleManager.GridToWorld(x, y);
                    }
                }
            }
        }
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
}
