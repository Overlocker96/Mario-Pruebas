using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //Variables de la C�mara
    private Transform mario;
    
    //Recogemos las propiedades de la C�mara
    void Start()
    {
        mario = GameObject.Find("Mario").transform;
    }

    void Update()
    {
        if (mario.position.x >= transform.position.x)//Si la posicion de Mario es mayor o igual que la posicion de la C�mara
        {
            transform.position = new Vector3(mario.position.x, transform.position.y, transform.position.z);//La C�mara se mueve en el eje X con Mario
        }
        else//Si la posici�n no es menor que la posici�n de la C�mara
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);//La C�mara se queda est�tica
        }
    }
}
