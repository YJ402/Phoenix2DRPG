using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossEnemyController : EnemyController
{
    BattleManager battleManager;
    ObstacleManager obstacleManager;
    [SerializeField] Enemyskill enemyskill;
    //public List<Action> bossEvent = new();
    int[,] map;
    [SerializeField] private int skill2Number = 20;


    protected override void Awake()
    {
        base.Awake();
        //bossEvent.Add(() => { });
        //bossEvent.Add(() => { });
        //bossEvent.Add(() => { });
        //bossEvent[0] += Attack1;
        //bossEvent[1] += Attack2;
        //bossEvent[2] += Attack3;
        obstacleManager = FindObjectOfType<ObstacleManager>();
        battleManager = FindObjectOfType<BattleManager>();

        //map = battleManager.Map;
    }

    //public void TriggerBossEvent(int i)
    //{
    //    bossEvent[i]?.Invoke();
    //}

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
    private void Attack1() // 몹 5마리 소환
    {
        Debug.Log("1실행");

        battleManager.enenmyManager.SpawnEnemy(5);
    }


    private void Attack2() // Boom
    {
        Debug.Log("2실행");
        for (int i = 0; i < skill2Number; i++)
        {
            //Instantiate(enemyskill, randomSpotInMap(), Quaternion.identity);
            Instantiate(enemyskill, Vector2.zero, Quaternion.identity);
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
                    if (map[i, j] == 0)
                    {
                        return obstacleManager.GridToWorld(x, y);
                    }
                }
            }
        }
    }
}
