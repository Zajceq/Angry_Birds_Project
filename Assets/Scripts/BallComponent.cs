using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    public float Speed = 1.0f;
    public float RotationSpeed = 10.0f;
    private Vector3 vecRotation = Vector3.zero;

    void Update()
    {
        //Debug.Log("Frames per second: " + 1/Time.deltaTime);
        transform.position += Vector3.up * Time.deltaTime * Speed;
        vecRotation += Vector3.forward * RotationSpeed;
        transform.rotation = Quaternion.Euler(vecRotation);
        if (transform.localScale.x < 3)
        {
             transform.localScale += Vector3.one * Time.deltaTime * Speed;
        }
        else
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
    }
}
