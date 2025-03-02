using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instanciamos el GameManager
    public static GameManager instance;

    //Nos aseguramos que SIEMPRE tengamos un GameManager en la Escena
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = "GameManager";
                instance = gameObject.AddComponent<GameManager>();

                DontDestroyOnLoad(Instance.gameObject);
            }
            return instance;
        }
    }

    //Variables que vamos a usar

    //Tiempo iniciado a 300
    private float timer = 300f;

    //Vidas iniciadas a 3
    private int lives = 3;

    //Puntos iniciados a 0
    private int points = 0;

    //Monedas iniciadas a 0
    private int coins = 0;

    // Versiones Públicas de todas las variables necesarias para poder acceder desde fuera sin tocar las privadas
    public float Timer { get { return timer; } }

    public int Lives { get { return lives; } }

    public int Points { get { return points; } }

    public int Coins { get { return coins; } }

    //GameManager nunca se destruye
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
        if (SceneManager.GetActiveScene().name == "GameStart" && Input.GetKeyDown("space"))//Si estamos en la Escena "GameStart" y pulsamos "Space"
        {
            ResetTimer();//Llamamos al método ResetTimer
            SceneManager.LoadScene("Main");//Cambiamos a la Escena "Main"
        }

        if (SceneManager.GetActiveScene().name == "Main" && timer == 0)//Si el Timer llega a 0
        {
            GameOver();//Llamamos al método GameOver
        }

        if (SceneManager.GetActiveScene().name == "Main" && lives == 0)//Si las vidas llegan a 0
        {
            SceneManager.LoadScene("GameOver");//Cambiamos a la Escena "GameOver"
        }
    }

    //Método GameOver
    public void GameOver()
    {
        if(SceneManager.GetActiveScene().name == "Main")//Si la Escena es "Main"
        {
            LessLife();//Llamamos al método LessLife
            ResetCoins();//Llamamos al método ResetCoins
            SceneManager.LoadScene("GameStart");//Cambiamos a la Escena "GameStart"
        }
    }

    //Método GameWin
    public void GameWin()
    {
        SceneManager.LoadScene("GameWin");//Cambiamos a la Escena "GameWin"
    }
}
