using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private float time;
    private Text text;
    [SerializeField]
    private int velocity = 2;

    void Start()
    {
        time = 300;
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        time -= Time.deltaTime * velocity;
        text.text = time.ToString("000");

        if(time < 100)
        {
            velocity = 4;
        }
    }
}
