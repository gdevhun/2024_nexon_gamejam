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

        //���� ���� �����̵尡 �ƴ϶�� hole ��ġ �¿��������
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
        //�׷��ϱ� slidingball�� layer slidingBall�� ����
        while (isSliding)
        {
            slidingRb.velocity = Vector3.zero; //�� ���ۿ� ���缼���
            slidingRb.gravityScale = 0f;
            slidingBall.transform.position = hole.position;
            slidingBall.transform.DOScale(new Vector3(0.3f, 0.3f, 0f), 1f);
            yield return new WaitForSeconds(0.2f);
            slidingBall.SetActive(false);

            //������ snail������ �Ű��ְ�
            slidingBall.transform.position = snail.position;

            //���ְ�
            slidingBall.SetActive(true);
            slidingBall.transform.DOScale(new Vector3(0.8f, 0.8f, 0f), 0.5f);
            yield return new WaitForSeconds(0.2f);

           
            slidingBall.layer = 7;
            slidingRb.gravityScale = 4f;
            isSliding = false;
        }
    }

 
}