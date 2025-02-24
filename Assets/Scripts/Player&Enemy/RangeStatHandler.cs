using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangeStatHandler : StatHandler
{
    AnimationHandler animationHandler;


    //속성들  체력,최대체력,이동속도,공격력,공격속도,사정거리,투사체종류,투사체속도



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
