using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    AnimationHandler animationHandler;
    ResourceController resourceController;

    //속성들  최대체력,이동속도,공격력,공격속도,사정거리,투사체종류,투사체속도

    //[SerializeField] private int health = 1000;       ResourceController로 이관됨
    //public int Health
    //{
    //    get { return health; }
    //    set
    //    {
    //        if(value > maxHealth)
    //        {
    //            health = maxHealth;
    //        }
    //    }
    //}
    [SerializeField] private int maxHealth = 1000;
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            int delta = value - maxHealth;
            //if (delta > 0)                                Health를 ResourceController 로 이관하여 변경
            //    Health += delta;
            //else Health = Health;
            if (delta > 0)
                resourceController.ChangeHealth(delta);
            else resourceController.ChangeHealth(0);

            if (value <= 0)
                maxHealth = 1;
            if (value > 9999)
                maxHealth = 9999;
        }
    }
    [SerializeField] private float speed;    //플레이어 이동속도   기본 5,   (10 = 2배)
    public float Speed
    {
        get { return speed; }
        set 
        { 
            speed = value;
            animationHandler.ChangeMovingSpeed(Speed/5);
        }
    }

    [SerializeField] private int attackPower;
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }

    [SerializeField] private float attackSpeed;   //공격속도  기본 1.0f    (2.0f = 속도 2배)
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            attackSpeed = value;
            animationHandler.ChangeAttackSpeed(AttackSpeed);
        }
    }

    [SerializeField] private float attackDistance;  //사정거리  이 범위내에 있어야 인식하고 공격함
    public float AttackRange
    {
        get { return attackDistance; }
        set { attackDistance = value; }
    }

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        resourceController = GetComponent<ResourceController>();     
        //if (Speed == 0f)
        //    Speed = 5;
        //if(AttackSpeed == 0f)
        //    AttackSpeed = 1;
        //if (MaxHealth == 0)
        //    MaxHealth = 1000;
        //if (AttackRange == 0)
        //    AttackRange = 5;
        //if (BulletCount == 0)
        //    BulletCount = 1; 
        // ===> 인스펙터로 조절하시면 될듯
    }
}
