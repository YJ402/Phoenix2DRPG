using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

        

        Attack(isAttacking); // �ִϸ��̼� ���� �޼���� �����ϰ�, ���� ���������� �ִϸ��̼ǿ��� �̺�Ʈ�� Ʈ����
    }

    private void FindPathToDestination(Vector2 direction, float distance)
    {
        if (followRange > distance) // �߰� ���� ��
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
                //�ϳ� ������
                spot = spots.Dequeue();
                //�湮 üũ
                isVisited.Add(spot);
                //�˻�: �������ΰ�? ���� ���� ���ΰ�?
                if (TargetInLine(new Vector2(spot.x, spot.y), direction) && TargetInDiatance(new Vector2(spot.x, spot.y)))// �ϳ��� �Լ��� ��ĥ �� ����.
                {
                    movementDirection = DirectionToSomewhere(new Vector2(spot.x, spot.y));
                    break;
                }

                //8���� �߰�(�湮 ���� ����)
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
        if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer)) // �÷��̾��
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
    //                Debug.Log("���� �������ϴ�. ��� ã�� ������ �����մϴ�.");
    //            }
    //        }
    //        else if (!isAttacking)
    //        {
    //            movementDirection = direction;
    //        }
    //        ////��𷺼�, ����𷺼�, �������ŷ ���� ����
    //        //if (TargetInDiatance(distance, direction))
    //        //{
    //        //    isAttacking = true;
    //        //    movementDirection = Vector2.zero;
    //        //}
    //        //else
    //        //{
    //        //    //������ ��ֹ�/�÷��̾ �ִ��� üũ.             
    //        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance + 0.3f, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
    //        //    if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer))
    //        //    {
    //        //        movementDirection = direction;
    //        //    }
    //        //    else Debug.Log("���� �������ϴ�. ��� ã�� ������ �����մϴ�.");//movementDirection = FindWayToTarget(); //��� ã�Ƽ� �̵� �������� ����.
    //        //}
    //    }
    //    Attack(isAttacking); // �ִϸ��̼� ���� �޼���� �����ϰ�, ���� ���������� �ִϸ��̼ǿ��� �̺�Ʈ�� Ʈ����
    //}

    //private bool TargetInSight(float distance, Vector2 direction)
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance + 0.3f, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
    //    if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer)) // �÷��̾��
    //        return true;
    //    return false;
    //}
    //private bool TargetInDiatance(float distance, Vector2 direction)
    //{
    //    if (statHandler.AttackRange < distance)
    //        return false; // �Ÿ��� ���ݹ������� �ָ� false
    //    else
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, statHandler.AttackRange * 1.3f, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
    //        if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer))
    //        {
    //            return true; // ���� ���� ������ enemy �տ� �ִ� ���� level�� �ƴ϶� �÷��̾�� true;
    //        }
    //        return false; // �÷��̾ �ƴ϶� level�̸� false;
    //    }
    //}

    private void OnDestroy()
    {
        Debug.Log("���� �׾����ϴ�");
        battleManager.UpdateEnemyDeath(this);
    }



    //public Vector2 FindWayToTarget()
    //{
    //    //���ã��

    //    //Ÿ�ٰ� �� ��ǥ�� intȭ.
    //    Vector2 targetOnTarget = new Vector2((int)Math.Round(target.position.x), (int)Math.Round(target.position.y));
    //    Vector2 MeOnTarget = new Vector2((int)Math.Round(transform.position.x), (int)Math.Round(transform.position.y));

    //    //grid ���� ����.
    //    Vector2 LTgrid = new Vector2(-13, 6);
    //    Vector2 RBgrid = new Vector2(-14, -10);

    //    //��ֹ� ��ġ
    //    obstaclePosition;

    //    //�׷��� ����
    //    Graph graph = new Graph();
    //}

    //private void Attack(Player player)
    //{
    //    if(weaponHandler)
    //    if (Targeting(player.transform)) // ���� ���� �Ǵ�
    //    {

    //    }

    //    Chasing(player.transform);
    //}

    //private bool Targeting(transform playerPosition)
    //{
    //    bool attackAvailable = (playerPosition - transform.position).size < weaponHandler.AttackRange; // ���� ���� �Ǵ�
    //    animationHandler.targeting();
    //    return attackAvailable;
    //}

    //private void Chasing()
}
