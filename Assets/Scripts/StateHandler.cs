using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    AnimationHandler animationHandler;
    

    //속성들  체력,최대체력,이동속도,공격력,공격속도,사정거리,투사체종류,투사체속도

    [SerializeField] private int health = 1000;
    public int Health
    {
        get { return health; }
        set
        {
            if(value > maxHealth)
            {
                health = maxHealth;
            }
        }
    }
    [SerializeField] private int maxHealth = 1000;
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            int delta = value - maxHealth;
            if (delta > 0)
                Health += delta;
            else Health = Health;

            if (value <= 0)
                maxHealth = 1;
            if (value > 9999)
                maxHealth = 9999;
        }
    }
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        set 
        { 
            speed = value;
            animationHandler.ChangeMovingSpeed(Speed/5);
        }
    }
    [SerializeField] private int attack;
    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }
    [SerializeField] private float attackSpeed;
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            attackSpeed = value;
            animationHandler.ChangeAttackSpeed(AttackSpeed);
        }
    }
    [SerializeField] private float attackDistance;
    public float AttackRange
    {
        get { return attackDistance; }
        set { attackDistance = value; }
    }

    [SerializeField] private int bulletIndex;
    public int BulletIndex
    {
        get { return bulletIndex; }
        set { bulletIndex = value; }
    }
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }
    [SerializeField] private int bulletCount;
    public int BulletCount
    {
        get { return bulletCount; }
        set { bulletCount = value; }
    }
    

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        if (Speed == 0f)
            Speed = 5;
        if(AttackSpeed == 0f)
            AttackSpeed = 1;
        if (MaxHealth == 0)
            MaxHealth = 1000;
        if (AttackRange == 0)
            AttackRange = 5;
        if (BulletCount == 0)
            BulletCount = 1;
    }
}
