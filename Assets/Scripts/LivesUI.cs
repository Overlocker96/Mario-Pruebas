using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        text.text = "x " + GameManager.Instance.Lives.ToString("00");
    }
}
