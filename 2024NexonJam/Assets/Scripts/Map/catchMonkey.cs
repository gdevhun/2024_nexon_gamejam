using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;


public class catchMonkey : MonoBehaviour
{
    DG.Tweening.Sequence startSequence;
    GameObject ball;
    SpriteRenderer crabSR;
    BallManager ballManager;

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
            ballManager = ball.GetComponent<BallManager>();
            startSequence.Append(monkeySequence());

        }
    }


    DG.Tweening.Sequence monkeySequence()
    {
        return DOTween.Sequence()
        .OnStart(() => {
            ball.SetActive(false);
            if (ballManager.LastPlayerType == PlayerType.Player2)
            {
                crabSR.sprite = ballType[1];
            }
            else
                crabSR.sprite = ballType[0];
        })
        .Append(transform.DOShakePosition(1.5f, 0.5f, 10, 5, false, true)) // 진동 효과
        .OnComplete(() => {
            ball.SetActive(true);
            LaunchBallWithDirection();
            this.GetComponent<SpriteRenderer>().sprite = original; // 세레모니 종료 후 원래 스프라이트로 복원
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
