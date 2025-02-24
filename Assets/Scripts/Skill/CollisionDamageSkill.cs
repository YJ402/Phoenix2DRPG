using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageSkill : BaseSkill
{
    public float damageMultiplier = 1.0f;

    public override void Activate(GameObject user)
    {
        CollisionDamageEffect effect = user.AddComponent<CollisionDamageEffect>();
        effect.damageMultiplier = damageMultiplier;

        StateHandler stateHandler = user.GetComponent<StateHandler>();
        if (stateHandler != null)
        {
            effect.baseDamage = stateHandler.Attack;
        }
        else
        {
            effect.baseDamage = 10;
        }
        StartCoroutine(RemoveCollisionDamageEffect(user));
    }

    private IEnumerator RemoveCollsionDamageEffect(GameObject user)
    {
        yield return new WaitForSeconds(duration);
        CollsionDamageEffect effect = user.GetComponent<CollisionDamageEffect>();
        if (effect != null)
        {
            Destroy(effect);
        }
    }
}
