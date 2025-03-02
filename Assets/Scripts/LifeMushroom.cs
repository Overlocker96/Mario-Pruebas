using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMushroom : MonoBehaviour
{
    //Propiedades de la Seta de Vida
    Rigidbody2D lm_rb;

    //Variables de la Seta de Vida
    [SerializeField]
    private float velocity;
    private bool moving = false;

    //Recogemos las variables de la Seta de Vida
    private void Awake()
    {
        lm_rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(this.Moving());//Comenzamos la corrutina de Movimiento al salir del bloque
    }

    //FixedUpdate para Físicas
    private void FixedUpdate()
    {
        if (moving)//Si nos estamos moviendo
        {
            if (Mathf.Abs(lm_rb.velocity.x) < 0.05f)//Si la velocidad es casi 0
            {
                ChangeDirection();//Cambiamos de dirección
            }

            lm_rb.velocity = new Vector2(velocity, lm_rb.velocity.y);//Aplicamos la velocidad
        }
    }

    //Método para Cambiar Dirección
    private void ChangeDirection()
    {
        this.velocity *= -1;
    }

    //Corrutina para Movimiento al salir del bloque
    private IEnumerator Moving()
    {
        float time = 0f;//Parametro Tiempo
        float duration = 0.1f;//Parametro duracion

        Vector2 startPosition = this.transform.position;//Guardamos la posición inicial
        Vector2 endPosition = (Vector2)this.transform.position + (Vector2.up * 2);//La posicion final es 2 unidades hacia arriba de la inicial

        // Movemos la Seta de Vida hacia arriba
        while (time < duration)//Mientras el tiempo sea menor a la duración
        {
            this.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);//Nos transladamos en el tiempo Duracion
            time += Time.deltaTime;//Vamos sumando a Tiempo cada frame
            yield return null;//Nos salimos del bucle
        }

        this.transform.position = endPosition;//La posicion tiene que ser la posición final

        time = 0f;//El tiempo lo volvemos a poner a 0
        moving = true;//Nos comenzamos a mover normal
    }
}
