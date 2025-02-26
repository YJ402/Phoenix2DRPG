using System;
using UnityEngine;

public class MeleeEnemyController : EnemyController
{
    [SerializeField] protected Transform hitBox;

    public override void CheckHit()
    {
        Debug.Log("근접 공격");

        RaycastHit2D hit = Physics2D.BoxCast(hitBox.position, hitBox.lossyScale, 0, lookDirection, 0, targetLayerMask);
        if (hit.transform.gameObject.layer == 6)
            Debug.Log("타격 성공");

        if (!hit.collider.TryGetComponent<ResourceController>(out ResourceController resourceController))
            Debug.Log("피격 오브젝트에 리소스 컨트롤러가 없습니다.");
        resourceController.ChangeHealth(-statHandler.AttackPower);
    }
}