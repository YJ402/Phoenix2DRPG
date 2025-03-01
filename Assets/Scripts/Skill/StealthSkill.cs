using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthSkill : ActiveSkill
{
    public string stealthLayer="Stealth";

    private int originalLayer;
    private GameObject player;

    private void Start()
    {
        skillName = "����ȭ";
    }

    public override void Activate(GameObject user)
    {
        player = user;
        originalLayer = player.layer;
        StartCoroutine(DoStealth());
    }
    private IEnumerator DoStealth()
    {
        int stealthLayerIndex = LayerMask.NameToLayer(stealthLayer);
        player.layer = stealthLayerIndex;

        yield return new WaitForSeconds(duration);

        player.layer=originalLayer;
    }
}
