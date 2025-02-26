using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDoubleSkill : ActiveSkill
{
    public float bulletMultiplier = 2f;

    public override void Activate(GameObject user)
    {
        RangeStatHandler rangeStatHandler = user.GetComponent<RangeStatHandler>();
        if (rangeStatHandler != null)
        {
            StartCoroutine(ApplyBulletDouble(rangeStatHandler));
        }
    }
    private IEnumerator ApplyBulletDouble(RangeStatHandler rangeStatHandler)
    {
        int originalBulletCount = rangeStatHandler.BulletCount;
        rangeStatHandler.BulletCount = Mathf.RoundToInt(originalBulletCount * bulletMultiplier);
        yield return new WaitForSeconds(duration);
        rangeStatHandler.BulletCount = originalBulletCount;
    }
}

