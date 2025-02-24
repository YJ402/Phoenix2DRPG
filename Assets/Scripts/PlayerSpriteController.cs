using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }
    private void Fire()
    {
        playerController.Fire();
    }
}
