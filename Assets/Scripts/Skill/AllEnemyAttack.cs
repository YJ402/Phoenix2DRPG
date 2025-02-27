using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemyAttack : ActiveSkill
{
    public float damage = 50f;
    public new string skillNmae = "전체공격";

    public override void Activate(GameObject user)
    {
        AreaAttack();
    }

    public void AreaAttack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            ResourceController resource = enemy.GetComponent<ResourceController>();
            if (resource != null)
            {
                resource.ChangeHealth(-damage);
            }
        }
    }
}
