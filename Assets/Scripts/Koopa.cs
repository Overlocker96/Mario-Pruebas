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

    //Fixed Update para F�sicas
    private void FixedUpdate()
    {
        //Movimiento cuando no est� modo caparaz�n
        if (moving && !stomped && !shell)
        {
            StopCoroutine(this.Shell());
            Move();
        }

        //Paramos movimientos cuando est� en modo caparaz�n est�tico
        if (!moving && stomped && !shell)
        {
            Stop();
            moving = false;
            StartCoroutine(this.Shell());
        }

        //Si entramos en modo caparaz�n moviendose, nos movemos
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

    //M�todo Move para el movimiento
    private void Move()
    {
        //Cambiamos la direcci�n cuando la velocidad es casi 0
        if (Mathf.Abs(k_rb.velocity.x) < 0.05f)
        {
            ChangeDirection();
        }

        //Si est�mos modo caparaz�n moviendose, usamos la variable velocityShell para que sea m�s r�pida
        if (shell)
        {
            k_rb.velocity = new Vector2(velocityShell, k_rb.velocity.y);
        }
        else
        {
            k_rb.velocity = new Vector2(velocity, k_rb.velocity.y);
        } 
    }

    //M�todo para parar el movimiento
    private void Stop()
    {
        k_rb.velocity = new Vector2(0, 0);
    }

    //M�todo para cambiar la direcci�n
    private void ChangeDirection()
    {
        //Si estamos modo caparaz�n movi�ndose, usamos velocityShell
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

    //Evento de Colision con Mario cuando estamos modo Caparaz�n Est�tico
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
