using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cameraTarget;

    private void Start()
    {
        if (cameraTarget == null)
            return;

    }
    private void Update()
    {
        if (cameraTarget == null)
            return;

        transform.position = new Vector3(cameraTarget.position.x, cameraTarget.position.y, transform.position.z);
    }
}
