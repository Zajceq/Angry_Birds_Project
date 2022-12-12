using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float TimeToComplete = 3.0f;
    private float timeCounter;
    public Slider Slider;

    private void Start()
    {
        timeCounter = 0.0f;
        Slider.value = 0.0f;
    }

    private void Update()
    {
        timeCounter += Time.deltaTime / TimeToComplete;
        FillLoadingBar();
    }

    private void FillLoadingBar()
    {
        Slider.value = Mathf.Lerp(0, 1, timeCounter);
    }
}
