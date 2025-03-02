using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(this.Moving());//Comenzamos la Corrutina de Movimiento desde el Bloque
    }

    //Corrutina de movimiento desde el bloque
    private IEnumerator Moving()
    {
        float time = 0f;//Parametro Tiempo
        float duration = 2f;//Parametro Duracion

        Vector2 startPosition = this.transform.position;//Guardamos la posición inicial
        Vector2 endPosition = (Vector2)this.transform.position + (Vector2.up * 1);//La posicion final es 1 unidades hacia arriba de la inicial

        // Movemos la flor hacia arriba
        while (time < duration)//Mientras el tiempo sea menor a la duración
        {
            this.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);//Nos transladamos en el tiempo Duracion
            time += Time.deltaTime;//Vamos sumando a Tiempo cada frame
            yield return null;//Nos salimos del bucle
        }

        this.transform.position = endPosition;//La posicion tiene que ser la posición final

        time = 0f;//El tiempo lo volvemos a poner a 0
    }
}
