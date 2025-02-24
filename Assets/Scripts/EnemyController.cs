using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : BaseController
{
    [SerializeField] WeaponHandler weaponHandler; // 무기 따로 할건지 정해야함.

    AnimationHandler animationHandler;
    EnemyManager enemyManager;
    Transform target;

    [SerializeField] LayerMask targetLayerMask; // basecontroller로 옮겨도 될듯.

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

        //룩디렉션, 무브디렉션, 이즈어태킹.
        lookDirection = direction;

        if (AttackAvailable(distance, direction))
        {
            Attack(); // 애니메이션 실행 메서드로 변경하고, 실제 공격판정은 애니메이션에서 이벤트로 트리거
        }
        else
        {
            //경로 찾아서 이동 방향으로 대입.
            movementDirection = FindWayToTarget();
        }
    }

    private bool AttackAvailable(float distance, Vector2 direction)
    {
        if(attackRange < distance)
            return false; // 거리가 공격범위보다 멀면 false
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange * 1.3f, targetLayerMask | LayerMask.NameToLayer("level"));
            if(hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer))
            {
                return true; // 바로 앞에 있는게 플레이어면 true;
            }
            return false; // 플레이어가 아니면 false;
        }
    }

    private void CheckAttackSuccess() // 유니티 애니메이션에 이벤트로 추가.
    {
        //RaycastHit2D hit = Physics2D.BoxCast(transform.position.)
            //공격 성공 => 플레이어의 피격 판정
    }

    public Vector2 FindWayToTarget()
    {
        //경로찾기
    }

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
