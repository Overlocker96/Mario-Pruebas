using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    private int coins;
    private Text text;

    void Start()
    {
        coins = 0;
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        text.text = coins.ToString("00");
    }
}
