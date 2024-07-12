using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflect : MonoBehaviour
{
    private Rigidbody2D rb; // 2D 물리 엔진을 위한 Rigidbody2D 사용
    public float forcePower = 10f;
    public float forceDirection = 80f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트를 가져옴
        
        // 방향을 라디안으로 변환
        float directionInRadians = forceDirection * Mathf.Deg2Rad;
        
        // 힘의 방향 벡터 계산 (Vector2 사용)
        Vector2 forceVector = new Vector2(Mathf.Cos(directionInRadians), Mathf.Sin(directionInRadians)) * forcePower;
        
        // 힘을 추가
        rb.AddForce(forceVector, ForceMode2D.Impulse);
    }
}