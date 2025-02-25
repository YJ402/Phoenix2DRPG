using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    //[SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;


    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();

        //if(!TryGetComponent<RangeStatHandler>(out rangeStatHandler))
        //{
        //    Debug.Log("이 유닛은 근거리 유닛입니다.");
        //}
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();
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

        //if (weaponPivot != null)
        //{
        //    weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        //}

        //weaponHandler?.Rotate(isLeft);
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {
        //몬스터 웨폰 없음 > 자체 스텟 가져야할듯.
        //if (weaponHandler == null)
        //    return;

        //if (timeSinceLastAttack <= weaponHandler.Delay)
        //{
        //    timeSinceLastAttack += Time.deltaTime;
        //}

        //if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        //{
        //    timeSinceLastAttack = 0;
        //    Attack();
        //}
    }

    protected virtual void Attack(bool isAttack)
    {
        if (lookDirection != Vector2.zero)
            animationHandler.Attack(isAttacking); // 그냥 애니메이션만 트리거하고 애니메이션에서 attack 판정 검사 메서드.

        //하위 클래스(플레이어, 근접적, 원거리적)에서 구현)
    }
    private void CheckAttackSuccess() // 유니티 애니메이션에 이벤트로 추가.
    {
        //RaycastHit2D hit = Physics2D.BoxCast(transform.position.)
        //공격 성공 => 플레이어의 피격 판정 
    }

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

    public virtual void Fire()
    {
        //statHandler.shoot();
    }
}
