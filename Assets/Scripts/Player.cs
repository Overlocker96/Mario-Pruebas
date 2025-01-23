using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 1f;

    Animator m_animator;
    SpriteRenderer m_sr;
    Rigidbody2D m_rb;

    private bool jumping;

    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
        m_sr = gameObject.GetComponent<SpriteRenderer>();
        m_rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        m_animator.SetFloat("VelocityX", horizontal);

        if (horizontal < 0)
        {
            m_sr.flipX = true;
        }
        else
        {
            m_sr.flipX = false;
        }

        m_rb.velocity = new Vector2(horizontal * speed, m_rb.velocity.y);
    }

    void Update()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position - (m_sr.bounds.size * 0.75f), Vector2.down, m_sr.bounds.size.y, LayerMask.GetMask("Ground"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + (m_sr.bounds.size * 0.75f), Vector2.down, m_sr.bounds.size.y, LayerMask.GetMask("Ground"));
        if (Input.GetKeyDown(KeyCode.Space) && (hit1.collider || hit2.collider))
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, jumpHeight);
        }
        if (m_rb.velocity.y > 0.1 || m_rb.velocity.y < -0.1)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
        m_animator.SetBool("Jumping", jumping);
    }
}