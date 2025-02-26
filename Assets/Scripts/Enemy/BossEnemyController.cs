using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyController
{
    protected override void Start()
    {
         // 지워도 무방
    }

    protected override void Attack(bool isAttack)
    {
        base.Attack(isAttack); // Attack 애니메이션 트리거 // 추가 로직 작성 안할거면 지워도 무방.
    }

    public void Attack1()
    {
        //몹 소환
    }

    public void Attack2()
    {
        //전체 공격
    }

    public void Attack3()
    {
        //순간 이동
    }


    //순간이동

    //공격은 어디서 실행해?
    //애니메이션은 어디서 실행해? =>BaseController.Attack에서 애니메이션만 실행시키고. 애니메이션의 이벤트로 공격을 가함.

    //언제 할것인가?

}
