using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    }

    public void UpdatePoints(int points)
    {
        PointsText.text = "Points: " + points;
    }
}
