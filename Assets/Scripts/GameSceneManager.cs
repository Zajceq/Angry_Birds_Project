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


    public void LoadNextScene(int currentSceneIndex)
    {
        if (currentSceneIndex < lastLevelIndex - 1)
        {
            SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
