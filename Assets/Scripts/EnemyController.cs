using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : BaseController
{
    [SerializeField] WeaponHandler weaponHandler; // ���� ���� �Ұ��� ���ؾ���.

    AnimationHandler animationHandler;
    EnemyManager enemyManager;
    Transform target;

    [SerializeField] LayerMask targetLayerMask; // basecontroller�� �Űܵ� �ɵ�.

    float attackRange;

    public void Init(EnemyManager enemyManager, Transform target)
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

        if (weaponHandler == null || target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        Vector2 direction = DirectionToTarget();
        float distance = DistanceToTarget();

        //��𷺼�, ����𷺼�, �������ŷ.
        lookDirection = direction;

        if (AttackAvailable(distance, direction))
        {
            Attack(); // �ִϸ��̼� ���� �޼���� �����ϰ�, ���� ���������� �ִϸ��̼ǿ��� �̺�Ʈ�� Ʈ����
        }
        else
        {
            //��� ã�Ƽ� �̵� �������� ����.
            movementDirection = FindWayToTarget();
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

    public Vector2 FindWayToTarget()
    {
        //���ã��
    }

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
