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
    public Button LoadSpriteButton;
    public Button LoadNewSceneButton;
    public TextMeshProUGUI PointsText;

    //private SpriteAssetLoader spriteAssetLoader;
    private AssetBundlesManager assetBundleManager;

    private SpriteAssetLoader[] objectsForSpritesChanges;

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

        //spriteAssetLoader = FindObjectOfType<SpriteAssetLoader>();
        assetBundleManager = FindObjectOfType<AssetBundlesManager>();
        LoadSpriteButton.onClick.AddListener(() => OnLoadSprite());
        LoadNewSceneButton.onClick.AddListener(() => OnNewSceneLoad());
    }

    private void OnLoadSprite()
    {
        objectsForSpritesChanges = FindObjectsOfType<SpriteAssetLoader>();
        foreach (var item in objectsForSpritesChanges)
        {
            item.GetComponent<SpriteAssetLoader>().LoadSprite();
        }
    }

    private void OnNewSceneLoad()
    {
        assetBundleManager.GetAndLoadNewScene();
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
