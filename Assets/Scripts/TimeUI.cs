using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private int time;
    private Text text;
    [SerializeField]
    private int timeMult = 2;

    void Start()
    {
        text = this.GetComponent<Text>();
    }

    void Update()
    {
        time = GameManager.Instance.Timer;
        time -= (int)Time.deltaTime * timeMult;
        text.text = time.ToString("000");

        if(time < 100)
        {
            timeMult = 4;
        }
    }
}
