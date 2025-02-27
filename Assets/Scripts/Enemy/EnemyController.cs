using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : BaseController
{
    EnemyManager enemyManager;
    [SerializeField] Transform target; // ���������� �ø����ʵ�� ����. ���߿� ��Ʋ�Ŵ������� enemyController.Init�ϸ� �ڵ����� �Ҵ��.

    [SerializeField] protected LayerMask targetLayerMask; // ���������� �ø����ʵ�� ����. ���߿� ��Ʋ�Ŵ������� enemyController.Init�ϸ� �ڵ����� �Ҵ��.

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
                Vector2 nextDirection = BFS().normalized; //��ֶ�����
                movementDirection = nextDirection;
            }
            Attack(isAttacking); // �ִϸ��̼� ���� �޼���� �����ϰ�, ���� ���������� �ִϸ��̼ǿ��� �̺�Ʈ�� Ʈ����
        }
    }

    private Vector2 BFS()
    {
        Debug.Log($"�÷��̾� ��ġ: {target.position}");
        //int[,] map = battleManager.Map;
        int[,] map = new int[10, 10] // �׽�Ʈ��
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

        Queue<Vector2> spots = new Queue<Vector2>(); // ����¥�� ���� ���� ���� ���
        HashSet<Vector2> isVisited = new HashSet<Vector2>();
        Stack<Vector2> path = new Stack<Vector2>();
        Vector2 spot;
        Vector2[,] infoFrom = new Vector2[map.GetLength(0), map.GetLength(1)]; // ���� ���� ��� ������ ����� �����ٹ�
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
        //Vector2 targetPos = new Vector2(x1, y1) - battleManager.obstacleManager.GridToWorld(0, 0); // Ÿ�� ��ǥ�� ��ġȭ
        Vector2 targetPos = new Vector2(x1, y1) - new Vector2(-5, -5); // �׽�Ʈ��

        int x2 = Mathf.RoundToInt(transform.position.x);
        int y2 = Mathf.RoundToInt(transform.position.y);
        //Vector2 finderPos = new Vector2(x2, y2) - battleManager.obstacleManager.GridToWorld(0, 0); // ���� �� ��ǥ�� ��ġȭ
        Vector2 finderPos = new Vector2(x2, y2) - new Vector2(-5, -5); // �׽�Ʈ��

        spots.Enqueue(finderPos);

        while (spots.Count > 0 && MaxNavigation-- > 0)
        {
            //�ϳ� ������
            spot = spots.Dequeue();
            //�湮 üũ
            //isVisited.Add(spot); // �湮 üũ�� �߰��Ҷ� ��� �ؾ���
            //�˻�: Ÿ�� ��ġ����
            if (spot == targetPos)
            {
                BackTracking(finderPos, spot, infoFrom, path);
                break;
            }
            //8���� �߰� �� ��� ���(�湮 ���� ���� / (0: �� ����, 2: ���� ����)
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
                        isVisited.Add(nextVector); // �湮 ó���� �̸� �ؾ���.
                        infoFrom[(int)(spot.x + i.x), (int)(spot.y + i.y)] = spot;
                    }
                }
            }
        }

        if (path.Count != 0)
        {
            //Vector2 destination = battleManager.obstacleManager.GridToWorld(path.Pop().x, path.Pop().y);
            Vector2 destination = path.Pop() + new Vector2(-5, -5);
            Debug.Log($"{destination}���� �̵�");
            Debug.Log($"�÷��̾� ��ġ: {target.position}");

            return destination - (Vector2)transform.position;
        }
        else
        {
            Debug.Log("Ÿ���� �ʹ� �ָ� �ֽ��ϴ�. ��� ã�⿡ �����մϴ�.");
            return Vector2.zero;
        }
    }

    private void BackTracking(Vector2 finder, Vector2 vector, Vector2[,] infoFrom, Stack<Vector2> path)
    {
        if (vector == finder) // ������� ���ϴ� �� ��������.
            return;
        path.Push(vector);
        BackTracking(finder, infoFrom[(int)vector.x, (int)vector.y], infoFrom, path);
    }


    private bool TargetInLine(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
        if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer)) // �÷��̾��
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
