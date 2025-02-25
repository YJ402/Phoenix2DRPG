using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalStatHandler : MonoBehaviour
{
    [Range(1, 9999)][SerializeField] private int health = 1000; //최대 100에서 9999, 기본 100에서 1000으로 변경
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 9999);
    }

    [Range(1f, 20f)][SerializeField] private float speed = 5;  //기본 3에서 5로 변경
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 40);
    }
    //{                                                             스피드에 따라 무빙 애니메이션 속도조절,  둘다 적용가능할듯함
    //    get { return speed; }
    //    set 
    //    { 
    //        speed = value;
    //        animationHandler.ChangeMovingSpeed(Speed/5);
    //    }
    //}
}
