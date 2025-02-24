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
            StateHandler enemyState = collision.gameObject.GetComponent<StateHandler>();
            if (enemyState != null)
            {
                int damageToApply = Mathf.RoundToInt(baseDamage * damageMultiplier);
                enemyState.Health -= damageToApply;
            }
        }
    }
}
