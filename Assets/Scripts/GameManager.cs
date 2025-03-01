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

    // Versiones Públicas de todas las variables necesarias para poder acceder desde fuera sin tocar las privadas
    public int Timer { get { return timer; } }

    public int Lives { get { return lives; } }

    public int Points { get { return points; } }

    public int Coins { get { return coins; } }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //Método Coin para Sumar Monedas
    public void Coin()
    {
        coins++;
    }

    //Método ResetCoins para resetear Monedas a 0
    public void ResetCoins()
    {
        coins = 0;
    }

    //Metodo Life para Sumar Vidas
    public void Life()
    {
        lives++;
    }

    //Método ResetTimer para resetear el Temporizador a 300
    public void ResetTimer()
    {
        timer = 300;
    }

    //Método LessLife para Quitar Vidas
    public void LessLife()
    {
        lives--;
    }

    //Método AddPoints para Dar Puntos
    public void AddPoints()
    {
        points += 100;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameStart" && Input.GetKeyDown("space"))
        {
            //StartCoroutine(GameOverTime());
            ResetTimer();
            SceneManager.LoadScene("Main");
        }

        if (timer == 0)
        {
            GameOver();
        }

        if (lives == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void GameOver()
    {
        if(SceneManager.GetActiveScene().name == "Main")
        {
            LessLife();
            ResetCoins();
            SceneManager.LoadScene("GameStart");
        }
    }
}
