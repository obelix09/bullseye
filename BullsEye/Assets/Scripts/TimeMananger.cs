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
        if (!canCount)
        {
            timer = mainTimer;
            timeText.text = "Time: " + timer.ToString("F");
        }

        if (resetTimer)
        {
            resetTimer = false;
            canCount = true;
        }

        if (timer > 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            timeText.text = "Time: " + timer.ToString("F");
        }

        else if (timer <= 0.0f && canCount)
        {
            LevelManager.gameOver = true;
            timeText.text = "Time: 0.00";
            timer = 0.0f;
            canCount = false;
        }
    }
}