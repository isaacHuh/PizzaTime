using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI : MonoBehaviour
{
    public TMP_Text timer;
    float seconds = 10;
    float miliseconds = 0;

    void Update()
    {
        if(seconds > 0)
        {
            CountDown();
        }
    }

    void CountDown()
    {
        if (miliseconds <= 0)
        {
            seconds--;
            miliseconds = 100;
        }
        if (seconds == 0)
        {
            miliseconds = 0;
            timer.text = string.Format("{0}:{1}", seconds, (int)miliseconds);
            return;
        }

        miliseconds -= Time.deltaTime * 100;
        timer.text = string.Format("{0}:{1}", seconds, (int)miliseconds);
    }

}