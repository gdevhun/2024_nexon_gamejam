using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class SceneConManager : SingletonBehaviour<SceneConManager>
{
    void Awake()
    {
        base.Awake();
        //게임 환경 기본 설정
        Screen.SetResolution(1920,1080,true);
        Application.targetFrameRate = 60; 
    }
    
    //신 로드 함수
    public void MoveScene(string sceneName) => SceneManager.LoadScene(sceneName);
    
    //게임 종료 함수
    public void ExitGame() => Application.Quit();
}
