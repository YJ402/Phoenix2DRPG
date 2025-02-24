using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class BulletDoubleSkill : BaseSkill
//{
//    public float bulletMultiplier = 2f;

//    public override void Activate(GameObject user)
//    {
//        PlayerStatus playerStatus = user.GetComponent<PlayerStatus>();
//        if (playerStatus != null)
//        {
//            playerStatus.StartCoroutine(ApplyBulletDouble(playerStatus));
//        }
//    }
//    private IEnumerable ApplyBulletDouble(PlayerStatus playerStatus)
//    {
//        int originalBulletCount = PlayerStatus.bulletCount;
//        playerStatus.bulletCount = Mathf.RoundToInt(originalBulletCount * bulletMultiplier);
//        yield return new WaitForSeconds(duration);

//        playerStatus.bulletCount = originalBulletCount;
//    }
//}

