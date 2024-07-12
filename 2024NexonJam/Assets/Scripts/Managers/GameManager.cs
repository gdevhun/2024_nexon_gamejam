using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
public enum PlayerType
{
    Player1,
    Player2
}

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public int headSecond;
    
    public Image timeGauge;
    private float elapsedTime;
    private int previousSecond;

    private void Start()
    {
        elapsedTime = 0f;
        previousSecond = 0;
        UpdateTimerText();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        int currentSecond = (int)elapsedTime;

        if (currentSecond != previousSecond)
        {
            previousSecond = currentSecond;
            CheckMilestones(currentSecond);
            UpdateTimerText();
        }
    }

    private void CheckMilestones(int seconds)
    {
        if (seconds == 30 || seconds == 60 || seconds == 90)
        {
            Debug.Log($"{seconds}");
            // 30,60,90 초 도달
        }
        else if (seconds > 90)
        {   //90초 넘으면 게이지 초기화
            elapsedTime = 0f;
            previousSecond = 0;
        }
    }

    private void UpdateTimerText()
    {
        int displaySeconds = (int)elapsedTime;
        timerText.text = displaySeconds.ToString();
        timeGauge.fillAmount = elapsedTime / 90f;
    }
}
