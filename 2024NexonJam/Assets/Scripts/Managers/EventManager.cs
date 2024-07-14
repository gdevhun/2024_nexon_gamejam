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
    public List<GameObject> itemList;
    
    public GameObject waveObj;
    public List<GameObject> waveChangeObj;

    public List<GameObject> crabObj;
    
    readonly WaitForSeconds halfsec =new WaitForSeconds(0.5f);
    readonly WaitForSeconds onesec =new WaitForSeconds(1f);
    readonly WaitForSeconds twosec =new WaitForSeconds(2f);
    readonly WaitForSeconds twodothalfsec =new WaitForSeconds(2.5f);
    
    /*  //FOR TEST
     private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            WaveEvent();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HelicopterEvent(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CrabEvent();
        }
    }*/

    public void WaveEvent() => WaveOccur();  //파도 기믹이벤트
    public void HelicopterEvent(int idx) => StartCoroutine(HelicopterRoutine(idx));//헬리톱터 이벤트
    public void CrabEvent() =>CrabsMove(); //꽃게 이벤트

    private IEnumerator HelicopterRoutine(int idx)
    {
        SoundManager.Instance.PlaySfx(SoundType.헬리콥터기믹sfx);
        // 페이드 인
        fadeImage.color = new Color(0, 0, 0, 0); // 페이드 이미지 초기화
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, 2.2f); // 2초 동안 페이드 인
        yield return halfsec; 

        // 헬리콥터 이동
        helicopter.transform.localPosition = new Vector3(12, 2.5f, 0); // 초기 위치 설정
        helicopter.SetActive(true);
        helicopter.transform.DOMoveX(-12, 2f); // 2초 동안 이동
        yield return twosec; 
        
        if (idx == 1)
        {
            itemList[0].SetActive(true);
        }
        else if (idx == 2)
        {
            itemList[1].SetActive(true);
        }

        
        // 페이드 아웃
        fadeImage.DOFade(0, 1.5f).OnComplete(() => fadeImage.gameObject.SetActive(false)); // 1.5초 동안 페이드 아웃
        yield return twosec; 
        helicopter.SetActive(false);
        
    }
    private void WaveOccur()
    {
        SoundManager.Instance.PlaySfx(SoundType.파도기믹sfx);
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

    private void CrabsMove()
    {
        SoundManager.Instance.PlaySfx(SoundType.꽃게기믹2sfx);
        // leftCrab 이동 (2초 동안)
        crabObj[0].transform.localPosition = new Vector3(-15, 1.86f, 0); // 초기 위치 설정
        crabObj[0].SetActive(true);
        crabObj[0].transform.DOMove(new Vector3(-9.3f, 1.86f, 0), 2f).OnComplete(() =>
        {
            // 1초 대기 후 원래 위치로 돌아가기
            SoundManager.Instance.PlaySfx(SoundType.꽃게기믹sfx);
            StartCoroutine(WaitAndMoveBack(crabObj[0], new Vector3(-15, 1.86f, 0), 2f));
        });
        SoundManager.Instance.PlaySfx(SoundType.꽃게기믹2sfx);
        // rightCrab 이동 (2.5초 동안)
        crabObj[1].transform.localPosition = new Vector3(8.5f, 2f, 0); // 초기 위치 설정
        crabObj[1].SetActive(true);
        crabObj[1].transform.DOMove(new Vector3(0.68f, 2.9f, 0), 2.5f).OnComplete(() =>
        {
            // 1초 대기 후 원래 위치로 돌아가기
            StartCoroutine(WaitAndMoveBack(crabObj[1], new Vector3(8.5f, 2f, 0), 2.5f));
            SoundManager.Instance.PlaySfx(SoundType.꽃게기믹sfx);
        });
    }

    private IEnumerator WaitAndMoveBack(GameObject crab, Vector3 originalPosition, float duration)
    {
        yield return onesec; // 1초 대기
        crab.transform.DOMove(originalPosition, duration);
    }
}
