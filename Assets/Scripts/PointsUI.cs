using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointsUI : MonoBehaviour
{
    private int points;
    private Text text;

    void Start()
    {
        points = 0;
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        text.text = points.ToString("000000");
    }
}
