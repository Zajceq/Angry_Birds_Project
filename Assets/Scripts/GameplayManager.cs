using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameSettingsDatabase GameDatabase;
    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();
    public EnemyManager enemyManager;


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
        GameObject.Instantiate(GameDatabase.SimpleAnimPropPrefab, new Vector3(20.0f, -2.75f, 0.0f), Quaternion.identity);
        m_state = EGameState.Playing;
        GetAllRestartableObjects();

        m_HUD = FindObjectOfType<HUDController>();
        Points = 0;
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
        for (int i = 0; i < rootGameObjects.Length; i++)
        {
            IRestartableObject[] childrenInterfaces = rootGameObjects[i].GetComponentsInChildren<IRestartableObject>();

            for (int j = 0; j < childrenInterfaces.Length; j++)
                m_restartableObjects.Add(childrenInterfaces[j]);
        }
    }

    public void Restart()
    {
        for (int i = 0; i < m_restartableObjects.Count; i++)
            m_restartableObjects[i].DoRestart();
        
        Points = 0;
        GameState = EGameState.Playing;
    }

    public int Points
    {
        get { return m_points; }
        set
        {
            if (m_points != value)
            {
                m_points = value;
                m_HUD.UpdatePoints(m_points);
            }
        }
    }
}
