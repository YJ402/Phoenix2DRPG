using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : BaseController
{
    AnimationHandler animationHandler;
    EnemyManager enemyManager;
    Transform target;

    [SerializeField] LayerMask targetLayerMask; // basecontroller�� �Űܵ� �ɵ�.

    [SerializeField] private float attackRange;
    public float AttackRange { get { return attackRange; } }
    [SerializeField] private float followRange;
    public float FollowRange { get { return followRange; } }

    List<Vector2> obstaclePosition;

    public void Init(EnemyManager enemyManager, Transform target, Graph graph)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    public Vector2 DirectionToTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        return direction;
    }

    public float DistanceToTarget()
    {
        float distance = (target.position - transform.position).magnitude;
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

        Vector2 direction = DirectionToTarget();
        float distance = DistanceToTarget();

        if(followRange > distance)
        lookDirection = direction;

        //��𷺼�, ����𷺼�, �������ŷ.
        if (AttackAvailable(distance, direction))
        {
            Attack(); // �ִϸ��̼� ���� �޼���� �����ϰ�, ���� ���������� �ִϸ��̼ǿ��� �̺�Ʈ�� Ʈ����
        }
        else
        {
            //������ ��ֹ� �÷��̾ �ִ��� üũ.             
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance+0.3f, targetLayerMask | LayerMask.NameToLayer("level"));
            if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer))
            {
                movementDirection = direction;
            }
            else Debug.Log("���� �������ϴ�. ��� ã�� ������ �����մϴ�.");//movementDirection = FindWayToTarget(); //��� ã�Ƽ� �̵� �������� ����.
        }
    }

    private bool AttackAvailable(float distance, Vector2 direction)
    {
        if(attackRange < distance)
            return false; // �Ÿ��� ���ݹ������� �ָ� false
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange * 1.3f, targetLayerMask | LayerMask.NameToLayer("level"));
            if(hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer))
            {
                return true; // �ٷ� �տ� �ִ°� �÷��̾�� true;
            }
            return false; // �÷��̾ �ƴϸ� false;
        }
    }

    private void CheckAttackSuccess() // ����Ƽ �ִϸ��̼ǿ� �̺�Ʈ�� �߰�.
    {
        //RaycastHit2D hit = Physics2D.BoxCast(transform.position.)
            //���� ���� => �÷��̾��� �ǰ� ���� 
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
