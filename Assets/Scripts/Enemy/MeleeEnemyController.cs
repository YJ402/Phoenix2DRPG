using System;
using UnityEngine;

public class MeleeEnemyController : EnemyController
{
    [SerializeField] protected Transform hitBox;

    public override void CheckHit()
    {
        Debug.Log("근접 공격");

        RaycastHit2D[] hit = Physics2D.BoxCastAll(hitBox.position, hitBox.lossyScale, 0, lookDirection.normalized, 0, targetLayerMask);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
                Debug.Log("타격 성공");
            else
            {
                Debug.Log("타격 실패");
                continue;
            }

            //OnDrawGizmos();

            if (!hit[i].collider.TryGetComponent<ResourceController>(out ResourceController resourceController))
            {
                Debug.Log("피격 오브젝트에 리소스 컨트롤러가 없습니다.");
                return;
            }
            resourceController.ChangeHealth(-statHandler.AttackPower);
            break;
        }
    }

    void OnDrawGizmos()
    {
        if (hitBox == null) return;

        Vector2 start = hitBox.position;
        Vector2 size = hitBox.lossyScale;
        Vector2 direction = lookDirection.normalized;
        float distance = 10f; // 캐스트할 거리

        // 박스 캐스트의 시작 위치
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(start, size);

        // 박스 캐스트의 도착 위치
        Vector2 end = start + direction * distance;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(end, size);
    }
}

