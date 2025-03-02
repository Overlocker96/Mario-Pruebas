using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables Movimiento de Mario
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

    //Componentes de Mario
    Animator m_animator;
    SpriteRenderer m_sr;
    Rigidbody2D m_rb;

    //Variables Muerte y PowerUp
    public bool dead;
    public bool bigPowerUp;
    public bool flowerPowerUp;
    private bool jumping;
    private bool hit;

    //Variables Movimiento ANTIGUAS
    /*[SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 1f;
    private bool maxHeightReached;
    private float positionOld;*/

    //Recogemos las propiedades de Mario y reseteamos algunas de sus variables
    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
        m_sr = gameObject.GetComponent<SpriteRenderer>();
        m_rb = gameObject.GetComponent<Rigidbody2D>();
        dead = false;
        bigPowerUp = false;
        jump = false;
    }

    private void Update()
    {
        this.horizontal = Input.GetAxisRaw("Horizontal");//Recogemos las teclas de movimiento y las aplicamos como Float a horizontal

        if (dead == false)//Si no estamos muertos, entramos al método Salto (Salto debe estar en Update)
        {
            Jumping();
        }
    }

    private void FixedUpdate()
    {
        if (dead == false)//Si no estamos muertos, entramos al método Movimiento (Movimiento debe estar en FixedUpdate)
        {
            Movement();
        }
    }

    //Late Update para las Animaciones
    private void LateUpdate()
    {
        m_animator.SetFloat("VelocityX", horizontal);
        m_animator.SetBool("Jumping", jumping);
        m_animator.SetBool("BigPowerUp", bigPowerUp);
        m_animator.SetBool("FlowerPowerUp", flowerPowerUp);
        m_animator.SetBool("Dead", dead);

        if ((m_rb.velocity.y > 0.1 || m_rb.velocity.y < -0.1) && dead == false)//Si la velocidad en no es casi 0, y no estamos muertos, estamos saltando
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

    //Método de Movimiento
    private void Movement()
    {
        direction = horizontal;//Igualamos las variables (Podría ser la misma, pero prefiero que sea diferente a la del Animator)

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
    }

    //Método para Saltar
    private void Jumping()
    {
        //Lanzamos los Raycast
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position - (m_sr.bounds.size * 0.45f),//Ambos desde un poco menos del tamaño del SpriteRenderer, esta a la Izq
            Vector2.down, m_sr.bounds.size.y * 0.55f,//Dirección hacia abajo y distancia máxima de un poco más de la mitad del SpriteRenderer
            LayerMask.GetMask("Ground", "Blocks"));//Solo se aplica a las Layer "Ground" y "Blocks"
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + (m_sr.bounds.size * 0.45f),//Ambos desde un poco menos del tamaño del SpriteRenderer, esta a la Dch
            Vector2.down, m_sr.bounds.size.y * 0.55f,//Dirección hacia abajo y distancia máxima de un poco más de la mitad del SpriteRenderer
            LayerMask.GetMask("Ground", "Blocks"));//Solo se aplica a las Layer "Ground" y "Blocks"

        //Código para el Salto
        if (Input.GetKeyDown(KeyCode.Space) && this.jump)//Cuando estamos pulsando saltar y estamos saltando
        {
            this.m_rb.velocity = new Vector2(this.m_rb.velocity.x, this.m_rb.velocity.y);//Aplicamos la velocidad sin modificar
        }

        if (Input.GetKey(KeyCode.Space) && !jumping && //Pulsamos saltar y si no estamos saltando
            ((hit1.collider || hit2.collider) || (hit1.collider && hit2.collider)))//Y alguno de los raycast, O los dos están activos
        {
            this.jump = true;//Estamos saltando
            this.m_rb.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);//Y aplicamos una fuerza hacia arriba con jumpForce
        }

        //Antiguo Código de Salto
        /*if (jumping != true && (hit1.collider || hit2.collider))
        {
            maxHeightReached = false;
            positionOld = m_rb.position.y;
            m_rb.AddForce(Vector2.up * jumpForce);
            //m_rb.velocity = new Vector2(m_rb.velocity.x, jumpHeight);
        }
        else if (m_rb.position.y < positionOld + 3f && Input.GetKey(KeyCode.Space) == true && maxHeightReached == false)
        {
            m_rb.AddForce(Vector2.up * jumpForce * 2);
            //m_rb.velocity = new Vector2(m_rb.velocity.x, jumpHeight * 2);
        }
        else if (m_rb.velocity.y <= 0)
        {
            maxHeightReached = true;
        }
        else if (m_rb.position.y > positionOld + 3f && Input.GetKeyDown(KeyCode.Space) == true && maxHeightReached == true)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, m_rb.velocity.y);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && maxHeightReached)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, m_rb.velocity.y);
        }*/
    }

    //Evento de colision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.isTrigger == false && dead == false)//Si la colision NO es un trigger y NO estamos muertos
        {
            if (collision.gameObject.GetComponent<Goomba>() ||//Si colisionamos con un Goomba
            (collision.gameObject.GetComponent<Koopa>() &&//O un Koopa que
            (collision.gameObject.GetComponent<Koopa>().stomped == false ||//No esté Stomped (Modo Caparazón estático)
            collision.gameObject.GetComponent<Koopa>().shellMoving == true)))//O esté  (Modo Caparazón Moviéndose)
            {
                if (flowerPowerUp == true)//Si tenemos el Potenciador de Flor
                {
                    flowerPowerUp = false;//Lo desactivamos
                }
                else if (bigPowerUp == true)//Si tenemos el Potenciador de Mario Grande
                {
                    bigPowerUp = false;//Lo desactivamos
                }
                else if (bigPowerUp == false)//Si no tenemos el Potenciador de Mario Grande
                {
                    dead = true;//Estamos muertos
                    Death();//Llamamos al Método Muerte
                }
            }
        }

        if (collision.gameObject.GetComponent<Mushroom>())//Si colisionamos con una Seta Normal
        {
            Destroy(collision.gameObject);//Destruimos la Seta Normal
            GameManager.Instance.AddPoints();//Añadimos Puntos
            bigPowerUp = true;//Aplicamos Potenciador Mario Grande
            
        }

        if (collision.gameObject.GetComponent<LifeMushroom>())//Si colisionamos con una Seta de Vida
        {
            Destroy(collision.gameObject);//Destruimos la Seta de Vida
            GameManager.Instance.AddPoints();//Añadimos Puntos
            GameManager.Instance.Life();//Añadimos una Vida
            
        }

        if (collision.gameObject.GetComponent<Flower>())//Si colisionamos con una Flor
        {
            Destroy(collision.gameObject);//Destruimos la Flor
            GameManager.Instance.AddPoints();//Añadimos Puntos
            bigPowerUp = false;//Desactivamos Potenciador Mario Grande
            flowerPowerUp = true;//Aplicamos Potenciador Flor
        }
    }

    //Evento de Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DeathZone" && dead == false)//Si entramos en la ZonaMuerte y NO estamos muertos
        {
            dead = true;//Estamos Muertos
            Death();//Llamamos al evento Muerte
        }
    }

    //Evento Muerte
    public void Death()
    {
        this.m_rb.velocity = Vector2.zero;//La velocidad es 0 y nos paramos
        this.m_rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);//Aplicamos una fuerza hacia arriba
        this.gameObject.layer = LayerMask.NameToLayer("Dead");//Aplicamos la Layer "Muerto" a Mario
    }
}