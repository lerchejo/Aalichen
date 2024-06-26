using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    
        // Clamp the x and y coordinates of the smoothedPosition
        float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);
    
        // Assign the clamped position to the camera's position
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}