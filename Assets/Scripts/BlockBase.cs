using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour
{
    //Variables de los Bloques
    private bool isBouncing = false;
    private SpriteRenderer b_sr;
    protected Rigidbody2D b_rb;
    private bool isBlocked = false;

    //Propiedades de los Bloques
    [SerializeField]
    private Sprite b_sp_blocked;
    [SerializeField]
    private Sprite b_sp_brick;
    [SerializeField]
    public int blockType;

    //Recogemos las propiedades de los Bloques
    private void Awake()
    {
        b_sr = GetComponent<SpriteRenderer>();
    }

    //Método Hit de los Bloques
    public void Hit()
    {
        if (isBouncing == false && isBlocked != true)//Si NO estamos rebotando y NO estamos bloqueados
        {
            StartCoroutine(this.Bouncing());//Comenzamos la corrutina de rebote
        }
    }

    //Método protegido para aplicar el Sprite del Bloque
    protected void SetSprite()
    {
        switch (blockType)//Tipo de Bloque
        {
            case 0:
                b_sr.sprite = b_sp_blocked;//Sprite de Bloqueado
                isBlocked = true;//Estamos Bloqueados
            break;

            case 1:
                b_sr.sprite = b_sp_brick;//Sprite de Ladrillo
                isBlocked = false;//No estamos Bloqueados
            break;
        }
    }

    //Método de rebote de los bloques
    private IEnumerator Bouncing()
    {
        float time = 0f;//Parametro Tiempo
        float duration = 0.1f;//Parametro Duracion

        Vector2 startPosition = this.transform.position;//Guardamos la posición inicial
        Vector2 endPosition = (Vector2)this.transform.position + (Vector2.up * 0.5f);//La posicion final es media unidad hacia arriba de la inicial

        //Movemos el bloque hacia arriba
        while (time < duration)//Mientras el tiempo sea menor a la duración
        {
            this.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);//Nos transladamos hacia arriba en el tiempo Duracion
            time += Time.deltaTime;//Vamos sumando a Tiempo cada frame
            yield return null;//Nos salimos del bucle
        }

        this.transform.position = endPosition;//La posicion tiene que ser la posición final

        time = 0f;//Reseteamos el tiempo a 0

        //Movemos el bloque hacia abajo
        while (time < duration)//Mientras el tiempo sea menor a la duración
        {
            this.transform.position = Vector2.Lerp(endPosition, startPosition, time / duration);//Nos transladamos hacia abajo en el tiempo Duracion
            time += Time.deltaTime;//Vamos sumando a Tiempo cada frame
            yield return null;//Nos salimos del bucle
        }

        this.transform.position = startPosition;//La posicion tiene que ser la posición inicial

        isBouncing = false;//Ya no estamos rebotando
    }
}
