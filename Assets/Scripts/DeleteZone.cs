using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteZone : MonoBehaviour
{
    //Evento de Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Goomba>() || collision.gameObject.GetComponent<Koopa>())//Si en la zona entra un Goomba o un Koopa
        {
            Destroy(collision.gameObject);//Destruimos el GameObject
        }

        if (collision.gameObject.GetComponent<Player>())//Si entra en la zona el Jugador
        {
            GameManager.Instance.GameOver();//Llamamos al Método GameOver del GameManager
        }
    }
}