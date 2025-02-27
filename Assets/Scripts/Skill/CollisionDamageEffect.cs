using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageEffect : MonoBehaviour
{
    public float baseDamage;
    public float damageMultiplier = 1.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ResourceController resource = collision.gameObject.GetComponent<ResourceController>();
            if (resource != null)
            {
                int damageToApply = Mathf.RoundToInt(baseDamage * damageMultiplier);
                resource.ChangeHealth(-damageToApply);
            }
        }
    }
}
