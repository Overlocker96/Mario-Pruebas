using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Goomba>())
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.GetComponent<Player>())
        {
            SceneManager.LoadScene("Main");
        }
    }
}