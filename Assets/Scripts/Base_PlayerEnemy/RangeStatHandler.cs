using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangeStatHandler : StatHandler
{
    [SerializeField] private Transform projectileSpawnPosition;
    ProjectileManager projectileManager;

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


    [SerializeField] private float multipleProjectilesAngel;
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } }
    [SerializeField] private float spread;
    public float Spread { get { return spread; } }

    protected void Start()
    {
        projectileManager = ProjectileManager.Instance;
    }

    
    public override void Shoot(Vector2 _lookDirection)
    {
        
        float projectilesAngleSpace = multipleProjectilesAngel;
        int numberOfProjectilesPerShot = BulletCount;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngel;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(_lookDirection, angle);
        }



    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle));
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
