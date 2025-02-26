using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArrow : MonoBehaviour
{
    private Collider2D triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        // 현재 트리거 안에 있는 충돌체 감지
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;

        int count = triggerCollider.OverlapCollider(filter, colliders);
        for (int i = 0; i < count; i++)
        {
            ResourceController target = colliders[i].GetComponent<ResourceController>();
            if (target != null)
            {
            }
        }
    }
}
