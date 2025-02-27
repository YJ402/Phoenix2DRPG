using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : BaseController
{
    EnemyManager enemyManager;
    [SerializeField] Transform target; // 디버깅용으로 시리얼필드로 만듦. 나중에 배틀매니저에서 enemyController.Init하면 자동으로 할당됨.

    [SerializeField] protected LayerMask targetLayerMask; // 디버깅용으로 시리얼필드로 만듦. 나중에 배틀매니저에서 enemyController.Init하면 자동으로 할당됨.

    [SerializeField] private float followRange;
    public float FollowRange { get { return followRange; } }

    List<Vector2> obstaclePosition;

    [SerializeField] protected float attackDelayT = 0.5f;
    protected float delayT;
    public void Init(EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
        target = battleManager.player.transform;
        targetLayerMask = target.gameObject.layer;
        delayT = attackDelayT;
    }

    protected override void Update()
    {
        base.Update();
        delayT -= Time.deltaTime;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentisLeft = Mathf.Abs(rotZ) > 90f;

        if (currentisLeft != previsLeft)
        {
            characterRenderer.transform.parent.rotation *= Quaternion.Euler(0, 180, 0);
        }

        previsLeft = currentisLeft;
    }


    public Vector2 DirectionToSomewhere(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        return direction;
    }

    public float DistanceToTarget(Vector2 origin)
    {
        float distance = ((Vector2)target.position - origin).magnitude;
        return distance;
    }



    protected override void HandleAction()
    {
        base.HandleAction();

        if (target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        Vector2 direction = DirectionToSomewhere(target.position);
        float distance = DistanceToTarget(transform.position);

        

        Attack(isAttacking); // 애니메이션 실행 메서드로 변경하고, 실제 공격판정은 애니메이션에서 이벤트로 트리거
    }

    private void FindPathToDestination(Vector2 direction, float distance)
    {
        if (followRange > distance) // 추격 범위 내
        {
            int[,] map = battleManager.Map;
            int MaxNavigation = 200;

            Queue<(int, int)> spots = new Queue<(int, int)>();
            List<(int, int)> isVisited = new List<(int, int)>();
            (int x, int y) spot;

            (int x, int y)[] eightway = new (int x, int y)[]
            {
                    (1, 1), (1, 0), (1, -1),
                    (0, 1),         (0, -1),
                    (-1, 1), (-1, 0), (-1, -1)
            };

            int x = Mathf.RoundToInt(transform.position.x);
            int y = Mathf.RoundToInt(transform.position.y);
            spots.Enqueue((x, y));

            while (spots.Count > 0 && MaxNavigation > 0)
            {
                //하나 꺼내고
                spot = spots.Dequeue();
                //방문 체크
                isVisited.Add(spot);
                //검사: 직선상인가? 공격 범위 안인가?
                if (TargetInLine(new Vector2(spot.x, spot.y), direction) && TargetInDiatance(new Vector2(spot.x, spot.y)))// 하나의 함수로 합칠 수 있음.
                {
                    movementDirection = DirectionToSomewhere(new Vector2(spot.x, spot.y));
                    break;
                }

                //8방향 추가(방문 안한 곳만)
                foreach (var i in eightway)
                {
                    Vector2 s = new Vector2(spot.x + i.x, spot.y + i.y) - battleManager.obstacleManager.GridToWorld(0, 0);
                    if (!isVisited.Contains((spot.x + i.x, spot.y + i.y)) && (map[(int)s.x, (int)s.y] == 1))
                        spots.Enqueue((spot.x + i.x, spot.y + i.y));
                }

                MaxNavigation--;
            }
        }
    }
    private bool TargetInLine(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
        if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer)) // 플레이어면
            return true;
        return false;
    }

    private bool TargetInDiatance(Vector2 origin)
    {
        if (statHandler.AttackRange > DistanceToTarget(origin))
            return true;
        return false;
    }










    //    if (followRange > distance)
    //    {
    //        lookDirection = direction;

    //        bool targetInDiatance = TargetInDiatance(distance, direction);
    //        bool targetInSight = TargetInSight(distance, direction);

    //        if (targetInDiatance)
    //        {
    //            if (targetInSight)
    //            {
    //                if (delayT < 0)
    //                {
    //                    isAttacking = true;
    //                    delayT = attackDelayT;
    //                }
    //                movementDirection = Vector2.zero;
    //            }
    //            else
    //            {
    //                Debug.Log("길이 막혔습니다. 경로 찾기 로직을 실행합니다.");
    //            }
    //        }
    //        else if (!isAttacking)
    //        {
    //            movementDirection = direction;
    //        }
    //        ////룩디렉션, 무브디렉션, 이즈어태킹 설정 로직
    //        //if (TargetInDiatance(distance, direction))
    //        //{
    //        //    isAttacking = true;
    //        //    movementDirection = Vector2.zero;
    //        //}
    //        //else
    //        //{
    //        //    //직선상에 장애물/플레이어가 있는지 체크.             
    //        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance + 0.3f, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
    //        //    if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer))
    //        //    {
    //        //        movementDirection = direction;
    //        //    }
    //        //    else Debug.Log("길이 막혔습니다. 경로 찾기 로직을 실행합니다.");//movementDirection = FindWayToTarget(); //경로 찾아서 이동 방향으로 대입.
    //        //}
    //    }
    //    Attack(isAttacking); // 애니메이션 실행 메서드로 변경하고, 실제 공격판정은 애니메이션에서 이벤트로 트리거
    //}

    //private bool TargetInSight(float distance, Vector2 direction)
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance + 0.3f, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
    //    if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer)) // 플레이어면
    //        return true;
    //    return false;
    //}
    //private bool TargetInDiatance(float distance, Vector2 direction)
    //{
    //    if (statHandler.AttackRange < distance)
    //        return false; // 거리가 공격범위보다 멀면 false
    //    else
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, statHandler.AttackRange * 1.3f, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
    //        if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer))
    //        {
    //            return true; // 공격 범위 내에서 enemy 앞에 있는 것이 level이 아니라 플레이어면 true;
    //        }
    //        return false; // 플레이어가 아니라 level이면 false;
    //    }
    //}

    private void OnDestroy()
    {
        Debug.Log("몹이 죽었습니다");
        battleManager.UpdateEnemyDeath(this);
    }



    //public Vector2 FindWayToTarget()
    //{
    //    //경로찾기

    //    //타겟과 나 좌표를 int화.
    //    Vector2 targetOnTarget = new Vector2((int)Math.Round(target.position.x), (int)Math.Round(target.position.y));
    //    Vector2 MeOnTarget = new Vector2((int)Math.Round(transform.position.x), (int)Math.Round(transform.position.y));

    //    //grid 범위 설정.
    //    Vector2 LTgrid = new Vector2(-13, 6);
    //    Vector2 RBgrid = new Vector2(-14, -10);

    //    //장애물 위치
    //    obstaclePosition;

    //    //그래프 생성
    //    Graph graph = new Graph();
    //}

    //private void Attack(Player player)
    //{
    //    if(weaponHandler)
    //    if (Targeting(player.transform)) // 공격 가능 판단
    //    {

    //    }

    //    Chasing(player.transform);
    //}

    //private bool Targeting(transform playerPosition)
    //{
    //    bool attackAvailable = (playerPosition - transform.position).size < weaponHandler.AttackRange; // 공격 가능 판단
    //    animationHandler.targeting();
    //    return attackAvailable;
    //}

    //private void Chasing()
}
