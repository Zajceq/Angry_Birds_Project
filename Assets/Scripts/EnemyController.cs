using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : InteractiveComponent
{
    public GameSettingsDatabase GameDatabase;
    private bool gotHit;
    [SerializeField] private int points = 1;

    protected override void Start()
    {
        base.Start();
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            if (!gotHit)
            {
                AnalyticsManager.Instance.SendEvent("HitTarget");
                gotHit = true;
            }
        }
        PlaySoundOnCollision(collision, "Ball", GameDatabase.ImpactSound);

    }

    private void OnDisable()
    {
        GameplayManager.Instance.Points += points;
        EnemyManager.Instance.enemiesOnTheScene -= 1;
        EnemyManager.Instance.CheckIfThereIsNoEnemiesLeft();
    }
    public override void DoRestart()
    {
        base.DoRestart();
    }
}
