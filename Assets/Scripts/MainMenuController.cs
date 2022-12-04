using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public Button PlayButton;
    public Button OptionsButton;
    public Button QuitButton;

    public GameObject PopupPanel;
    public Button YesButton;
    public Button NotSureButton;

    public GameObject OptionsMenuPanel;
    public GameObject BackgroundMenu;

    private void Start()
    {
        PlayButton.onClick.AddListener(() => OnPlay());
        OptionsButton.onClick.AddListener(() => ShowOptions(true));
        QuitButton.onClick.AddListener(() => OnQuit());

        YesButton.onClick.AddListener(() => OnQuitYes());
        NotSureButton.onClick.AddListener(() => OnQuitNo());

        SetMainMenuVisible(true);
        SetBackgroundVisible(true);
        SetQuestionPopupVisible(false);
        SetOptionsMenuVisible(false);

        GameplayManager.Instance.GameState = EGameState.Paused;
    }

    private void SetQuestionPopupVisible(bool visible)
    {
        PopupPanel.SetActive(visible);
    }
    private void SetMainMenuVisible(bool visible)
    {
        MainMenuPanel.SetActive(visible);
    }

    private void SetOptionsMenuVisible(bool visible)
    {
        OptionsMenuPanel.SetActive(visible);
    }

    private void SetBackgroundVisible(bool visible)
    {
        BackgroundMenu.SetActive(visible);
    }

    private void OnPlay()
    {
        SetMainMenuVisible(false);
        BackgroundMenu.SetActive(false);
        GameplayManager.Instance.Restart();
    }

    public void ShowOptions(bool bShow)
    {
        SetOptionsMenuVisible(bShow);
        SetMainMenuVisible(!bShow);
    }

    private void OnQuit()
    {
        SetQuestionPopupVisible(true);
    }

    private void OnQuitNo()
    {
        SetQuestionPopupVisible(false);
    }

    private void OnQuitYes()
    {
        Application.Quit();
    }
}
