using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Koopa : MonoBehaviour
{
    Rigidbody2D k_rb;
    Rigidbody2D m_rb;
    Animator k_anim;
    Vector2 contactPoint;

    [SerializeField]
    private float velocity;
    [SerializeField]
    private float velocityShell;
    [SerializeField]
    private bool moving = false;
    [SerializeField]
    public bool stomped = false;
    [SerializeField]
    public bool shell = false;
    [SerializeField]
    public bool shellMoving = false;

    private void Awake()
    {
        k_rb = gameObject.GetComponent<Rigidbody2D>();
        k_anim = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        k_anim.SetBool("Moving", moving);
        stomped = k_anim.GetBool("Stomp");

        if (moving && !stomped && !shell)
        {
            StopCoroutine(this.Shell());
            Move();
        }

        if (!moving && stomped && !shell)
        {
            Stop();
            moving = false;
            StartCoroutine(this.Shell());
        }

        if (shell && stomped)
        {
            velocity = velocityShell;
            Move();
            shellMoving = true;
        }
    }
    
    private void Move()
    {
        if (Mathf.Abs(k_rb.velocity.x) < 0.05f)
        {
            ChangeDirection();
        }

        k_rb.velocity = new Vector2(velocity, k_rb.velocity.y);
    }
    private void Stop()
    {
        k_rb.velocity = new Vector2(0, 0);
    }

    private void ChangeDirection()
    {
        this.velocity *= -1;
    }

    private IEnumerator Shell()
    {
        float time = 0f;
        float duration = 5f;

        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null; 
        }

        stomped = false;
        moving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Mario")
        {
            contactPoint = collision.contacts[0].point;

            if (contactPoint.x > k_rb.position.x)
            {
                m_rb.velocity = new Vector2(0, 10);
                shell = true;
            }
            else
            {
                m_rb.velocity = new Vector2(0, 10);
                shell = true;
                this.velocity *= -1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.name == "Mario" && stomped == false)
        {
            moving = true;
        }
    }
}
