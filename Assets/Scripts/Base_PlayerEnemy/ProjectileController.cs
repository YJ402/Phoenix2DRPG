using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeStatHandler rangeStatHandler;

    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestory = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        //if (currentDuration > rangeStatHandler.Duration)  // Duration ���� ����̱淹 �ϴ� �ּ�ȭ
        //{
        //    DestroyProjectile(transform.position, false);
        //}

        _rigidbody.velocity = direction * rangeStatHandler.BulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        else if (rangeStatHandler.target.value == (rangeStatHandler.target.value | (1 << collision.gameObject.layer))) // target���� �޾ƿ;���. 
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-rangeStatHandler.AttackPower);
                Debug.Log($"ü�� ����: {-rangeStatHandler.AttackPower} \n �ܿ� ü��: {resourceController.CurrentHealth}");
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
        //transform.localScale = Vector3.one * rangeStatHandler.BulletSize;  // BulletSize ���� ����̱淹 �ϴ� �ּ�ȭ
        //spriteRenderer.color = rangeStatHandler.ProjectileColor; //  ���� ����̱淹 �ϴ� �ּ�ȭ

        transform.right = this.direction;

        if (this.direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
}
