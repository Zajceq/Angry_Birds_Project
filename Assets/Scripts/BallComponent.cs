using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    void Update()
    {
         Debug.Log("Frames per second: " + 1/Time.deltaTime);
    }
}
