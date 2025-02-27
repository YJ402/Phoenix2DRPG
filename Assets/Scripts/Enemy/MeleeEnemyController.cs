using System;
using UnityEngine;

public class MeleeEnemyController : EnemyController
{
    [SerializeField] protected Transform hitBox;

    public override void CheckHit()
    {
        Debug.Log("근접 공격");



        RaycastHit2D hit = Physics2D.BoxCast(hitBox.position, hitBox.lossyScale, 0, lookDirection.normalized, 0, targetLayerMask);
        if (hit.collider != null)
            Debug.Log("타격 성공");
        else
        {
            Debug.Log("타격 실패");
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