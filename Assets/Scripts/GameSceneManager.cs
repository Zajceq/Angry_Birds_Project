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
    }


    public void LoadNextScene(int currentSceneIndex)
    {
        if (currentSceneIndex < lastLevelIndex)
        {
            SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }
        SceneManager.LoadSceneAsync(0);
    }
}
