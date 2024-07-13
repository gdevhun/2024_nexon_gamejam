using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class catchMonkey : MonoBehaviour
{
    DG.Tweening.Sequence startSequence;
    GameObject ball;
    SpriteRenderer ballSR;
    SpriteRenderer crabSR;

    public Sprite ballHipSprite;

    public Sprite[] ballType;
    private Sprite original;

    // Start is called before the first frame update
    void Start()
    {
        startSequence = DOTween.Sequence();
        crabSR = GetComponent<SpriteRenderer>();
        original = this.GetComponent<SpriteRenderer>().sprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            ball = collision.gameObject;
            ballSR = collision.GetComponent<SpriteRenderer>();
            startSequence.Append(monkeySequence());

        }
    }


    DG.Tweening.Sequence monkeySequence()
    {
        return DOTween.Sequence()
        .OnStart(() => {
            ball.SetActive(false);
            if (ballSR.sprite == ballHipSprite)
            {
                crabSR.sprite = ballType[0];
            }
            else
                crabSR.sprite = ballType[1];
        })
        .Append(transform.DOShakePosition(1.5f, 0.5f, 10, 5, false, true)) // ���� ȿ��
        .OnComplete(() => {
            ball.SetActive(true);
            LaunchBallWithDirection();
            this.GetComponent<SpriteRenderer>().sprite = original; // ������� ���� �� ���� ��������Ʈ�� ����
        });
    }

    void LaunchBallWithDirection()
    {
        ball.transform.position = Vector2.zero;
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.8f, -0.5f)).normalized;

        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        Vector2 direction = randomDirection * 6f;
        ballRb.AddForce(direction, ForceMode2D.Impulse);
    }
}
