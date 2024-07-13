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

    private GameObject ball;
    private Rigidbody2D ballRb;

    private bool isSlow = false;

    private void Start()
    {
        ball = GameObject.FindWithTag("ball");
        ballRb = ball.GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        if (!isSlow)
        {
            float currentSpeed = ballRb.velocity.magnitude;

            if (currentSpeed < maxBallSpeed)
            {
                isSlow = true;
            }
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            if (isSlow)
            {
                ballRb.velocity = Vector2.zero;

                float directionX = (Random.value < 0.5f) ? 1f : -1f;
                float directionY = 0.8f;

                Vector2 randomDirection = new Vector2(directionX, directionY).normalized;
                Vector2 forceToAdd = randomDirection * speed;

                ballRb.AddForce(forceToAdd, ForceMode2D.Impulse);

                isSlow = false;
            }
        }
    }
 
}
