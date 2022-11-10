using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallComponent : MonoBehaviour
{
    public float PositionSpeed = 1.0f;
    public float ScaleSpeed = 0.3f;
    public float RotationSpeed = 10.0f;
    private Vector3 vecRotation = Vector3.zero;
    private float timeElapsed;
    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;
    private float maxFPS = 0f;
    private float minFPS = 0f;
    [SerializeField] private TextMeshProUGUI avgFPSText;
    [SerializeField] private TextMeshProUGUI maxFPSText;
    [SerializeField] private TextMeshProUGUI minFPSText;

    void Awake()
    {
        frameDeltaTimeArray = new float[50]; 
    }
    void Update()
    {
        //FPS
        //Debug.Log("Frames per second: " + 1 / Time.unscaledDeltaTime);
        frameDeltaTimeArray[lastFrameIndex] = Time.unscaledDeltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
        avgFPSText.text = "AVG FPS: " + Mathf.RoundToInt(CalculateAvgFPS()).ToString();
        maxFPSText.text = "MAX FPS: " + Mathf.RoundToInt(CalculateMaxFPS()).ToString();
        minFPSText.text = "MIN FPS: " + Mathf.RoundToInt(CalculateMinFPS()).ToString();

        //Changing transform of the ball
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
        //transform.localScale = new Vector3(Mathf.Lerp(1f, 3f, Mathf.Clamp01(timeElapsed)), Mathf.Lerp(1f, 3f, Mathf.Clamp01(timeElapsed)), Mathf.Lerp(1f, 3f, Mathf.Clamp01(timeElapsed)));
        transform.localScale = Vector3.one * (Mathf.Lerp(1f, 3f, Mathf.Clamp01(timeElapsed)));
    }

    private float CalculateAvgFPS()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length / total;
    }

    private float CalculateMaxFPS()
    {
        float currentFPS = 1 / Time.deltaTime;
        //float maxFPS = 0f;
        maxFPS = Mathf.Max(maxFPS, currentFPS);
        return maxFPS;
    }
    private float CalculateMinFPS()
    {
        float currentFPS = 1 / Time.deltaTime;
        //float minFPS = 0f;
        minFPS = Mathf.Min(minFPS, currentFPS);
        return minFPS;
    }
}
