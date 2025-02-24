using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    AnimationHandler animationHandler;
    

    //�Ӽ���  ü��,�ִ�ü��,�̵��ӵ�,���ݷ�,���ݼӵ�,�����Ÿ�,����ü����,����ü�ӵ�

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
    [SerializeField] private float speed;    //�÷��̾� �̵��ӵ�   �⺻ 5,   (10 = 2��)
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
    [SerializeField] private float attackSpeed;   //���ݼӵ�  �⺻ 1.0f    (2.0f = �ӵ� 2��)
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            attackSpeed = value;
            animationHandler.ChangeAttackSpeed(AttackSpeed);
        }
    }
    [SerializeField] private float attackDistance;  //�����Ÿ�  �� �������� �־�� �ν��ϰ� ������
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
