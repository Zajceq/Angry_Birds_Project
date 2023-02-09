using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : InteractiveComponent
{
    private SpringJoint2D m_connectedJoint;
    private Rigidbody2D m_connectedBody;
    public float SlingStart = 1f;
    public float MaxSpringDistance = 2.5f;
    private LineRenderer m_lineRenderer;
    public Transform LeftSlingPoint;
    private TrailRenderer m_trailRenderer;
    private bool m_hitTheGround = false;
    private Animator m_animator;
    private ParticleSystem m_particles;
    private bool isShooted;
    public GameSettingsDatabase GameDatabase;

    protected override void Start() 
    {
        base.Start();

        m_connectedJoint = GetComponent<SpringJoint2D>();
        m_connectedBody = m_connectedJoint.connectedBody;
        m_lineRenderer = GetComponent<LineRenderer>();
        m_trailRenderer = GetComponent<TrailRenderer>();

        m_animator = GetComponentInChildren<Animator>();
        m_particles = GetComponentInChildren <ParticleSystem>();

        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;

        isShooted = false;

        StartCoroutine(CheckingSpringJoint());
    }

    IEnumerator CheckingSpringJoint()
    {
        while (true)
        {
            yield return null;
            yield return null;
            if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
            {
                m_connectedJoint.enabled = false;
                m_lineRenderer.enabled = false;
                m_trailRenderer.enabled = !m_hitTheGround;

                isShooted = true;
            }
        }
    }

    private void Update()
    {
    #if UNITY_IOS || UNITY_ANDROID
            UpdateTouch();
    #endif
    }

#if UNITY_IOS || UNITY_ANDROID
    private void UpdateTouch()
    {
        if (Input.touchCount <= 0)
            return;

        switch (Input.touches[0].phase)
        {
            case TouchPhase.Began:
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                            OnTouchDown();
                    }
                }
                break;

            case TouchPhase.Moved:
                {
                    OnTouchDrag();
                }
                break;

            case TouchPhase.Ended:
                {
                    OnTouchUp();
                }
                break;
        };
    }

    private void OnTouchDrag()
    {
        if (GameplayManager.Instance.GameState == EGameState.Paused || isShooted == true)
        {
            return;
        }
        else
        {
            if (Input.touchCount <= 0)
                return;

            m_rigidbody.simulated = false;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Vector2 newBallPos = new Vector3(worldPos.x, worldPos.y);
            float CurJoingDistance = Vector3.Distance(newBallPos, m_connectedBody.transform.position);

            if (CurJoingDistance > MaxSpringDistance)
            {
                Vector2 direction = (newBallPos - m_connectedBody.position).normalized;
                transform.position = m_connectedBody.position + direction * MaxSpringDistance;
            }
            else
            {
                transform.position = newBallPos;
            }

            SetLineRendererPoints();
            m_hitTheGround = false;
        }
    }

    private void OnTouchUp()
    {
        if (GameplayManager.Instance.GameState == EGameState.Paused || isShooted == true)
        {
            return;
        }
        else
        {
            m_rigidbody.simulated = true;
            m_audioSource.PlayOneShot(GameDatabase.ShootSound);
            m_particles.Play();
        }
    }

    private void OnTouchDown()
    {
        if (GameplayManager.Instance.GameState == EGameState.Paused || isShooted == true)
        {
            return;
        }
        else
        {
            m_audioSource.PlayOneShot(GameDatabase.PullSound);
        }
    }
#endif

#if UNITY_EDITOR
    private void OnMouseDrag() 
    {   
        if (GameplayManager.Instance.GameState == EGameState.Paused || isShooted == true)
        {
            return;
        }
        else
        {
            m_rigidbody.simulated = false;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 newBallPos = new Vector3(worldPos.x, worldPos.y);
            float CurJoingDistance = Vector3.Distance(newBallPos, m_connectedBody.transform.position);

            if (CurJoingDistance > MaxSpringDistance)
            {
                Vector2 direction = (newBallPos - m_connectedBody.position).normalized;
                transform.position = m_connectedBody.position + direction * MaxSpringDistance;
            }
            else
            {
                transform.position = newBallPos;
            }

            SetLineRendererPoints();
            m_hitTheGround = false;
        }
    }

    private void OnMouseUp() 
    {
        if (GameplayManager.Instance.GameState == EGameState.Paused || isShooted == true)
        {
            return;
        }
        else
        {
            m_rigidbody.simulated = true;
            m_audioSource.PlayOneShot(GameDatabase.ShootSound);
            m_particles.Play();
        }
    }

    private void OnMouseDown()
    {
        if (GameplayManager.Instance.GameState == EGameState.Paused || isShooted == true)
        {
            return;
        }
        else
        {
            m_audioSource.PlayOneShot(GameDatabase.PullSound);    
        }
    }
#endif

    public bool IsSimulated()
    {
        return m_rigidbody.simulated;
    }

    public float GetBallSpeed()
    {
        return m_rigidbody.velocity.magnitude;
    }


    public override void DoRestart()
    {
        base.DoRestart();

        m_connectedJoint.enabled = true;
        m_lineRenderer.enabled = true;
        m_trailRenderer.enabled = false;

        SetLineRendererPoints();

        m_audioSource.PlayOneShot(GameDatabase.RestartSound);
        
        isShooted = false;
    }

    private void SetLineRendererPoints()
    {
        m_lineRenderer.positionCount = 3;
        m_lineRenderer.SetPositions(new Vector3[] {
            m_connectedBody.position,
            transform.position,
            LeftSlingPoint.position});
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_hitTheGround = true;

            m_animator.enabled = true;
            m_animator.Play(0);
        }
        PlaySoundOnCollision(collision, "Ground", GameDatabase.ImpactSound);
    }
}
