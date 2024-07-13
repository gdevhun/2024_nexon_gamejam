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
        
        StartCoroutine(StateMachine());
    }

    IEnumerator StateMachine(){
        while(true){
            Debug.Log("Current State: " + ballState.ToString());

            yield return StartCoroutine(ballState.ToString());
        }
    }

    IEnumerator WAITING(){ //게임 초기화
        gameObject.layer = 6; 
        ballRb.gravityScale = 0f;
        ballRb.velocity = Vector2.zero;
        transform.position = new Vector3(0f, -4f, 0f);
   
        Debug.Log("Waiting State Entered");
        Debug.Log("Position set to: " + transform.position.ToString());
        Debug.Log("Velocity set to: " + ballRb.velocity.ToString());
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
        Vector2 direction = lineObject.GetComponent<gaugeRotate>().GetCurrentDirection();

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
        while(ballState == BallState.GOAL)
        {
            //goal 세레모니?
            Debug.Log("Goal State Entered");
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

    private void OnBecameInvisible2D()
    {
        ballState = BallState.WAITING;
    }
}
