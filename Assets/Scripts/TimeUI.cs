using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    //Variables del Temporizador
    [SerializeField]
    private float time;
    private Text text;
    [SerializeField]
    private int timeMult = 2;

    //Recogemos las propiedades del Temporizador
    private void Start()
    {
        text = this.GetComponent<Text>();
    }

    private void Awake()
    {
        time = GameManager.Instance.Timer;//Recogemos Timer del GameManager
    }

    void Update()
    {
        if (time < 100)//Cuando el temporizador sea menor de 100
        {
            timeMult = 4;//El tiempo pasa el doble de rápido que por defecto
        }

        if (SceneManager.GetActiveScene().name == "Main" && time > 0)
        {
            time -= Time.deltaTime * timeMult;// * timeMult;//Hacemos que cuente hacia atras a la velocidad de timeMult
        }

        text.text = time.ToString("000");//Formateamos el texto para mostrar 3 digitos en la UI
    }
}