using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationHandler : AnimationHandler
{
    private static int attackSpoke1 = Animator.StringToHash("AttackSpoke1");
    private static int attackSpoke2 = Animator.StringToHash("AttackSpoke2");
    private static int attackSpoke3 = Animator.StringToHash("AttackSpoke3");

    List<int> attackSpokeList = new List<int>() { attackSpoke1, attackSpoke2, attackSpoke3 };



    public override void Attack(bool isattack) // attack º¯¼ö
    {
        base.Attack(isattack);
        int rand = Random.Range(0, attackSpokeList.Count);
        AttackSpoke(attackSpokeList[rand]);
    }
    private void AttackSpoke(int attackSpoke)
    {
        animator.SetTrigger(attackSpoke);
    }
}
