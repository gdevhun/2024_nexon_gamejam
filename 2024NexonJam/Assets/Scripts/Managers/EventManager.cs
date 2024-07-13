using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class EventManager : MonoBehaviour
{
    public Image fadeImage;
    public GameObject helicopter;
    
    public GameObject waveObj;
    public List<GameObject> waveChangeObj;
    
    readonly WaitForSeconds halfsec =new WaitForSeconds(0.5f);
    readonly WaitForSeconds onesec =new WaitForSeconds(1f);
    readonly WaitForSeconds twosec =new WaitForSeconds(2f);
    readonly WaitForSeconds twodothalfsec =new WaitForSeconds(2.5f);
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            WaveEvent();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HelicopterEvent();
        }
    }

    public void WaveEvent()  //파도 기믹이벤트
    {
        WaveOccur();
    }

    public void HelicopterEvent() //헬리톱터 이벤트
    {
        StartCoroutine(HelicopterRoutine());
        //fade는 1초에 걸쳐 255에서 0으로
        //끝나면 바로 헬리콥터는 2.5초에 걸쳐 
        //Vector3(12,2.5,0)
        //Vector3(-12,2.5,0) 로 이동
    }

    public void CrabEvent() //꽃게 이벤트
    {
        
    }

    private IEnumerator HelicopterRoutine()
    {
        // 페이드 인
        fadeImage.color = new Color(0, 0, 0, 0); // 페이드 이미지 초기화
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, 2.2f); // 2.2초 동안 페이드 인
        yield return onesec; 

        // 헬리콥터 이동
        helicopter.transform.localPosition = new Vector3(12, 2.5f, 0); // 초기 위치 설정
        helicopter.SetActive(true);
        helicopter.transform.DOMoveX(-12, 2f); // 2초 동안 이동
        yield return twodothalfsec; 

        // 페이드 아웃
        fadeImage.DOFade(0, 1.5f).OnComplete(() => fadeImage.gameObject.SetActive(false)); // 1.5초 동안 페이드 아웃
        yield return twosec; 
        helicopter.SetActive(false);
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
        yield return halfsec;
        foreach (var obj in waveChangeObj)
        {
            obj.SetActive(true);
        }
    }

    private IEnumerator ResetMap()
    {
        yield return twodothalfsec; 
        
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
