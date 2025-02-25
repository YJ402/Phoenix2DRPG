using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalBaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected AnimationHandler animationHandler;

    protected StatHandler statHandler;

    //[SerializeField] public WeaponHandler WeaponPrefab;       무기 장착 안함
    //protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    //private float timeSinceLastAttack = float.MaxValue;       애니메이션 스피드로 공격속도 조절

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();

        //if (WeaponPrefab != null)                                             무기장착안함
        //    weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        //else
        //    weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        //HandleAttackDelay();                          여기서 공격속도조절 안함
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction)
    {
        direction = direction * statHandler.Speed;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;

        //if (weaponPivot != null)                                      무기착용안하지만 타겟 화살표 표시 기능에 사용, 이름변경 필요
        //{
        //    weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        //}

        //weaponHandler?.Rotate(isLeft);                                무기착용안함
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    //private void HandleAttackDelay()                                    공격속도조절 메서드, 애니메이션속도로 조절, 사용안함
    //{
    //    if (weaponHandler == null)
    //        return;

    //    if (timeSinceLastAttack <= weaponHandler.Delay)
    //    {
    //        timeSinceLastAttack += Time.deltaTime;
    //    }

    //    if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
    //    {
    //        timeSinceLastAttack = 0;
    //        Attack();
    //    }
    //}

    //protected virtual void Attack()                                       공격 메서드,  애니메이션 내부에서 공격호출하므로 필요없음
    //{
    //    if (lookDirection != Vector2.zero)
    //        weaponHandler?.Attack();
    //}

    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
