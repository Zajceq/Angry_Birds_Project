using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BeachBallLevitate : MonoBehaviour
{
    private Vector3 m_startPosition;

    private float m_curYPos = 0.0f;
    private float m_curZRot = 0.0f;
    private float m_curScale = 0.0f;

    public float Amplitude = 0.5f;
    public float RotationSpeed = 50;
    public float FinalScale = 0.5f;
    private bool isAnimating;
    private float timeInAnimation;
    private float animationSpeed = 0.5f;

    void Start()
    {
        m_startPosition = transform.position;
        timeInAnimation = 0f;
        isAnimating = true;
        //StartCoroutine(LevitateBeachBallCoroutine());
        LevitateBeachBallAsync();
    }

    private void LevitateBeachBall()
    {
        timeInAnimation += Time.deltaTime * animationSpeed;
        m_curYPos = Mathf.Abs(Mathf.Sin(Mathf.Lerp(0, Mathf.PI, timeInAnimation) * Amplitude));
        transform.position = new Vector3(m_startPosition.x,
                                            m_startPosition.y + m_curYPos,
                                            m_startPosition.z);
        m_curZRot += Time.deltaTime * RotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, m_curZRot);
        m_curScale = -Mathf.Abs(Mathf.Sin(Mathf.Lerp(0, Mathf.PI, timeInAnimation) * FinalScale));
        transform.localScale = Vector3.one * m_curScale;
    }

    //IEnumerator LevitateBeachBallCoroutine()
    //{
    //    while (true)
    //    {
    //        if (isAnimating)
    //        {
    //            yield return null;
    //            LevitateBeachBall();
    //            if (IsAnimationCompleted())
    //            {
    //                isAnimating = false;
    //            }
    //        }
    //        else
    //        {
    //            timeInAnimation = 0f;
    //            yield return new WaitForSeconds(1);
    //            isAnimating = true;
    //        }
    //    }
    //}

    async void LevitateBeachBallAsync()
    {
        while (true)
        {
            try
            {
                if (isAnimating)
                {
                    await Task.Yield();
                    LevitateBeachBall();
                    if (IsAnimationCompleted())
                    {
                        isAnimating = false;
                    }
                }
                else
                {
                    timeInAnimation = 0f;
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    isAnimating = true;
                }
            }
            catch (TaskCanceledException)
            {
                break;
            }
            catch (MissingReferenceException)
            {
                break;
            }
        }
    }

    private bool IsAnimationCompleted()
    {
        return (transform.position.y == m_startPosition.y);
    }

    //private void OnDestroy()
    //{
    //    StopAllCoroutines();
    //}
}
