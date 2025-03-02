using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    //Propiedades del Goomba
    Rigidbody2D g_rb;
    Rigidbody2D m_rb;
    Animator g_anim;
    int deadLayer;

    //Variables del Goomba
    [SerializeField]
    private float velocity;
    private bool moving = false;
    private bool stomped = false;

    //Recogemos las Propiedades del Goomba
    private void Awake()
    {
        g_rb = gameObject.GetComponent<Rigidbody2D>();
        g_anim = gameObject.GetComponent<Animator>();
        deadLayer = LayerMask.NameToLayer("EnemyDead");
    }

    //FixedUpdate para F�sicas
    private void FixedUpdate()
    {
        //Si empezamos a movernos y no estamos aplastados, nos movemos
        if (moving && !stomped)
        {
            Move();
        }
        else if (stomped) //Si nos aplastan
        {
            Stop();//Paramos el movimiento
            gameObject.layer = deadLayer;//Pasamos a una Layer solo con colisi�n con el suelo
            Destroy(this.gameObject, 0.5f);//Y destruimos el GameObject Goomba en 0.5 segundos
        }
    }

    //LateUpdate para Animaciones
    private void LateUpdate()
    {
        g_anim.SetBool("Moving", moving);
        stomped = g_anim.GetBool("Stomp");
    }

    //M�todo para movernos
    private void Move()
    {
        //Si la velocidad es casi 0, cambiamos la direcci�n
        if (Mathf.Abs(g_rb.velocity.x) < 0.05f)
        {
            ChangeDirection();
        }

        //Aplicamos velocidades 
        g_rb.velocity = new Vector2(velocity, g_rb.velocity.y);
    }

    //M�todo para pararnos
    private void Stop()
    {
        g_rb.velocity = new Vector2(0, 0);
    }

    //M�todo para cambiar direcci�n
    private void ChangeDirection()
    {
        this.velocity *= -1;
    }

    //Evento Trigger para empezar a moverse al acercarse Mario
    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.name == "Mario")
        {
            moving = true;
        }
    }
}