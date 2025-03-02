using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointsUI : MonoBehaviour
{
    //Variables de la UI Puntos
    private Text text;

    //Recogemos propiedades de la UI Puntos
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        text.text = GameManager.Instance.Points.ToString("000000");//Damos formato al texto pra que se muestren los Puntos con 6 dígitos
    }
}
