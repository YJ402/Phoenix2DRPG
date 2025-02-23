using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerController : BaseController
{
    Camera camera;
    Transform targetTransform;
    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
    }
    protected override void HandleAction()
    {
        base.HandleAction();
        movementDirection= new Vector2 (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;


        


    }
}
