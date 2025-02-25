using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : BaseController
{
    AnimationHandler animationHandler;
    EnemyManager enemyManager;
    [SerializeField] Transform target; // 디버깅용 시리얼필드

    [SerializeField] LayerMask targetLayerMask; // basecontroller로 옮겨도 될듯.

    [SerializeField] private float attackRange;
    public float AttackRange { get { return attackRange; } }
    [SerializeField] private float followRange;
    public float FollowRange { get { return followRange; } }

    List<Vector2> obstaclePosition;

    public void Init(EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
        this.target = BattleManager.PlayerTransform;
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

        isAttacking = false;

        if (target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        Vector2 direction = DirectionToTarget();
        float distance = DistanceToTarget();

        if (followRange > distance)
        {
            lookDirection = direction;

            //룩디렉션, 무브디렉션, 이즈어태킹 설정 로직
            if (AttackAvailable(distance, direction))
            {
                isAttacking = true;
            }
            else
            {
                //직선상에 장애물/플레이어가 있는지 체크.             
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance + 0.3f, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
                if (hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer))
                {
                    movementDirection = direction;
                }
                else Debug.Log("길이 막혔습니다. 경로 찾기 로직을 실행합니다.");//movementDirection = FindWayToTarget(); //경로 찾아서 이동 방향으로 대입.
            }
        }
            Attack(isAttacking); // 애니메이션 실행 메서드로 변경하고, 실제 공격판정은 애니메이션에서 이벤트로 트리거
    }

    private bool AttackAvailable(float distance, Vector2 direction)
    {
        if(attackRange < distance)
            return false; // 거리가 공격범위보다 멀면 false
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange * 1.3f, targetLayerMask | (1 << LayerMask.NameToLayer("Level")));
            if(hit.collider != null && targetLayerMask == (1 << hit.collider.gameObject.layer))
            {
                return true; // 공격 범위 내에서 enemy 앞에 있는 것이 level이 아니라 플레이어면 true;
            }
            return false; // 플레이어가 아니라 level이면 false;
        }
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
