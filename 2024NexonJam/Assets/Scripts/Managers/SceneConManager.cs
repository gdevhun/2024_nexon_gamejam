using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneConManager : MonoBehaviour
{
    public static SceneConManager Instance;
    
    public Button startBtn;
    public Button exitBtn;
    //public Button MenuBtn;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance= this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        Screen.SetResolution(1920,1080,true);
        Application.targetFrameRate = 60; 
    }
    private void Start()
    {
        startBtn.onClick.AddListener(() => MoveScene("GameScene"));
        exitBtn.onClick.AddListener(()=>ExitGame());
    }

    //신 로드 함수
    public void MoveScene(string sceneName) => SceneManager.LoadScene(sceneName);
    
    //게임 종료 함수
    public void ExitGame() => Application.Quit();
}
