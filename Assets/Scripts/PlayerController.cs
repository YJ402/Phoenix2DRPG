using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerController : BaseController
{
    [SerializeField] Transform enemys;
    [SerializeField] Transform targetTransform;
    float targetDistance;

    

    protected override void Awake()
    {
        base.Awake();
        targetDistance = float.MaxValue;
    }
    protected override void HandleAction()
    {

        base.HandleAction();
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

        animationHandler.Attack(targetDistance < stateHandler.AttackRange);
    }
    public void Fire()
    {

    }
}
