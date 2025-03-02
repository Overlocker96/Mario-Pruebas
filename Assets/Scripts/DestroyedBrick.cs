using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedBrick : MonoBehaviour
{
    //Propiedades del Bloque Destruido (Cada componente RigidBody2D de los trozos)
    [SerializeField]
    private Rigidbody2D Chunk_UL;
    [SerializeField]
    private Rigidbody2D Chunk_UR;
    [SerializeField]
    private Rigidbody2D Chunk_DL;
    [SerializeField]
    private Rigidbody2D Chunk_DR;

    private void Start()
    {
        GameManager.Instance.AddPoints();//Añadimos puntos

        Chunk_UL.velocity = new Vector2(-2f, 2f);//Aplicamos velocidad Arriba a la Izq
        Chunk_UR.velocity = new Vector2(2f, 2f);//Aplicamos velocidad Arriba a la Dch
        Chunk_DL.velocity = new Vector2(-2f, -2f);//Aplicamos velocidad Abajo a la Izq
        Chunk_DR.velocity = new Vector2(2f, -2f);//Aplicamos velocidad Abajo a la Dch

        Destroy(this.gameObject, 1);//Destruimos el GameObject y sus hijos en 1 segundo
    }
}
