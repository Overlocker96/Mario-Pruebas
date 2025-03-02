using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    //Propiedades del Stomp
    Animator s_anim;

    //Variables del Stomp
    private bool stomped;

    //Aplicamos propiedades del Stomp al llamar al objeto por primera vez en la escena
    private void Awake()
    {
        stomped = false;
    }

    //LateUpdate para Animaciones
    private void LateUpdate()
    {
        s_anim = GetComponentInParent<Animator>();
        s_anim.SetBool("Stomp", stomped);
    }

    //Evento de Trigger para cuando colisiona con Mario
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Mario")
        {
            collider.attachedRigidbody.velocity = new Vector2(0, 10);//Aplicamos una pequeña velocidad hacia arriba a Mario cuando colisiona
            stomped = true;//Pasamos a Stomped
            GetComponent<Collider2D>().enabled = false;//Desactivamos el Collider
            GameManager.Instance.AddPoints();//Y Añadimos puntos al GameManager
        }
    }
}