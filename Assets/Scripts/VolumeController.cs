using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Button VolumeUp;
    public Button VolumeDown;
    //public Image Bar;
    public Slider Slider;

    
    void Start()
    {
        VolumeUp.onClick.AddListener(() => OnChangeVolume(true));
        VolumeDown.onClick.AddListener(() => OnChangeVolume(false));
        Slider.onValueChanged.AddListener((v) => { AudioListener.volume = v; });
    }

    private void UpdateBar()
    {
        Slider.value = AudioListener.volume;
    }

    private void OnEnable()
    {
        UpdateBar();
    }

    private void OnChangeVolume(bool bUp)
    {
        float newValue = AudioListener.volume;

        if (bUp)
            newValue += 0.1f;
        else
            newValue -= 0.1f;

        newValue = Mathf.Clamp01(newValue);
        AudioListener.volume = newValue;
        UpdateBar();
    }
}
