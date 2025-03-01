using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Koopa : MonoBehaviour
{
    Rigidbody2D k_rb;
    Rigidbody2D m_rb;
    Animator k_anim;
    int deadLayer;

    [SerializeField]
    private float velocity;
    [SerializeField]
    private bool moving = false;
    [SerializeField]
    public bool stomped = false;

    private void Awake()
    {
        k_rb = gameObject.GetComponent<Rigidbody2D>();
        k_anim = gameObject.GetComponent<Animator>();
        deadLayer = LayerMask.NameToLayer("EnemyDead");
    }

    private void FixedUpdate()
    {
        k_anim.SetBool("Moving", moving);
        stomped = k_anim.GetBool("Stomp");

        if (moving && !stomped)
        {
            StopCoroutine(this.Shell());
            Move();
        }

        if (!moving && stomped)
        {
            Stop();
            moving = false;
            StartCoroutine(this.Shell());
        }
    }

    private void Move()
    {
        k_rb.velocity = new Vector2(velocity, k_rb.velocity.y);
        if (Mathf.Abs(k_rb.velocity.x) < 0.05f)
        {
            ChangeDirection();
        }
    }
    private void ChangeDirection()
    {
        this.velocity *= -1;
    }

    private void Stop()
    {
        k_rb.velocity = new Vector2(0, 0);
    }

    private IEnumerator Shell()
    {
        float time = 0f;
        float duration = 2f;

        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null; 
        }

        stomped = false;
        moving = true;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Mario")
        {
            Move();
            k_rb.velocity = new Vector2(velocity * 2, k_rb.velocity.y);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.name == "Mario" && stomped == false)
        {
            moving = true;
        }
    }
}
