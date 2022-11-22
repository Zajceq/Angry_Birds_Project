using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    private ParticleSystem m_particles;
    private AudioSource m_audioSource;
    public AudioClip ImpactSound;

    private void Start()
    {
        m_particles = GetComponentInChildren<ParticleSystem>();
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            m_particles.Play();
            m_audioSource.PlayOneShot(ImpactSound);
        }
    }
}
