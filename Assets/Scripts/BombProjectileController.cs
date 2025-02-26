using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeStatHandler rangeStatHandler;

    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;
    private Transform boomArea;

    private Rigidbody2D _rigidbody;



    public bool fxOnDestory = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
        boomArea = transform.GetChild(1);
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        _rigidbody.velocity = direction * rangeStatHandler.BulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        else if (rangeStatHandler.target.value == (rangeStatHandler.target.value | (1 << collision.gameObject.layer))) // target정보 받아와야함. 
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-rangeStatHandler.AttackPower);
                Debug.Log($"체력 변경: {-rangeStatHandler.AttackPower} \n 잔여 체력: {resourceController.CurrentHealth}");
                if (rangeStatHandler.IsOnKnockback)
                {
                    BaseController controller = collision.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeStatHandler.KnockbackPower, rangeStatHandler.KnockbackTime);
                    }
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }




    public void Init(Vector2 direction, RangeStatHandler _rangeStatHandler)
    {
        rangeStatHandler = _rangeStatHandler;

        this.direction = direction;
        currentDuration = 0;
        //transform.localScale = Vector3.one * rangeStatHandler.BulletSize;  // BulletSize 변수 지우셨길레 일단 주석화
        //spriteRenderer.color = rangeStatHandler.ProjectileColor; //  변수 지우셨길레 일단 주석화

        transform.right = this.direction;

        if (this.direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject,0.1f);
    }
}
