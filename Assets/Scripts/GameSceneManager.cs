using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    int lastLevelIndex;
    [SerializeField] private float delay = 2f;

    private void Awake()
    {
        lastLevelIndex = SceneManager.sceneCountInBuildSettings;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        EnemyManager.onAllEnemiesKilled += LoadNextScene;
    }

    async void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < lastLevelIndex - 1)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }
        else
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            SceneManager.LoadSceneAsync(0);
        }
    }

    private void OnDisable()
    {
        EnemyManager.onAllEnemiesKilled -= LoadNextScene;
    }
}
