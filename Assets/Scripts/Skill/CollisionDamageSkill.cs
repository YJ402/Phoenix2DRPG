using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageSkill : ActiveSkill
{
    public float damageMultiplier = 1.0f;

    public override void Activate(GameObject user)
    {
        CollisionDamageEffect effect = user.AddComponent<CollisionDamageEffect>();
        effect.damageMultiplier = damageMultiplier;

        StatHandler statHandler = user.GetComponent<StatHandler>();
        if (statHandler != null)
        {
            effect.baseDamage = statHandler.AttackPower;
        }
        else
        {
            effect.baseDamage = 10;
        }
        StartCoroutine(RemoveCollsionDamageEffect(user));
    }

    private IEnumerator RemoveCollsionDamageEffect(GameObject user)
    {
        yield return new WaitForSeconds(duration);
        CollisionDamageEffect effect = user.GetComponent<CollisionDamageEffect>();
        if (effect != null)
        {
            Destroy(effect);
        }
    }
}
