using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    Transform parent;
    AnimationHandler animationHandler;
    PlayerController playerController;
    
    private void Awake()
    {
        parent = transform.parent;
        playerController = GetComponentInParent<PlayerController>();
        animationHandler = GetComponentInParent<AnimationHandler>();
    }
}
