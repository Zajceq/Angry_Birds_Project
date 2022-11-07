using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    public float PositionSpeed = 1.0f;
    public float ScaleSpeed = 0.3f;
    public float RotationSpeed = 10.0f;
    private Vector3 vecRotation = Vector3.zero;
    private float timeElapsed;

    void Update()
    {
        Debug.Log("Frames per second: " + 1 / Time.deltaTime);
        transform.position += Vector3.up * Time.deltaTime * PositionSpeed;
        vecRotation += Vector3.forward * RotationSpeed;
        transform.rotation = Quaternion.Euler(vecRotation);
        //if (transform.localScale.x < 3)
        //{
        //     transform.localScale += Vector3.one * Time.deltaTime * Speed;
        //}
        //else
        //{
        //    transform.localScale = new Vector3(3, 3, 3);
        //}
        timeElapsed += Time.deltaTime * ScaleSpeed;
        transform.localScale = new Vector3(Mathf.Lerp(1f, 3f, Mathf.Clamp01(timeElapsed)), Mathf.Lerp(1f, 3f, Mathf.Clamp01(timeElapsed)), Mathf.Lerp(1f, 3f, Mathf.Clamp01(timeElapsed)));
    }
}
