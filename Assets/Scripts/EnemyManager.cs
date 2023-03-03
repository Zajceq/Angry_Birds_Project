using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EnemyManager : Singleton<EnemyManager>
{
    public int enemiesOnTheScene;
    public static Action onAllEnemiesKilled;

    private void Start()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        enemiesOnTheScene = enemies.Length;
    }

    public void CheckIfThereIsNoEnemiesLeft()
    {
        if (enemiesOnTheScene <= 0)
        {
            onAllEnemiesKilled?.Invoke();
        }
    }
}
