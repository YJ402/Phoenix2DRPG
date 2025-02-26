using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    AnimationHandler animationHandler;
    ResourceController resourceController;
    
    //========================================================================================  �˹� ���� �Ӽ�
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }
    //=========================================================================================
    
    //�Ӽ���  �ִ�ü��,�̵��ӵ�,���ݷ�,���ݼӵ�,�����Ÿ�,����ü����,����ü�ӵ�

    //[SerializeField] private int health = 1000;       ResourceController�� �̰���
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
            //if (delta > 0)                                Health�� ResourceController �� �̰��Ͽ� ����
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
    [SerializeField] private float speed;    //�÷��̾� �̵��ӵ�   �⺻ 5,   (10 = 2��)
    public float Speed
    {
        get { return speed; }
        set 
        { 
            speed = value;
            //animationHandler.ChangeMovingSpeed(Speed);    �ν����� â���� �����ϸ� ȿ����� �ӽ÷� update�� �Űܵ�
        }
    }

    [SerializeField] private int attackPower;
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }

    [SerializeField] private float attackSpeed;   //���ݼӵ�  �⺻ 1.0f    (2.0f = �ӵ� 2��)
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            attackSpeed = value;
            //animationHandler.ChangeAttackSpeed(AttackSpeed);      �ν����� â���� �����ϸ� ȿ����� �ӽ÷� update�� �Űܵ�   
        }
    }

    [SerializeField] private float attackDistance;  //�����Ÿ�  �� �������� �־�� �ν��ϰ� ������
    public float AttackRange
    {
        get { return attackDistance; }
        set { attackDistance = value; }
    }

    public LayerMask target;

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
        // ===> �ν����ͷ� �����Ͻø� �ɵ�
    }
    private void Update()                                  //�ִϸ��̼� �ӵ������� �ӽ� �޼���
    {                                                      //                                                           
        animationHandler.ChangeMovingSpeed(Speed);         //                                                           
        animationHandler.ChangeAttackSpeed(AttackSpeed);   //                                                           
    }                                                      //                                                           
}                                                          //                                                           
                                                                                                                      