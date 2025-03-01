using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    Rigidbody2D g_rb;
    Rigidbody2D m_rb;
    Animator g_anim;
    int deadLayer;

    [SerializeField]
    private float velocity;
    private bool moving = false;
    private bool stomped = false;

    private void Awake()
    {
        g_rb = gameObject.GetComponent<Rigidbody2D>();
        g_anim = gameObject.GetComponent<Animator>();
        deadLayer = LayerMask.NameToLayer("EnemyDead");
    }

    void FixedUpdate()
    {
        g_anim.SetBool("Moving", moving);
        stomped = g_anim.GetBool("Stomp");

        if (moving && !stomped)
        {
            g_rb.velocity = new Vector2(velocity, g_rb.velocity.y);

            if(Mathf.Abs(g_rb.velocity.x) < 0.05f)
            {
                ChangeDirection();
            }
        }
        else if (stomped)
        {
            g_rb.velocity = new Vector2(0, 0);
            gameObject.layer = deadLayer;
            Destroy(this.gameObject, 0.5f);
        }
    }
    private void ChangeDirection()
    {
        this.velocity *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.name == "Mario")
        {
            moving = true;
        }
    }
}