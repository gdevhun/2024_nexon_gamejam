using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine.PlayerLoop;

public class slidingEvent : MonoBehaviour
{
    GameObject slidingBall;
    Rigidbody2D slidingRb;
    public bool isLeft = true;

    public Transform hole;
    public Transform snail;

    private float dir = 1f;
    private bool isSliding = false;
    SpriteRenderer spriteRenderer;

    private float timeInTrigger = 0f;
    private bool isInTrigger = false;

    private int originalLayer;
    // Start is called before the first frame update
    void Start()
    {
        dir = isLeft ? 1f : -1f;


        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        //���� ���� �����̵尡 �ƴ϶�� hole ��ġ �¿��������
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ball") && !isSliding)
        {
            slidingBall = collision.gameObject;
            slidingRb = slidingBall.GetComponent<Rigidbody2D>();
            spriteRenderer = slidingBall.GetComponent<SpriteRenderer>();
            originalLayer = slidingBall.gameObject.layer;

            //�ð� ��� ���� 
            isInTrigger = true;
            timeInTrigger = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            isInTrigger = false;
            timeInTrigger = 0f;
        }

    }

    void Update()
    {
        if (isInTrigger)
        {
            timeInTrigger += Time.deltaTime;
            if (timeInTrigger >= 0.15f && !isSliding)
            {
                isSliding = true;
                StartCoroutine(startSliding());
            }
        }
    }

    IEnumerator startSliding()
    {
        //�׷��ϱ� slidingball�� layer slidingBall�� ����
        while (isSliding)
        {
            yield return new WaitForSeconds(0.2f);
            slidingRb.velocity = Vector3.zero; //�� ���ۿ� ���缼���
            slidingRb.gravityScale = 0f;
            slidingBall.layer = 8;
            slidingBall.transform.position = new Vector3(hole.position.x*dir, hole.position.y, hole.position.z);
            spriteRenderer.sortingOrder = 5;

            slidingBall.transform.DOScale(new Vector3(0.3f, 0.3f, 0f), 0.5f);
            yield return new WaitForSeconds(0.2f);
            slidingBall.SetActive(false);

            //������ snail������ �Ű��ְ�
            slidingBall.transform.position = new Vector3(snail.position.x * dir, snail.position.y, snail.position.z);
            yield return new WaitForSeconds(0.5f);

            //���ְ�
            slidingBall.SetActive(true);
            slidingBall.transform.DOScale(new Vector3(0.8f, 0.8f, 0f), 0.5f);
            slidingRb.gravityScale = 4f;
            yield return new WaitForSeconds(2f);

           
            isSliding = false;
            slidingBall.layer = originalLayer;
            spriteRenderer.sortingOrder = 3;

        }
    }

 
}