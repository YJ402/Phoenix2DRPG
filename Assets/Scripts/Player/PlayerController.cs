using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform rangeCircle;//임시 사정거리 가시원
    
    Rigidbody2D _rigidbody;
    [SerializeField] SpriteRenderer characterRenderer;
    [SerializeField] Transform targetPointer;
    [SerializeField] Transform enemys;
    [SerializeField] Transform targetTransform;
    AnimationHandler animationHandler;
    RangeStatHandler rangeStatHandler;
    
    

    float targetDistance;

    Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    [SerializeField] public Slider hpSlider;
    private Image barImage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        rangeStatHandler = GetComponent<RangeStatHandler>();
        characterRenderer = GetComponentInChildren<SpriteRenderer>(true);
        barImage = hpSlider.fillRect.GetComponent<Image>();
    }

    private void Start()
    {
        rangeCircle.transform.localScale = new Vector3(2* rangeStatHandler.AttackRange, 2* rangeStatHandler.AttackRange); // 임시로 생성한 사정거리 원 크기
        PlayerData.Instance.ApplyPassiveSkill();
    }

    private void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }
    private void FixedUpdate()
    {
        Movment(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }
    private void Movment(Vector2 direction)
    {
        direction = direction * rangeStatHandler.Speed;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
        
        UpdateHpSliderPosition();
    }
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;
        if (targetPointer != null)
        {
            targetPointer.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        characterRenderer.flipX = isLeft;
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }
    private void HandleAction()
    {
        movementDirection= new Vector2 (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;

        
        foreach(Transform enemyTransform in enemys)
        {
            if(targetTransform == null || Vector3.Distance(transform.position, targetTransform.position) > Vector3.Distance(transform.position, enemyTransform.position))
            {
                targetTransform = enemyTransform;
            }
        }

        if (targetTransform != null)
            targetDistance = Vector3.Distance(transform.position, targetTransform.position);

        lookDirection = (targetTransform.position - transform.position).normalized;

        animationHandler.Attack(targetDistance < rangeStatHandler.AttackRange);
    }

    public void Fire()
    {
        rangeStatHandler.Shoot(LookDirection);
    }

    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
    //=================================================================
    // 캐릭터 체력바 UI관련

    public void UpdateHpSlider(float percentage)
    {
        hpSlider.value = percentage;
        if (percentage == 0)
            barImage.color = Color.clear;
        else if (percentage < 0.2)
            barImage.color = Color.red;
        else if (percentage < 0.5)
            barImage.color = Color.yellow;
        else
            barImage.color = Color.green;
    }
    public void UpdateHpSliderPosition()
    {
        Vector2 ScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        hpSlider.transform.position = new Vector3(ScreenPosition.x, ScreenPosition.y-32,hpSlider.transform.position.z);
    }
}
