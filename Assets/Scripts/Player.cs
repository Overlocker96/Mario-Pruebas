using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables Movimiento NUEVAS
    private float horizontal;
    private float direction;
    private bool jump;
    [SerializeField]
    private float Aceleration;
    [SerializeField]
    private float Deceleration;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float jumpForce;

    //Componente
    Animator m_animator;
    SpriteRenderer m_sr;
    Rigidbody2D m_rb;
    Collider2D m_c2D;
    GameObject m_go;

    //Variables Movimiento ANTIGUAS
    /*[SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 1f;
    private bool maxHeightReached;
    private float positionOld;*/

    //Variables Muerte y PowerUp
    public bool dead;
    public bool bigPowerUp;
    private bool jumping;

    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
        m_sr = gameObject.GetComponent<SpriteRenderer>();
        m_rb = gameObject.GetComponent<Rigidbody2D>();
        m_c2D = gameObject.GetComponent<BoxCollider2D>();
        m_go = this.gameObject;
        dead = false;
        bigPowerUp = false;
    }

    private void Update()
    {
        this.horizontal = Input.GetAxisRaw("Horizontal");

        //Código para el Salto
        if (Input.GetKeyDown(KeyCode.Space) && this.jump)
        {
            this.m_rb.velocity = new Vector2(this.m_rb.velocity.x, 0f);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            this.jump = true;
            this.m_rb.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (dead == false)
        {
            Movement();
        }

        /*if (dead == false)
        {
            Jumping();
        }*/
    }

    private void LateUpdate()
    {
        m_animator.SetFloat("VelocityX", horizontal);
        m_animator.SetBool("Jumping", jumping);
        m_animator.SetBool("BigPowerUp", bigPowerUp);
        m_animator.SetBool("Dead", dead);

        if ((m_rb.velocity.y > 0.1 || m_rb.velocity.y < -0.1) && dead == false)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }

        //Animación Sliding => APLICAR!
        /*if(Mathf.Sign(this.direction) != Mathf.Sign(this.m_rb.velocity.x))
        {
            this.m_animator.SetBool("Sliding", true);
        }
        else
        {
            this.m_animator.SetBool("Sliding", false);
        }*/
    }

    private void Movement()
    {
        direction = horizontal;

        //Acelerando
        var forceMario = new Vector2(this.direction, 0) * this.Aceleration;

        //Frenando
        if (direction == 0 && Mathf.Abs(this.m_rb.velocity.x) > 0.1)
        {
            forceMario = new Vector2(Mathf.Sign(this.m_rb.velocity.x) * this.Deceleration * -1, 0);
        }

        //Decelerando
        if (direction != 0 && Mathf.Sign(direction) != Math.Sign(this.m_rb.velocity.x))
        {
            forceMario = new Vector2(Mathf.Sign(this.m_rb.velocity.x) * this.Deceleration * -1, 0);
        }

        //Parando
        if (Mathf.Abs(this.m_rb.velocity.x) < 0.1f && direction == 0)
        {
            this.m_rb.velocity = new Vector2(0, this.m_rb.velocity.y);
        }

        //Aplicamos la fuerza
        this.m_rb.AddForce(forceMario);

        //Limitamos la velocidad máxima
        if (Mathf.Abs(this.m_rb.velocity.x) > this.maxVelocity)
        {
            this.m_rb.velocity = new Vector2(Mathf.Clamp(this.m_rb.velocity.x,-this.maxVelocity, this.maxVelocity), this.m_rb.velocity.y);
        }

        if (horizontal < 0)
        {
            m_go.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            m_go.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    /*private void Jumping()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position - (m_sr.bounds.size / 2), Vector2.down, m_sr.bounds.size.y * 0.6f, LayerMask.GetMask("Ground","Blocks"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + (m_sr.bounds.size / 2), Vector2.down, m_sr.bounds.size.y * 0.6f, LayerMask.GetMask("Ground","Blocks"));

        if (jump && (hit1.collider || hit2.collider))
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
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Goomba>() && collision.collider.isTrigger == false)
        {
            dead = true;
            Death();
        }

        if (collision.gameObject.GetComponent<Mushroom>())
        {
            bigPowerUp = true;
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DeathZone" && dead == false)
        {
            dead = true;
            Death();
        }
    }

    public void Death()
    {
        this.m_rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        this.gameObject.layer = LayerMask.NameToLayer("Dead");
    }
}