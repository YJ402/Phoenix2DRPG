using System;
using UnityEngine;

public class MeleeEnemyController : EnemyController
{
    [SerializeField] protected Transform hitBox;

    public override void CheckHit()
    {
        Debug.Log("���� ����");



        RaycastHit2D hit = Physics2D.BoxCast(hitBox.position, hitBox.lossyScale, 0, lookDirection.normalized, 0, targetLayerMask);
        if (hit.collider != null)
            Debug.Log("Ÿ�� ����");
        else
        {
            Debug.Log("Ÿ�� ����");
        }

        //OnDrawGizmos();

        if (!hit.collider.TryGetComponent<ResourceController>(out ResourceController resourceController))
        {
            Debug.Log("�ǰ� ������Ʈ�� ���ҽ� ��Ʈ�ѷ��� �����ϴ�.");
            return;
        }
        resourceController.ChangeHealth(-statHandler.AttackPower);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(hitBox.position, hitBox.lossyScale);

    //    // BoxCast�� ���� ǥ��
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawRay(hitBox.position, lookDirection.normalized * 1f);
    //}
}

