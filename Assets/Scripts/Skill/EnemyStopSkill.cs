using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopSkill : BaseSkill
{
    public override void Activate(GameObject user)
    {
        StartCoroutine(StopAllEnemies());
    }

    private IEnumerator StopAllEnemies()
    {
        foreach (EnemyHealth enemy in EnemyHealth.allEnemies)
        {
            if (enemy != null)
            {
                EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
                if (movement != null)
                {
                    movement.enabled = false;
                }
            }
        }
        yield return new WaitForSeconds(duration);

        foreach (EnemyHealth enemy in EnemyHealth.allEnemies)
        {
            if (enemy != null)
            {
                EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
                if (movement != null)
                {
                    movement.enabled = true;
                }

            }
        }
    }
}
