using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager>
{
    private EGameState m_state;
    public delegate void GameStateCallback();
    public static event GameStateCallback OnGamePaused;
    public static event GameStateCallback OnGamePlaying;

    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();

    public enum EGameState
    {
        Playing,
        Paused
    }

    public EGameState GameState
    {
        get { return m_state; }
        set 
        { 
            m_state = value;
            switch (m_state)
            {
                case EGameState.Paused:
                {
                    if (OnGamePaused != null)
                        OnGamePaused();
                } break;

                case EGameState.Playing:
                {
                    if (OnGamePlaying != null)
                    {
                        OnGamePlaying();
                    }
                } break;
            }
        }
    }

    private void Start()
    {
        m_state = EGameState.Playing;
        GetAllRestartableObjects();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            switch (GameState)
            {
                case EGameState.Playing:
                    {
                        GameState = EGameState.Paused;
                    }
                    break;

                case EGameState.Paused:
                    {
                        GameState = EGameState.Playing;
                    }
                    break;
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void GetAllRestartableObjects()
    {
        m_restartableObjects.Clear();

        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            IRestartableObject[] childrenInterfaces = rootGameObject.GetComponentsInChildren<IRestartableObject>();

            foreach (var childInterface in childrenInterfaces)
                m_restartableObjects.Add(childInterface);
        }
    }

    private void Restart()
    {
        foreach (var restartableObject in m_restartableObjects)
            restartableObject.DoRestart();
    }
}
