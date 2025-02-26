using Unity.VisualScripting;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] protected SpriteRenderer characterRenderer;
    //[SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;


    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;

    [HideInInspector] public bool isAttacking;
    private bool isPlayer;
    private float timeSinceLastAttack = float.MaxValue;

    protected bool currentisLeft;
    bool previsLeft;
    private bool isDead = false;
    public bool IsDead { get { return isDead; } private set { isDead = value; } }
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();

        //if(!TryGetComponent<RangeStatHandler>(out rangeStatHandler))
        //{
        //    Debug.Log("�� ������ �ٰŸ� �����Դϴ�.");
        //}
    }

    protected virtual void Start()
    {
        isPlayer = (transform.gameObject.layer == 6) ? true : false;
    }

    protected virtual void Update()
    {
        if (isPlayer | !isAttacking) // ���ݽ� ��� ����
        {
            HandleAction();
            Rotate(lookDirection);
            HandleAttackDelay();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (isPlayer | !isAttacking)
        {
            Movment(movementDirection);
            if (knockbackDuration > 0.0f)
            {
                knockbackDuration -= Time.fixedDeltaTime;
            }
        }
    }

    protected virtual void HandleAction()
    {

    }

    protected virtual void Movment(Vector2 direction)
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

    protected virtual void Rotate(Vector2 direction)
    {

    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {
        //���� ���� ���� > ��ü ���� �������ҵ�.
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
            animationHandler.Attack(isAttacking); // �׳� �ִϸ��̼Ǹ� Ʈ�����ϰ� �ִϸ��̼ǿ��� attack ���� �˻� �޼���.

        //���� Ŭ����(�÷��̾�, ������, ���Ÿ���)���� ����)
    }

    public virtual void Death()
    {
        IsDead = true;
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

    public void Fire()
    {
        Debug.Log("�߻�");
        statHandler.Shoot(lookDirection);
    }

    public virtual void CheckHit()
    {
    }
}
