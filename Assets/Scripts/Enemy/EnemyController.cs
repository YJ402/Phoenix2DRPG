using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

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
        if (followRange > distance)
        {
            lookDirection = direction;

            if (TargetInLine(transform.position, direction) && TargetInDistance(direction))
            {
                isAttacking = true;
            }
            else
            {
                Vector2 nextDirection = BFS().normalized; //노멀라이즈
                movementDirection = nextDirection;
            }
            Attack(isAttacking); // 애니메이션 실행 메서드로 변경하고, 실제 공격판정은 애니메이션에서 이벤트로 트리거
        }
    }

    private Vector2 BFS()
    {
        Debug.Log($"플레이어 위치: {target.position}");
        //int[,] map = battleManager.Map;
        int[,] map = new int[10, 10] // 테스트용
{
        { 0, 0, 3, 0, 1, 0, 0, 1, 0, 0 },
        { 0, 0, 0, 1, 0, 3, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0 },
        { 0, 0, 0, 0, 1, 0, 0, 3, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 0, 3, 0, 0, 0 },
        { 3, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
        { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0, 3, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }
};


        //int MaxNavigation = Mathf.Clamp((int)Math.Pow((followRange*2+1),2),0,200);
        int MaxNavigation = int.MaxValue;

        Queue<Vector2> spots = new Queue<Vector2>(); // 한장짜리 수색 스팟 순서 목록
        HashSet<Vector2> isVisited = new HashSet<Vector2>();
        Stack<Vector2> path = new Stack<Vector2>();
        Vector2 spot;
        Vector2[,] infoFrom = new Vector2[map.GetLength(0), map.GetLength(1)]; // 수색 스팟 출신 정보를 기록할 문서다발
        for (int i = 0; i < map.GetLength(0); i++)
            for (int j = 0; j < map.GetLength(1); j++)
                infoFrom[i, j] = new Vector2(-1, -1);

        Vector2[] eightway = new Vector2[]
        {
                                new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, -1),
                                new Vector2(0, 1),                     new Vector2(0, -1),
                                new Vector2(-1, 1), new Vector2(-1, 0), new Vector2(-1, -1)
        };

        int x1 = Mathf.RoundToInt(target.position.x);
        int y1 = Mathf.RoundToInt(target.position.y);
        //Vector2 targetPos = new Vector2(x1, y1) - battleManager.obstacleManager.GridToWorld(0, 0); // 타겟 좌표를 일치화
        Vector2 targetPos = new Vector2(x1, y1) - new Vector2(-5, -5); // 테스트용

        int x2 = Mathf.RoundToInt(transform.position.x);
        int y2 = Mathf.RoundToInt(transform.position.y);
        //Vector2 finderPos = new Vector2(x2, y2) - battleManager.obstacleManager.GridToWorld(0, 0); // 현재 몹 좌표를 일치화
        Vector2 finderPos = new Vector2(x2, y2) - new Vector2(-5, -5); // 테스트용

        spots.Enqueue(finderPos);

        while (spots.Count > 0 && MaxNavigation-- > 0)
        {
            //하나 꺼내고
            spot = spots.Dequeue();
            //방문 체크
            //isVisited.Add(spot); // 방문 체크는 추가할때 즉시 해야함
            //검사: 타겟 위치인지
            if (spot == targetPos)
            {
                BackTracking(finderPos, spot, infoFrom, path);
                break;
            }
            //8방향 추가 및 출신 기록(방문 안한 곳만 / (0: 빈 공간, 2: 인접 영역)
            else
            {
                foreach (var i in eightway)
                {
                    Vector2 nextVector = new Vector2(spot.x + i.x, spot.y + i.y);

                    if (!isVisited.Contains(nextVector)
                            && (spot.x + i.x < map.GetLength(0))
                            && (spot.y + i.y < map.GetLength(1))
                            && (spot.x + i.x >= 0)
                            && (spot.y + i.y >= 0)
                            && (map[(int)(spot.x + i.x), (int)(spot.y + i.y)] == 0
                            || map[(int)(spot.x + i.x), (int)(spot.y + i.y)] == 2))
                    {
                        spots.Enqueue(nextVector);
                        isVisited.Add(nextVector); // 방문 처리는 미리 해야함.
                        infoFrom[(int)(spot.x + i.x), (int)(spot.y + i.y)] = spot;
                    }
                }
            }
        }

        if (path.Count != 0)
        {
            //Vector2 destination = battleManager.obstacleManager.GridToWorld(path.Pop().x, path.Pop().y);
            Vector2 destination = path.Pop() + new Vector2(-5, -5);
            Debug.Log($"{destination}으로 이동");
            Debug.Log($"플레이어 위치: {target.position}");

            return destination - (Vector2)transform.position;
        }
        else
        {
            Debug.Log("타겟이 너무 멀리 있습니다. 경로 찾기에 실패합니다.");
            return Vector2.zero;
        }
    }

    private void BackTracking(Vector2 finder, Vector2 vector, Vector2[,] infoFrom, Stack<Vector2> path)
    {
        if (vector == finder) // 기저사례 정하는 거 깜빡했음.
            return;
        path.Push(vector);
        BackTracking(finder, infoFrom[(int)vector.x, (int)vector.y], infoFrom, path);
    }


    private bool TargetInLine(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
        if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer)) // 플레이어면
            return true;
        return false;
    }

    private bool TargetInDistance(Vector2 origin)
    {
        if (statHandler.AttackRange > DistanceToTarget(origin))
            return true;
        return false;
    }
}
