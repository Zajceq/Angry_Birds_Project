using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Button ResumeButton;
    public Button RestartButton;
    public Button QuitButton;
    public GameObject Panel;

    public GameObject QuestionPopup;
    public Button YesButton;
    public Button NoButton;


    private void Start() 
    {
        ResumeButton.onClick.AddListener(delegate { OnResume(); });
        RestartButton.onClick.AddListener(delegate { OnRestart(); });
        QuitButton.onClick.AddListener(delegate { OnQuitFirst(); });
        YesButton.onClick.AddListener(delegate { OnQuitSecond(); });
        NoButton.onClick.AddListener(delegate { OnNotSure(); });

        SetPanelVisible(false);
        SetQuestionPopupVisible(false);

        GameplayManager.OnGamePaused += OnPause;
        GameplayManager.OnGamePlaying += OnPlaying;
    }

    public void SetPanelVisible(bool visible)
    {
        Panel.SetActive(visible);
    }

    private void SetQuestionPopupVisible(bool visible)
    {
        QuestionPopup.SetActive(visible);
    }
    
    private void OnPause()
    {
        SetPanelVisible(true);
    }

    private void OnResume()
    {
        GameplayManager.Instance.GameState = EGameState.Playing;
        SetPanelVisible(false);
    }

    private void OnQuitFirst()
    {
        SetQuestionPopupVisible(true);
    }

    private void OnQuitSecond()
    {
        Application.Quit();
    }

    private void OnNotSure()
    {
        SetQuestionPopupVisible(false);
    }

    private void OnRestart()
    {
        GameplayManager.Instance.Restart();
        SetPanelVisible(false);
    }

    private void OnPlaying()
    {
        SetPanelVisible(false);
        SetQuestionPopupVisible(false);
    }

}
