using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    Rigidbody2D g_rb;
    Animator g_animator;

    [SerializeField]
    private float velocity;

    bool Moving = false;

    private void Awake()
    {
        g_rb = gameObject.GetComponent<Rigidbody2D>();
        g_animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    void FixedUpdate()
    { 
        g_animator.SetFloat("Velocity", g_rb.velocity.x);
        if (Mathf.Abs(g_rb.velocity.x) < 0.05f)
        {
            ChangeDirection();

        }
        g_rb.velocity = new Vector2(velocity, g_rb.velocity.y);
    }
    private void ChangeDirection()
    {
        this.velocity *= -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(g_rb.velocity.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Mario")
        {
            Moving = true;
        }
    }
}