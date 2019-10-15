using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// The target to follow.
    /// </summary>
    [SerializeField]
    private Transform target;

    /// <summary>
    /// How quickly to snap to the target's position.
    /// </summary>
    [SerializeField]
    private float smoothSpeed = 10f;

    /// <summary>
    /// Where to place the camera relative to the target.
    /// </summary>
    [SerializeField]
    private Vector3 offset;

    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.fixedDeltaTime);
        //transform.position = smoothedPosition;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(rb.position, desiredPosition, ref velocity, smoothSpeed * Time.fixedDeltaTime);
        rb.position = smoothedPosition;
    }
}
