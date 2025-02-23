using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D characterRigidbody;
    [SerializeField] protected SpriteRenderer characterSprite;
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    protected virtual void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterSprite =GetComponentInChildren<SpriteRenderer>();

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
        movementDirection = MovementDirection * 5/*player speed State*/;

        characterRigidbody.velocity = movementDirection;
    }

    protected virtual void Rotate(Vector2 LookDirection)
    {
        bool isLeft = Mathf.Abs(Mathf.Atan2(LookDirection.y, LookDirection.x)) > (Mathf.PI / 2);
        
        characterSprite.flipX = isLeft;
    }

}
