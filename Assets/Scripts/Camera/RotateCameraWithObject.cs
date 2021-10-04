using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RotateCameraWithObject : MonoBehaviour
{
    public Transform TargetPositionTransform;
    public Transform TargetRotationTransform;

    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        UpdatePositionAndRotation();

        // zoom the camera in more as the rotation increases
        float zRotationAsFloat = transform.rotation.eulerAngles.z;
        if(zRotationAsFloat > 180)
        {
            zRotationAsFloat = Mathf.Abs(zRotationAsFloat - 360);
        }
        camera.orthographicSize = 8 - Mathf.Clamp(zRotationAsFloat * zRotationAsFloat / 900.0f, 0, 1);
    }

    private void UpdatePositionAndRotation()
    {
        // copy the position and rotation of the target
        transform.position = TargetPositionTransform.position + Vector3.up*0.3f + Vector3.back * 100;
        transform.rotation = TargetRotationTransform.rotation;
    }
}
