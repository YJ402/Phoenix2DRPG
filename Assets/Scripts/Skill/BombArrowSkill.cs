using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArrowSkill : ActiveSkill
{
    public override void Activate(GameObject user)
    {
        PlayerWeapon weapon = user.GetComponent<PlayerWeapon>();
        if (weapon != null)
        {
            weapon.StartCoroutine(weapon.SwitchToBombArrow(duration));
        }
    }
}
