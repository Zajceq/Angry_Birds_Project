using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private BallComponent followTarget;
    private Vector3 originalPosition;
    private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        followTarget = FindObjectOfType<BallComponent>();
        originalPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (!followTarget.IsSimulated())
        {
            return;
        }
        transform.position = Vector3.SmoothDamp(transform.position, originalPosition + followTarget.transform.position, ref velocity, smoothTime, Mathf.Infinity, Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            transform.position = originalPosition;
        }
    }
}
