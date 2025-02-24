using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    Transform parent;
    Animator animator;
    PlayerController playerController;
    
    private void Awake()
    {
        parent = transform.parent;
        playerController = GetComponentInParent<PlayerController>();
        animator = parent.GetComponentInChildren<Animator>();
    }
}
