using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoundDoor : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            battleManager.GoNextRound();
    }
}
