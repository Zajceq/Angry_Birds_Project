using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveComponent : MonoBehaviour, IRestartableObject
{
    protected Rigidbody2D m_rigidbody;
    protected Vector3 m_startPosition;
    protected Quaternion m_startRotation;
    protected Vector3 m_startScale;

    protected virtual void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
        m_startScale = transform.localScale;

        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
    }

    public virtual void DoRestart()
    {
        transform.position = m_startPosition;
        transform.rotation = m_startRotation;
        transform.localScale = m_startScale;

        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = 0.0f;
        m_rigidbody.simulated = true;
    }

    protected virtual void DoPlay()
    {
        m_rigidbody.simulated = true;
    }

    protected virtual void DoPause()
    {
        m_rigidbody.simulated = false;
    }

    protected virtual void OnDestroy()
    {
        GameplayManager.OnGamePaused -= DoPause;
        GameplayManager.OnGamePlaying -= DoPlay;
    }
}
