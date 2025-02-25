using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangeStatHandler : StatHandler
{
    AnimationHandler animationHandler;


    //�Ӽ���  ü��,�ִ�ü��,�̵��ӵ�,���ݷ�,���ݼӵ�,�����Ÿ�,����ü����,����ü�ӵ�



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
}
