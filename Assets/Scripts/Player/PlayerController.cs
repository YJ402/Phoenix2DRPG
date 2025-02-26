using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    public Transform rangeCircle;//�ӽ� �����Ÿ� ���ÿ�
    [SerializeField] Transform targetPointer;
    [SerializeField] Transform enemys;
    [SerializeField] Transform targetTransform;
    RangeStatHandler rangeStatHandler;
    
    float targetDistance;

    [SerializeField] public Slider hpSlider;
    private Image barImage;

    protected override void Awake()
    {
        base.Awake();
        rangeStatHandler = GetComponent<RangeStatHandler>();
        barImage = hpSlider.fillRect.GetComponent<Image>();
    }

    protected override void Start()
    {
        base.Start();
        rangeCircle.transform.localScale = new Vector3(2* rangeStatHandler.AttackRange, 2* rangeStatHandler.AttackRange); // �ӽ÷� ������ �����Ÿ� �� ũ��
        PlayerData.Instance.ApplyPassiveSkill();
        
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void Movment(Vector2 direction)
    {
        base.Movment(direction);
        UpdateHpSliderPosition();
    }
    protected override void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentisLeft = Mathf.Abs(rotZ) > 90f;
        if (targetPointer != null)
        {
            targetPointer.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        characterRenderer.flipX = currentisLeft;
    }
    protected override void HandleAction()
    {
        movementDirection= new Vector2 (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;

        targetTransform = null;
        foreach (Transform enemyTransform in enemys)
        {
            BaseController enemybasecontroller = enemyTransform.GetComponent<BaseController>();

            if ((targetTransform == null || Vector3.Distance(transform.position, targetTransform.position) > Vector3.Distance(transform.position, enemyTransform.position)) && !enemybasecontroller.IsDead)
            {
                targetTransform = enemyTransform;
            }
        }


        if (targetTransform != null)
        {
            targetDistance = Vector3.Distance(transform.position, targetTransform.position);
            lookDirection = (targetTransform.position - transform.position).normalized;
        }


        animationHandler.Attack((targetDistance < rangeStatHandler.AttackRange) && targetTransform != null);
    }


    //=================================================================
    // ĳ���� ü�¹� UI����
    
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
