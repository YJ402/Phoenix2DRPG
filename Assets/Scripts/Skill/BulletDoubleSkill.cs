using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDoubleSkill : ActiveSkill
{
    public float bulletMultiplier = 2f;

    public override void Activate(GameObject user)
    {
        StateHandler stateHandler = user.GetComponent<StateHandler>();
        if (stateHandler != null)
        {
            StartCoroutine(ApplyBulletDouble(stateHandler));
        }
    }
    private IEnumerator ApplyBulletDouble(StateHandler statehandler)
    {
        int originalBulletCount = statehandler.BulletCount;
        statehandler.BulletCount = Mathf.RoundToInt(originalBulletCount * bulletMultiplier);
        yield return new WaitForSeconds(duration);

        statehandler.BulletCount = originalBulletCount;
    }
}

