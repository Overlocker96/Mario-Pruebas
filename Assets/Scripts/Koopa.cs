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
            velocity = -2;
            StopCoroutine(this.Shell());
            k_rb.velocity = new Vector2(velocity, k_rb.velocity.y);
            if (Mathf.Abs(k_rb.velocity.x) < 0.05f)
            {
                ChangeDirection();
            }
        }

        if (!moving && stomped && !shell)
        {
            k_rb.velocity = new Vector2(0, 0);
            moving = false;
            StartCoroutine(this.Shell());
        }

        if(shell && stomped)
        {
            k_rb.velocity = new Vector2(velocity, k_rb.velocity.y);
            if (Mathf.Abs(k_rb.velocity.x) < 0.05f)
            {
                ChangeDirection();
            }
            velocity = 8;
            shellMoving = true;
        }
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
                m_rb.velocity = new Vector2(0, 20);
                shell = true;
                this.velocity *= -1;
            }
            else
            {
                m_rb.velocity = new Vector2(0, 20);
                shell = true;
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
