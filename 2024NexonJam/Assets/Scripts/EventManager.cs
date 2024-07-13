using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject waveObj;
    public List<GameObject> waveChangeObj;
    WaitForSeconds _waitForSeconds =new WaitForSeconds(2.6f);
    WaitForSeconds _waitForSeconds2 =new WaitForSeconds(0.5f);
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            WaveOccur();
        }
    }

    public void WaveEvent()  //파도 기믹이벤트
    {
        
    }

    public void HelicopterEvent() //헬리톱터 이벤트
    {
        
    }

    public void CrabEvent() //꽃게 이벤트
    {
        
    }

    private void WaveOccur()
    {
        // 초기 위치로 설정
        waveObj.transform.localPosition = new Vector3(0, -20, 0);
        
        StartCoroutine(ChangeMap());
        
        waveObj.transform.DOMoveY(16, 2f).OnComplete(() =>
        {
            // 이동이 완료된 후 1초 대기하고 다시 원래 위치로 이동
            waveObj.transform.DOMoveY(-20, 2f).SetDelay(0.3f);
        });
        StartCoroutine(ResetMap());
    }

    private IEnumerator ChangeMap()
    {
        yield return _waitForSeconds2;
        foreach (var obj in waveChangeObj)
        {
            obj.SetActive(true);
        }
    }

    private IEnumerator ResetMap()
    {
        yield return _waitForSeconds; 
        
        foreach (var obj in waveChangeObj)
        {
            // 현재 알파값을 1로 설정하고 2초 동안 알파값을 0으로 감소
            obj.GetComponent<SpriteRenderer>().DOFade(0f, 1.2f).OnComplete(() =>
            {
                // 감소가 완료되면 비활성화하고 다시 알파값을 1로 설정
                obj.SetActive(false);
                obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // 알파값을 1로 설정
            });
        }
    }
}
