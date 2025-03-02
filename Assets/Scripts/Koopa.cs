using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Koopa : MonoBehaviour
{
    //Propiedades del Koopa
    Rigidbody2D k_rb;
    Rigidbody2D m_rb;
    Animator k_anim;
    Vector2 contactPoint;

    //Variables del Koopa
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float velocityShell;
    private bool moving = false;
    public bool stomped = false;
    public bool shell = false;
    public bool shellMoving = false;

    //Recogemos las Propiedades del Koopa
    private void Awake()
    {
        k_rb = gameObject.GetComponent<Rigidbody2D>();
        k_anim = gameObject.GetComponent<Animator>();
    }

    //Fixed Update para Físicas
    private void FixedUpdate()
    {
        //Movimiento cuando no está modo caparazón
        if (moving && !stomped && !shell)
        {
            StopCoroutine(this.Shell());
            Move();
        }

        //Paramos movimientos cuando está en modo caparazón estático
        if (!moving && stomped && !shell)
        {
            Stop();
            moving = false;
            StartCoroutine(this.Shell());
        }

        //Si entramos en modo caparazón moviendose, nos movemos
        if (shell && stomped)
        {
            Move();
            shellMoving = true;
        }
    }

    //LateUpdate para Animaciones
    private void LateUpdate()
    {
        k_anim.SetBool("Moving", moving);
        stomped = k_anim.GetBool("Stomp");
    }

    //Método Move para el movimiento
    private void Move()
    {
        //Cambiamos la dirección cuando la velocidad es casi 0
        if (Mathf.Abs(k_rb.velocity.x) < 0.05f)
        {
            ChangeDirection();
        }

        //Si estámos modo caparazón moviendose, usamos la variable velocityShell para que sea más rápida
        if (shell)
        {
            k_rb.velocity = new Vector2(velocityShell, k_rb.velocity.y);
        }
        else
        {
            k_rb.velocity = new Vector2(velocity, k_rb.velocity.y);
        } 
    }

    //Método para parar el movimiento
    private void Stop()
    {
        k_rb.velocity = new Vector2(0, 0);
    }

    //Método para cambiar la dirección
    private void ChangeDirection()
    {
        //Si estamos modo caparazón moviéndose, usamos velocityShell
        if (shell)
        {
            this.velocityShell *= -1;
        }
        else
        {
            this.velocity *= -1;
        }
        
    }

    //Corrutina para dormir y despertar al Koopa
    private IEnumerator Shell()
    {
        float time = 0f;
        float duration = 5f;

        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null; 
        }

        moving = true;
        stomped = false;
    }

    //Evento de Colision con Mario cuando estamos modo Caparazón Estático
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Mario")
        {
            //Recogemos el primer punto de contacto
            contactPoint = collision.contacts[0].point;

            //Si es por la izquierda, movemos hacia la derecha
            if (contactPoint.x > k_rb.position.x)
            {
                m_rb.velocity = new Vector2(0, 10);
                shell = true;
            }
            else //Si es por la derecha, movemos hacia la izquierda
            {
                m_rb.velocity = new Vector2(0, 10);
                shell = true;
                this.velocityShell *= -1;
            }
        }
    }

    //Evento de colision con Trigger del Koopa para comenzar a moverse
    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.name == "Mario" && stomped == false)
        {
            moving = true;
        }
    }
}
