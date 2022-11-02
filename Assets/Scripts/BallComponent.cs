using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Hello World!");
    }

    void Update()
    {
        Debug.Log("Time since last frame: " + Time.deltaTime);
    }
}
