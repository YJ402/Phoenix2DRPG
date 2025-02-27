using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectileController : ProjectileController
{
    private Transform boomArea;

    protected override void Awake()
    {
        base.Awake();
        boomArea = transform.GetChild(1);
    }

    protected override  void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            _rigidbody.velocity = Vector3.zero;
            spriteRenderer.gameObject.SetActive(false);
            boomArea.gameObject.SetActive(true);
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        else if (rangeStatHandler.target.value == (rangeStatHandler.target.value | (1 << collision.gameObject.layer))) // target정보 받아와야함. 
        {
            spriteRenderer.gameObject.SetActive(false);
            _rigidbody.velocity = Vector3.zero;
            boomArea.gameObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + 0.3f, transform.position.y), 1.5f);

            foreach (Collider2D collider in colliders)
            {
                ResourceController resourceController = collider.GetComponent<ResourceController>();
                if (resourceController != null&& rangeStatHandler.target.value == (rangeStatHandler.target.value | (1 << collider.gameObject.layer)))
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
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
        
    }





    protected override void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject,0.3f);
    }
}
