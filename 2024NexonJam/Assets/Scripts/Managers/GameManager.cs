using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlayerType
{
    None,
    Player1,
    Player2
}

public class GameManager : MonoBehaviour
{
    public GameObject eventManager;
    
    public TextMeshProUGUI timerText;
    public int headSecond;
    
    public Image timeGauge;
    private float elapsedTime;
    private int previousSecond;
    private bool _isGameEnded;
    private void Start()
    {
        elapsedTime = 0f;
        previousSecond = 0;
        UpdateTimerText();
    }

    private void Update()
    {
        if (_isGameEnded) return;
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
    {   //플레이 타임 총 160초
        if (seconds == 40 || seconds == 80 || seconds == 120)
        {
            // 40,80,120 초 도달
            int randomIdx = Random.Range(0, 1);

            if (randomIdx == 1)   //렌덤으로 호출
            {
                eventManager.GetComponent<EventManager>().CrabEvent();
            }
            else
            {
                eventManager.GetComponent<EventManager>().WaveEvent();
            }
            Debug.Log(seconds+"random"+randomIdx);
        }
        else if (seconds == 60 || seconds == 100)
        {   //60,90초 도달시 헬리콥터
            eventManager.GetComponent<EventManager>().HelicopterEvent();
            Debug.Log(seconds+"헬리콥터");
        }
        else if (seconds > 159)
        {   //90초 넘으면 게이지 초기화
            //게임 종료
            _isGameEnded = true;
            elapsedTime = 0f;
            previousSecond = 0;
        }
    }

    private void UpdateTimerText()
    {
        int displaySeconds = (int)elapsedTime;
        timerText.text = displaySeconds.ToString();
        timeGauge.fillAmount = elapsedTime / 160f;
    }
}
