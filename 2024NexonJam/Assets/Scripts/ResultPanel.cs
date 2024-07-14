using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{
    public TextMeshProUGUI resultScore;
    void Start()
    {
        resultScore.text = "Score : " + GameManager.Instance.winPlayerScore;
    }

    public void MoveToMenuScene()
    {
        SceneConManager.Instance.MoveScene("MenuScene");
        SoundManager.Instance.PlaySfx(SoundType.버튼클릭음sfx);
        Destroy(GameManager.Instance);
    }
}
