using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(this.Bouncing());//Comenzamos la corrutina de rebote de la moneda nada más Instanciarla
    }

    //Corrutina rebote para la moneda
    private IEnumerator Bouncing()
    {
        float time = 0f;//Parametro Tiempo
        float duration = 0.2f;//Parametro Duracion

        Vector2 startPosition = this.transform.position;//Guardamos la posición inicial
        Vector2 endPosition = (Vector2)this.transform.position + (Vector2.up * 3);//La posicion final es 3 unidades hacia arriba de la inicial

        //Movemos la moneda hacia arriba
        while (time < duration)//Mientras el tiempo sea menor a la duración
        {
            this.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);//Nos transladamos hacia arriba en el tiempo Duracion
            time += Time.deltaTime;//Vamos sumando a Tiempo cada frame
            yield return null;//Nos salimos del bucle
        }

        this.transform.position = endPosition;//La posicion tiene que ser la posición final

        time = 0f;//Reseteamos el tiempo a 0

        //Movemos la moneda hacia abajo
        while (time < duration)//Mientras el tiempo sea menor a la duración
        {
            this.transform.position = Vector2.Lerp(endPosition, startPosition, time / duration);//Nos transladamos hacia abajo en el tiempo Duracion
            time += Time.deltaTime;//Vamos sumando a Tiempo cada frame
            yield return null;//Nos salimos del bucle
        }

        this.transform.position = startPosition;//La posicion tiene que ser la posición inicial

        Destroy(this.gameObject);//Destruimos el GameObject Moneda
    }
}