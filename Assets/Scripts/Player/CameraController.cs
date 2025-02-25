using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cameraTarget;
    [SerializeField] float maxPositionX;
    [SerializeField] float maxPositionY;
    private void Start()
    {
        if (cameraTarget == null)
            return;
    }
    private void Update()
    {
        if (cameraTarget == null)
            return;

        transform.position = new Vector3(Mathf.Abs(cameraTarget.transform.position.x)>maxPositionX?(cameraTarget.transform.position.x > 0?maxPositionX:-maxPositionX): cameraTarget.transform.position.x, Mathf.Abs(cameraTarget.transform.position.y) > maxPositionY ? (cameraTarget.transform.position.y > 0 ? maxPositionY : -maxPositionY) : cameraTarget.transform.position.y,transform.position.z);
    }
}
