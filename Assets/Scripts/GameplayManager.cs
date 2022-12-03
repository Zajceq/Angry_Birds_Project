using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

    public enum EGameState
    {
        Playing,
        Paused
    }

public class GameplayManager : Singleton<GameplayManager>
{
    private EGameState m_state;
    public delegate void GameStateCallback();
    public static event GameStateCallback OnGamePaused;
    public static event GameStateCallback OnGamePlaying;
    private HUDController m_HUD;
    public int m_points = 0;
    private PauseMenuController m_pauseMenu;
    public GameSettingsDatabase GameDatabase;
    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();


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
        GameObject.Instantiate(GameDatabase.TargetPrefab, new Vector3(0.0f, 4.0f, 0.0f), Quaternion.identity);
        GameObject.Instantiate(GameDatabase.SimpleAnimPropPrefab, new Vector3(15.0f, 0.0f, 0.0f), Quaternion.identity);
        m_state = EGameState.Playing;
        GetAllRestartableObjects();

        m_HUD = FindObjectOfType<HUDController>();
        Points = 0;
        m_pauseMenu = FindObjectOfType<PauseMenuController>();


    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            PlayPause();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayPause();
        }
    }

    public void PlayPause()
    {
        switch (GameState)
        {
            case EGameState.Playing: { GameState = EGameState.Paused; } break;
            case EGameState.Paused: { GameState = EGameState.Playing; } break;
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

    public void Restart()
    {
        foreach (var restartableObject in m_restartableObjects)
            restartableObject.DoRestart();
        
        Points = 0;
        GameState = EGameState.Playing;
    }

    public int Points
    {
        get { return m_points; }
        set
        {
            m_points = value;
            m_HUD.UpdatePoints(m_points);
        }
    }
    
}
