using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public PlayerType LastPlayerType;
    public float throwingRange = 140f;
    public float throwingRadius = 5f;
    
    private bool isGaugeActive = false;
    private bool isForceApplied = false;
    private float gaugeAngle;


    enum BallState
    {
        WAITING,
        ROLLING,
        GOAL
    }

    
    BallState ballState;
    
    void Start()
    {
        //ballState = BallState.WAITING;
        //StartCoroutine(StateMachine());
    }

    /*IEnumerator StateMachine(){
        while(true){
            yield return StartCoroutine(ballState.ToString());
        }
    }*/

    /*IEnumerator WAITING(){
        lastTouch = LastTouch.none;
        //애니메이션 웨이팅으로
        //게이지바 setTrue?? 재활용 위해서라도 setTrue하고 vector 받아오는 식으로 해야할듯? 
     
    }
    IEnumerator ROLLING(){
        //얘는 딱히 할거 없음. 알아서 냅두면 됨.
        //goal tag 에 onTriggered되면 velocity 멈춤. 
        ballState = BallState.GOAL;
    }
    

    IEnumerator GOAL(){
        //goal 애니메이션
        ballState = BallState.WAITING;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        //player1 태그랑 부딪히면 animation 바꿈. 
        //  lastTouch = LastTouch.player1;
        //  animation player1으로 바꿔주기  

        //player2 태그랑 부딪히면 animation 바꿈. 
        //  lastTouch = LastTouch.player2;
        //  animation player2로 바꿔주기

    }

    private IEnumerator GaugeRoutine()
    {
        float gaugeSpeed = 60f; // 게이지가 이동하는 속도 (도/초)
        float angleStep = gaugeSpeed * Time.deltaTime;
        gaugeAngle = -throwingRange / 2;

        while (isGaugeActive)
        {
            gaugeAngle += angleStep;

            if (gaugeAngle > throwingRange / 2)
            {
                gaugeAngle = -throwingRange / 2;
            }

            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                isGaugeActive = false;
                
                // 현재 게이지 각도를 기반으로 방향 계산
                Vector2 direction = Rotate(Vector2.right, gaugeAngle).normalized;

                // 공의 속도를 0으로 설정
                ballRb.velocity = Vector2.zero;

                // 공에 힘 적용
                ballRb.AddForce(direction * forceStrength, ForceMode2D.Impulse);
                ballState = BallState.ROLLING;

            }

            yield return null;
        }
    }*/

}
