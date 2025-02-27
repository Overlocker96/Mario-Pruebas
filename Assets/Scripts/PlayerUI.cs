using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private Text text;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        if (Player.name == "Mario")
        {
            text.text = "Mario";
        }
        else if (Player.name == "Luigi")
        {
            text.text = "Luigi";
        }
        else
        {
            text.text = "Unknown";
        }
    }
}
