using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMananger : MonoBehaviour
{

    [SerializeField] Text timeText;
    [SerializeField] public static float mainTimer;
    private float timer;
    public static bool resetTimer;
    public static bool canCount;
    public static bool timeFinished;

    // Use this for initialization
    void Start()
    {
        mainTimer = 10f;
        timeText = GetComponent<Text>();
        timer = mainTimer;
    }

    // Update is called once per frame
    void Update()
    {

        if (!canCount && !timeFinished)
        {
            timer = mainTimer;
            timeText.text = "Time: " + timer.ToString("F");                     //can count down again
        }

        if (resetTimer)                                                         //resets the timer
        {
            resetTimer = false;                                                 //dont have to reset again
            canCount = true;
            timeFinished = false;
        }

        if (timer > 0.0f && canCount)                                           //if time is not done
        {
            timer -= Time.deltaTime;                                            //keep decreasing time left
            timeText.text = "Time: " + timer.ToString("F");                     //display the time
        }

        else if (timer <= 0.0f && canCount)                                     //time is done
        {
            LevelManager.gameOver = true;                                       //game over
            timeText.text = "Time: 0.00";                                       //show time as 0.00
            timer = 0.0f;                                                       //timer is 0.0f
            canCount = false;                                                   //cant count anymore
            timeFinished = true;
        }
    }
}