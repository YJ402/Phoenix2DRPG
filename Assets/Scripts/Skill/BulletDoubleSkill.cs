using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDoubleSkill : ActiveSkill
{
    public float bulletMultiplier = 2f;

    public override void Activate(GameObject user)
    {
        StatHandler statHandler = user.GetComponent<StatHandler>();
        if (statHandler != null)
        {
            StartCoroutine(ApplyBulletDouble(statHandler));
        }
    }
    private IEnumerator ApplyBulletDouble(StatHandler statehandler)
    {
        int originalBulletCount = stathandler.BulletCount;
        stathandler.BulletCount = Mathf.RoundToInt(originalBulletCount * bulletMultiplier);
        yield return new WaitForSeconds(duration);

        stathandler.BulletCount = originalBulletCount;
    }
}

