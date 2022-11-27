using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HUDController : MonoBehaviour
{
    public Button PauseButton;
    public Button RestartButton;
    public TextMeshProUGUI PointsText;

    private void Start() 
    {
        PauseButton.onClick.AddListener(delegate 
        { 
            GameplayManager.Instance.PlayPause(); 
        }
        );

        RestartButton.onClick.AddListener(delegate
        {
            GameplayManager.Instance.Restart();
        }
        );

        GameplayManager.OnGamePaused += OnPause;
        GameplayManager.OnGamePlaying += OnPlaying;
    }

    private void OnPlaying()
    {
        PauseButton.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
    }

    private void OnPause()
    {
        PauseButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
    }

    public void UpdatePoints(int points)
    {
        PointsText.text = "Points: " + points;
    }
}
