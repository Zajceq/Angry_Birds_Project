using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private BallComponent followTarget;
    private Vector3 originalPosition;
    private float smoothTime = 0.25f;
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
        //transform.position = Vector3.MoveTowards(transform.position, originalPosition + followTarget.transform.position, followTarget.GetBallSpeed() * Time.fixedDeltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, originalPosition + followTarget.transform.position, ref velocity, smoothTime * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            transform.position = originalPosition;
        }
    }
}
