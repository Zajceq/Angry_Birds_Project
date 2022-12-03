using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsController : MonoBehaviour
{
    public Button AcceptButton;
    public Button CancelButton;
    public MainMenuController MainMenu;

    private float m_initialVolume = 0.0f;

    private void Start()
    {
        AcceptButton.onClick.AddListener(() => OnAccept());
        CancelButton.onClick.AddListener(() => OnCancel());
    }

    private void OnEnable()
    {
        m_initialVolume = AudioListener.volume;
    }

    private void OnAccept()
    {
        SaveManager.Instance.SaveData.m_masterVolume = AudioListener.volume;
        SaveManager.Instance.SaveSettings();
        MainMenu.ShowOptions(false);
    }

    private void OnCancel()
    {
        AudioListener.volume = m_initialVolume;
        MainMenu.ShowOptions(false);
    }
}
