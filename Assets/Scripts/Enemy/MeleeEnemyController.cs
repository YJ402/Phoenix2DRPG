using System;
using UnityEngine;

public class MeleeEnemyController : EnemyController
{
    [SerializeField] protected Transform hitBox;

    public override void CheckHit()
    {
        Debug.Log("���� ����");

        RaycastHit2D[] hit = Physics2D.BoxCastAll(hitBox.position, hitBox.lossyScale, 0, lookDirection.normalized, 0, targetLayerMask);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
                Debug.Log("Ÿ�� ����");
            else
            {
                Debug.Log("Ÿ�� ����");
                continue;
            }

            //OnDrawGizmos();

            if (!hit[i].collider.TryGetComponent<ResourceController>(out ResourceController resourceController))
            {
                Debug.Log("�ǰ� ������Ʈ�� ���ҽ� ��Ʈ�ѷ��� �����ϴ�.");
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
        float distance = 10f; // ĳ��Ʈ�� �Ÿ�

        // �ڽ� ĳ��Ʈ�� ���� ��ġ
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(start, size);

        // �ڽ� ĳ��Ʈ�� ���� ��ġ
        Vector2 end = start + direction * distance;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(end, size);
    }
}

