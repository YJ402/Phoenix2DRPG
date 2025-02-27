using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopSkill : ActiveSkill
{
    public override void Activate(GameObject user)
    {
        StartCoroutine(StopAllEnemies());
    }

    private IEnumerator StopAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Dictionary<StatHandler,float> originalSpeeds = new Dictionary<StatHandler,float>();
        
        foreach (GameObject enemy in enemies)
        {
            StatHandler stat = enemy.GetComponent<StatHandler>();
            if (stat == null)
            {
                originalSpeeds[stat] = stat.Speed;
                stat.Speed = 0;
            }
        }
        yield return new WaitForSeconds(duration);

        foreach (var pair in originalSpeeds)
        {
            pair.Key.Speed = pair.Value;
        }
    }
}
