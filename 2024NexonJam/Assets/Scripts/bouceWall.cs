using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceWall : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 10f)] 
    float speed = 3f;

    [SerializeField]
    float maxBallSpeed = 5f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (ballRb != null)
            {
                // 현재 공의 속도의 크기를 구합니다.
                float currentSpeed = ballRb.velocity.magnitude;

                // 만약 공의 속도가 일정 속도(maxBallSpeed) 이하라면 힘을 추가합니다.
                if (currentSpeed < maxBallSpeed)
                {
                    ballRb.velocity = Vector2.zero;
                    // 랜덤으로 1 또는 -1을 선택하여 방향을 설정합니다.
                    float directionX = (Random.value < 0.5f) ? 1f : -1f;
                    float directionY = 0.8f;

                    Vector2 randomDirection = new Vector2(directionX, directionY).normalized;
                    Vector2 forceToAdd = randomDirection * speed;

                    ballRb.AddForce(forceToAdd, ForceMode2D.Impulse);
                }
            }
        }
    }
}
