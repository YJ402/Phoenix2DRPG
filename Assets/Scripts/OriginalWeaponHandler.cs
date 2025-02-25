using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalWeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    //[SerializeField] private float delay = 1f;                                공격속도 조절속성, 애니메이션 속도조절 방식으로 변경, 따라서 안씀
    //public float Delay { get => delay; set => delay = value; }                >>>><<<<<<<

    [SerializeField] private float weaponSize = 1f;                             //사이즈 변경을 하려나?
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }                                //
                                                                                                             //
    [SerializeField] private float power = 1f;                                                               //
    public float Power { get => power; set => power = value; }                                               //
                                                                                                             //
    [SerializeField] private float speed = 1f;                                                               //
    public float Speed { get => speed; set => speed = value; }                                               //
                                                                                                             //
    [SerializeField] private float attackRange = 10f;                                                        //
    public float AttackRange { get => attackRange; set => attackRange = value; }                             //
                                                                                                             //
    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    public BaseController Controller { get; private set; }

    private Animator animator;
    private SpriteRenderer weaponRenderer;

    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
