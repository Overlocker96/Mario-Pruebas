using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    //Variables de la UI Monedas
    private Text text;

    //Recogemos las propiedades de la UI Monedas
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        text.text = "x " + GameManager.Instance.Coins.ToString("00");//Damos formato para que siempre muestre "x CantidadMonedas" con dos dígitos
    }
}
