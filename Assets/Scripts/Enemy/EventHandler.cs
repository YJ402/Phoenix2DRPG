using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler: MonoBehaviour
{
    BaseController baseController;

    private void Awake()
    {
        baseController = GetComponentInParent<BaseController>();
    }
    private void Fire()
    {
        baseController.Fire();
    }

    private void CheckMeleeAttackSuccess()
    {
        baseController.CheckHit();
    }
}
