using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeRemaining = 180;
    bool timerIsRunning = false;

    GameObject minutes0;
    GameObject minutes1;
    GameObject seconds0;
    GameObject seconds1;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        minutes0 = GameObject.Find("Minutes0");
        minutes1 = GameObject.Find("Minutes1");
        seconds0 = GameObject.Find("Seconds0");
        seconds1 = GameObject.Find("Seconds1");

        Debug.Log(minutes0);
        Debug.Log(minutes1);
        Debug.Log(seconds0);
        Debug.Log(seconds1);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timeRemaining -= Time.deltaTime;
                if (minutes > 10)
                {
                    minutes0.GetComponent<TextMeshPro>().text = minutes.ToString().Substring(0, 1);
                    minutes1.GetComponent<TextMeshPro>().text = minutes.ToString().Substring(1, 1);
                }
                else if (minutes < 10)
                {
                    minutes1.GetComponent<TextMeshPro>().text = minutes.ToString().Substring(0, 1);
                    minutes0.GetComponent<TextMeshPro>().text =  "0";
                }

                if (seconds > 10)
                {
                    seconds0.GetComponent<TextMeshPro>().text = seconds.ToString().Substring(0, 1);
                    seconds1.GetComponent<TextMeshPro>().text = seconds.ToString().Substring(1, 1);
                }
                else if (seconds < 10)
                {
                    seconds0.GetComponent<TextMeshPro>().text = "0";
                    seconds1.GetComponent<TextMeshPro>().text = seconds.ToString().Substring(0, 1);
                }
            }
        }
        else
        {

            timeRemaining = 0;
            timerIsRunning = false;
        }
    }
}

