using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    int lastLevelIndex;

    private void Awake()
    {
        lastLevelIndex = SceneManager.sceneCountInBuildSettings;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        EnemyManager.onAllEnemiesKilled += LoadNextScene;
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < lastLevelIndex - 1)
        {
            SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    private void OnDisable()
    {
        EnemyManager.onAllEnemiesKilled -= LoadNextScene;
    }
}
