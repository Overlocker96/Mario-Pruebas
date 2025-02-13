using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 6f;

    Animator m_animator;
    SpriteRenderer m_sr;
    Rigidbody2D m_rb;
    GameObject m_go;
    Collider2D m_c2D;

    private bool jumping;
    private bool maxHeightReached;
    private float positionOld;
    private bool dead;

    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
        m_sr = gameObject.GetComponent<SpriteRenderer>();
        m_rb = gameObject.GetComponent<Rigidbody2D>();
        m_go = GameObject.Find("Mario");
        m_c2D = gameObject.GetComponent<BoxCollider2D>();
        dead = false;
    }

    void FixedUpdate()
    {
        if (dead == false)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        m_animator.SetFloat("VelocityX", horizontal);

        if (horizontal < 0)
        {
            //m_sr.flipX = true;
            m_go.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            //m_sr.flipX = false;
            m_go.transform.localScale = new Vector3(1, 1, 1);
        }

        m_rb.velocity = new Vector2(horizontal * speed, m_rb.velocity.y);
    }

    void Update()
    {
        if (dead == false)
        {
            Jumping();
        }
    }

    private void Jumping()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position - (m_c2D.bounds.size / 2), Vector2.down, m_sr.bounds.size.y * 0.6f, LayerMask.GetMask("Ground"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + (m_c2D.bounds.size / 2), Vector2.down, m_sr.bounds.size.y * 0.6f, LayerMask.GetMask("Ground"));

        if (Input.GetKeyDown(KeyCode.Space) && (hit1.collider || hit2.collider))
        {
            maxHeightReached = false;
            positionOld = m_rb.position.y;
            m_rb.velocity = new Vector2(m_rb.velocity.x, jumpHeight);
        }
        else if (m_rb.position.y < positionOld + 3f && Input.GetKey(KeyCode.Space) == true && maxHeightReached == false)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, jumpHeight * 2);
        }
        else if (m_rb.position.y > positionOld + 3f && Input.GetKeyDown(KeyCode.Space) == true && maxHeightReached == true)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, m_rb.velocity.y);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && maxHeightReached)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, m_rb.velocity.y);
        }
        else if (m_rb.velocity.y <= 0)
        {
            maxHeightReached = true;
        }

        if ((m_rb.velocity.y > 0.1 || m_rb.velocity.y < -0.1) && dead == false)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }

        m_animator.SetBool("Jumping", jumping);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Goomba")
        {
            m_animator.SetBool("Dead", dead);
            dead = true;
            Death();
        }
    }

    public void Death()
    {
        m_rb.velocity = new Vector2(0f, jumpHeight * 4);
        m_c2D.enabled = false;
    }
}