using System;
using UnityEngine;
using static UnityEngine.UI.Image;

public class MeleeEnemyController : EnemyController
{
    [SerializeField] protected Transform hitBox;

    public override void CheckHit()
    {
        //RaycastHit2D hit = Physics2D.BoxCast(hitBox.position, hitBox.lossyScale, 0, lookDirection.normalized, 0, targetLayerMask);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, statHandler.AttackRange + 0.2f, (1 << targetLayerMask));
        Debug.DrawRay(transform.position, lookDirection* (statHandler.AttackRange + 0.2f), Color.red);
        if (hit.collider != null)
            Debug.Log("근접 공격 성공");
        else
        {
            Debug.Log("근접 공격 실패");
        }

        //OnDrawGizmos();

        if (!hit.collider.TryGetComponent<ResourceController>(out ResourceController resourceController))
        {
            Debug.Log("피격 오브젝트에 리소스 컨트롤러가 없습니다.");
            return;
        }
        resourceController.ChangeHealth(-statHandler.AttackPower);
    }
}