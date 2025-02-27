using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointsUI : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        text.text = GameManager.Instance.Points.ToString("000000");
    }
}
