using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class holder : MonoBehaviour
{
    public PlayerType playerType;
    private Transform _transform;
    
    private Vector3 targetPosition;
    private float moveSpeed = 2.0f; // 이동 속도 조절

    private void Start()
    {
        _transform = transform;
        targetPosition = _transform.localPosition;
    }

    void Update()
    {
        if (playerType == PlayerType.Player1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                MoveUp();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                MoveDown();
            }
        }
        else if (playerType == PlayerType.Player2)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveUp();
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveDown();
            }
        }

        // Lerp를 사용하여 부드럽게 이동
        _transform.localPosition = Vector3.Lerp(_transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void MoveUp()
    {
        if (targetPosition.y <= 1.5f)
        {
            targetPosition += new Vector3(0, 0.1f, 0);
        }
    }

    private void MoveDown()
    {
        if (targetPosition.y >= -1.5f)
        {
            targetPosition += new Vector3(0, -0.1f, 0);
        }
    }
}
