using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public Button PauseButton;
    public Button RestartButton;
    public Button LoadSpriteButton;
    public Button LoadNewSceneButton;
    public TextMeshProUGUI PointsText;

    private AssetBundlesManager assetBundleManager;
    private GameSceneManager gameSceneManager;

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

        assetBundleManager = FindObjectOfType<AssetBundlesManager>();
        gameSceneManager = FindObjectOfType<GameSceneManager>();
        LoadSpriteButton.onClick.AddListener(() => OnLoadSprite());
        LoadNewSceneButton.onClick.AddListener(() => OnNewSceneLoad());
    }

    private void OnLoadSprite()
    {
        objectsForSpritesChanges = FindObjectsOfType<SpriteAssetLoader>();
        //foreach (var item in objectsForSpritesChanges)
        //{
        //    item.GetComponent<SpriteAssetLoader>().LoadSprite();
        //}
        for (int i = 0; i < objectsForSpritesChanges.Length; i++)
            objectsForSpritesChanges[i].GetComponent<SpriteAssetLoader>().LoadSprite();
    }

    private void OnNewSceneLoad()
    {
        gameSceneManager.LoadNextScene();
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
