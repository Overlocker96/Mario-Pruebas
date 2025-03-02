using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //Variables de la Cámara
    private Transform mario;
    
    //Recogemos las propiedades de la Cámara
    void Start()
    {
        mario = GameObject.Find("Mario").transform;
    }

    void Update()
    {
        if (mario.position.x >= transform.position.x)//Si la posicion de Mario es mayor o igual que la posicion de la Cámara
        {
            transform.position = new Vector3(mario.position.x, transform.position.y, transform.position.z);//La Cámara se mueve en el eje X con Mario
        }
        else//Si la posición no es menor que la posición de la Cámara
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);//La Cámara se queda estática
        }
    }
}
