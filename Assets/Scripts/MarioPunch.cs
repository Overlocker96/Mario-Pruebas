using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioPunch : MonoBehaviour
{
    //Evento de Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var blockScript = collision.GetComponent<Block>();//Variable de colision con el gameObject con componente "Block"

        if (blockScript != null)//Si existe la variable
        {
            blockScript.Hit();//Llamamos al método Hit de "Block"
        }
    }
}