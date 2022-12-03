using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : InteractiveComponent
{
    private ParticleSystem m_particles;
    public GameSettingsDatabase GameDatabase;

    protected override void Start()
    {
        base.Start();

        m_particles = GetComponentInChildren<ParticleSystem>();

        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            m_particles.Play();
            GameplayManager.Instance.Points += 1;
        }
        PlaySoundOnCollision(collision, "Ball", GameDatabase.ImpactSound);
    }
}
