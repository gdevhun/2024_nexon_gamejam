using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public PlayerType LastPlayerType;
    public float throwingForce = 10f;
    

    private Rigidbody2D ballRb;
    private GameObject lineObject;
    private GameObject gaugeBack;
    private Transform lineTransform;


    enum BallState
    {
        WAITING,
        ROLLING,
        GOAL
    }

    [SerializeField] private BallState ballState;
    
    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        ballState = BallState.WAITING;
        LastPlayerType = PlayerType.None;

        lineObject = transform.Find("Line").gameObject;
        gaugeBack = transform.Find("gauge_back").gameObject;
        if (lineObject != null) lineTransform = lineObject.transform;
        
        StartCoroutine(StateMachine());
    }

    IEnumerator StateMachine(){
        while(true){
            yield return StartCoroutine(ballState.ToString());
        }
    }

    IEnumerator WAITING(){ //게임 초기화
        gameObject.layer = 6; 
        ballRb.velocity = Vector2.zero; 
        ballRb.gravityScale = 0f;
        transform.position = new Vector3(0f, -4f, 0f);

        while (ballState == BallState.WAITING)
        {
            objSetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            // 공 발사
            launchBall();
        }
    }

    void launchBall()
    {
        Vector2 direction = lineObject.GetComponent<gagueRotate>().GetCurrentDirection();

        objSetActive(false);
        ballRb.velocity = Vector2.zero;
        ballRb.AddForce(direction * throwingForce, ForceMode2D.Impulse);

        gameObject.layer = 0;
        ballRb.gravityScale = 4f;
        ballState = BallState.ROLLING;
    }
    void objSetActive(bool active)
    {
        lineObject.SetActive(active);
        gaugeBack.SetActive(active);
    }
    IEnumerator ROLLING(){
        while(ballState == BallState.ROLLING){
            yield return null;
        }
    }
    

    IEnumerator GOAL(){
        while(ballState == BallState.GOAL){
            //goal 세레모니?
            yield return new WaitForSeconds(2f); //세레모니 기다리는 시간?
            ballState = BallState.WAITING;
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
       
        if (other.gameObject.CompareTag("player1"))
        {
            LastPlayerType = PlayerType.Player1;
             
        }
        else if (other.gameObject.CompareTag("player2"))
        {
            LastPlayerType = PlayerType.Player2;
          
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("goal"))
        {
            ballRb.velocity = Vector2.zero;
            ballState = BallState.GOAL;
        }
    }
}
