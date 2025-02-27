using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instanciamos el GameManager
    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = "GameManagerSingleton";
                instance = gameObject.AddComponent<GameManager>();

                DontDestroyOnLoad(Instance.gameObject);
            }
            return instance;
        }
    }

    //Variables que vamos a usar

    //Tiempo iniciado a 300
    private int timer = 300;

    //Vidas iniciadas a 3
    private int lives = 3;

    //Puntos iniciados a 0
    private int points = 0;

    //Monedas iniciadas a 0
    private int coins = 0;

    // Versiones Públicas de todas las variables necesarias para poder acceder desde fuera si tocar las privadas
    public int Timer { get { return timer; } }

    public int Lives { get { return lives; } }

    public int Points { get { return points; } }

    public int Coins { get { return coins; } }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Coin()
    {
        coins++;
    }

    public void Live()
    {
        lives++;
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    public void LessLife()
    {
        lives--;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            ResetTimer();
            StartCoroutine(GameOverTime());
        }

        if (lives == 0)
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    public void GameOver()
    {
        if(SceneManager.GetActiveScene().name == "Main")
        {
            LessLife();
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator GameOverTime()
    {
        yield return new WaitForSeconds(5f);
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            SceneManager.LoadScene("Main");
        }
    }
}
