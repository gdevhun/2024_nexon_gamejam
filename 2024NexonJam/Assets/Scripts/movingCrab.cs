using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingCrab : MonoBehaviour
{
    public float speed = 2.0f;
    public float distance = 3.0f; 

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = transform.position.x - startPosition.x;

        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (offset >= distance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (offset <= -distance)
            {
                movingRight = true;
            }
        }
    }
}
