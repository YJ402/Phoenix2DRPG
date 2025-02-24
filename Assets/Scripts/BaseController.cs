using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D characterRigidbody;
    [SerializeField] protected SpriteRenderer characterSprite;
    protected AnimationHandler animationHandler;
    protected StateHandler stateHandler;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }
    [SerializeField] protected Transform weapon; 


    
    protected virtual void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterSprite =GetComponentInChildren<SpriteRenderer>();
        stateHandler = GetComponent<StateHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }
    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
        HandleAction();
        Rotate(LookDirection);
    }

    protected virtual void FixedUpdate()
    {
        MoveCharacter(movementDirection);
    }

    protected virtual void HandleAction()
    {

    }
    protected virtual void MoveCharacter(Vector2 MovementDirection)
    {
        movementDirection = MovementDirection * stateHandler.Speed;

        characterRigidbody.velocity = movementDirection;

        animationHandler.Move(movementDirection);
    }

    protected virtual void Rotate(Vector2 LookDirection)
    {
        float degree = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(degree) > 90f;
        
        characterSprite.flipX = isLeft;

        if(weapon != null)
        {
            weapon.rotation = Quaternion.Euler(0, 0, degree);
        }
    }

}
