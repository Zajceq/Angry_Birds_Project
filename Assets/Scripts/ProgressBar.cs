using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float CompleteSpeed = 0.2f;
    private float timeCounter;
    public Slider Slider;

    private void Start()
    {
        timeCounter = 0.0f;
        Slider.value = 0.0f;
    }

    private void Update()
    {
        FillLoadingBar();
    }

    private void FillLoadingBar()
    {
        timeCounter += Time.deltaTime * CompleteSpeed;
        Slider.value = Mathf.Lerp(0, 1, timeCounter);
    }
}
