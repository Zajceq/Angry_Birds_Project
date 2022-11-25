using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : InteractiveComponent
{
    private ParticleSystem m_particles;
    private AudioSource m_audioSource;
    //private Rigidbody2D m_rigidbody;
    //private Vector3 m_startPosition;
    //private Quaternion m_startRotation;
    //private Vector3 m_startScale;

    protected override void Start()
    {
        base.Start();

        m_particles = GetComponentInChildren<ParticleSystem>();
        m_audioSource = GetComponent<AudioSource>();

        //m_rigidbody = GetComponent<Rigidbody2D>();

        //m_startPosition = transform.position;
        //m_startRotation = transform.rotation;
        //m_startScale = transform.localScale;

        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            m_particles.Play();
            m_audioSource.Play();
        }
    }

    //private void DoPlay()
    //{
    //    m_rigidbody.simulated = true;
    //}

    //private void DoPause()
    //{
    //    m_rigidbody.simulated = false;
    //}

    //void OnDestroy()
    //{
    //    GameplayManager.OnGamePaused -= DoPause;
    //    GameplayManager.OnGamePlaying -= DoPlay;
    //}

    //public void DoRestart()
    //{
    //    transform.position = m_startPosition;
    //    transform.rotation = m_startRotation;
    //    transform.localScale = m_startScale;

    //m_rigidbody.velocity = Vector3.zero;
    //    m_rigidbody.angularVelocity = 0.0f;
    //    m_rigidbody.simulated = true;
    //}
}
