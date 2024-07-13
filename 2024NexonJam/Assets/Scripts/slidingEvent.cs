using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;

public class slidingEvent : MonoBehaviour
{
    GameObject slidingBall;
    Rigidbody2D slidingRb;
    public bool isLeft = true;

    public Transform hole;
    public Transform snail;
    private float dir = 1f;
    private bool isSliding = false;

    // Start is called before the first frame update
    void Start()
    {
        dir = isLeft ? 1f : -1f;

        flipPos(hole);
        flipPos(snail);

        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        //만약 왼쪽 슬라이드가 아니라면 hole 위치 좌우반전해줌
    }

    void flipPos(Transform transform)
    {
        Vector3 position = transform.position;
        position.x = position.x * dir;
        transform.position = position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ball") && !isSliding)
        {
            slidingBall = collision.gameObject;
            slidingRb = slidingBall.GetComponent<Rigidbody2D>();
            slidingBall.layer = 8;
            isSliding = true;
            StartCoroutine(startSliding());
        }
    }

    IEnumerator startSliding()
    {
        //그러니까 slidingball이 layer slidingBall인 동안
        while (isSliding)
        {
            slidingRb.velocity = Vector3.zero; //그 구멍에 멈춰세우고
            slidingRb.gravityScale = 0f;
            slidingBall.transform.position = hole.position;
            slidingBall.transform.DOScale(new Vector3(0.3f, 0.3f, 0f), 1f);
            yield return new WaitForSeconds(0.2f);
            slidingBall.SetActive(false);

            //포지션 snail쪽으로 옮겨주고
            slidingBall.transform.position = snail.position;

            //켜주고
            slidingBall.SetActive(true);
            slidingBall.transform.DOScale(new Vector3(0.8f, 0.8f, 0f), 0.5f);
            yield return new WaitForSeconds(0.2f);

           
            slidingBall.layer = 7;
            slidingRb.gravityScale = 4f;
            isSliding = false;
        }
    }

 
}