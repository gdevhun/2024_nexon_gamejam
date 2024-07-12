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
            lineObject.SetActive(true);
            

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            Vector2 direction = lineObject.GetComponent<gagueRotate>().GetCurrentDirection();

            // 공 발사
            lineObject.SetActive(false);
            ballRb.velocity = Vector2.zero; 
            //ballRb.AddForce(direction * throwingForce, ForceMode2D.Impulse);
            
            gameObject.layer = 0; 
            ballRb.gravityScale = 4f;
            ballState = BallState.ROLLING;
            yield return null;
        }
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
        /*
        if (other.gameObject.CompareTag("player1"))
        {
            //lastTouch = LastTouch.player1;
            // player1 애니메이션으로 변경하는 코드를 여기에 추가
        }
        else if (other.gameObject.CompareTag("player2"))
        {
            //lastTouch = LastTouch.player2;
            // player2 애니메이션으로 변경하는 코드를 여기에 추가
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("goal"))
        {
            ballState = BallState.GOAL;
        }
    }
}
