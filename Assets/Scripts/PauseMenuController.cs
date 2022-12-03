using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Button ResumeButton;
    public Button RestartButton;
    public Button SaveButton;
    public Button LoadButton;
    public Button MenuButton;
    public GameObject Panel;

    public GameObject QuestionPopup;
    public Button YesButton;
    public Button NoButton;

    public GameObject MainMenu;
    public GameObject BackgroundMenu;


    private void Start() 
    {
        ResumeButton.onClick.AddListener(() => OnResume());
        RestartButton.onClick.AddListener(() => OnRestart());
        SaveButton.onClick.AddListener(() => OnSave());
        LoadButton.onClick.AddListener(() => OnLoad());
        MenuButton.onClick.AddListener(() => OnMenu());
        YesButton.onClick.AddListener(() => OnYesButton());
        NoButton.onClick.AddListener(() => OnNotSure());

        SetPanelVisible(false);
        SetQuestionPopupVisible(false);

        GameplayManager.OnGamePaused += OnPause;
        GameplayManager.OnGamePlaying += OnPlaying;
    }

    public void SetPanelVisible(bool visible)
    {
        Panel.SetActive(visible);
    }

    private void OnSave()
    {
        SaveManager.Instance.SaveSettings();
    }

    private void OnLoad()
    {
        SaveManager.Instance.LoadSettings();
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

    private void OnMenu()
    {
        SetQuestionPopupVisible(true);
    }

    private void OnYesButton()
    {
        SetPanelVisible(false);
        SetQuestionPopupVisible(false);
        SetMainMenuVisible(true);
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

    private void SetMainMenuVisible(bool visible)
    {
        MainMenu.SetActive(visible);
        BackgroundMenu.SetActive(visible);
    }
}
