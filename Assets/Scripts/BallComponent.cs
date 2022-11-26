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
    public AudioClip PullSound;
    public AudioClip ShootSound;
    public AudioClip ImpactSound;
    public AudioClip RestartSound;
    private Animator m_animator;
    private ParticleSystem m_particles;

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
    }

    private void Update() 
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
        {
            m_connectedJoint.enabled = false;
            m_lineRenderer.enabled = false;
            m_trailRenderer.enabled = !m_hitTheGround;
        }
    }

    private void OnMouseDrag() 
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

    private void OnMouseUp() 
    {
        m_rigidbody.simulated = true;
        m_audioSource.PlayOneShot(ShootSound);
        m_particles.Play();
    }

    private void OnMouseDown()
    {
        m_audioSource.PlayOneShot(PullSound);    
    }

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

        m_audioSource.PlayOneShot(RestartSound);
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
        PlaySoundOnCollision(collision, "Ground", ImpactSound);
    }
}
