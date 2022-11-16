using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private BallComponent followTarget;
    private Vector3 originalPosition;

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
        transform.position = Vector3.MoveTowards(transform.position, originalPosition + followTarget.transform.position, followTarget.GetBallSpeed() * Time.fixedDeltaTime);
    }
}
