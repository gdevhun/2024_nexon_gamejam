using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEditorInternal.VersionControl.ListControl;

public class BallManager : MonoBehaviour
{
    public PlayerType LastPlayerType;
    public float throwingForce = 10f;

    private SpriteRenderer ballSR;

    private Rigidbody2D ballRb;
    private GameObject lineObject;
    private GameObject gaugeBack;

    private bool isSlow = false;
    private float minSpeed = 3f;
    public float maxSpeed = 30f;

    private Animator anim;

    private Vector3 lastPosition;
    private float stationaryTime;


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
        ballSR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.keepAnimatorStateOnDisable = true;

        ballState = BallState.WAITING;

        int random = Random.Range(0, 2);

        LastPlayerType = (random == 0)? PlayerType.Player1 : PlayerType.Player2;

        if(random != 0)
        {
            anim.SetBool("isBlueBall", false);
        }

        lineObject = transform.Find("Line").gameObject;
        gaugeBack = transform.Find("gauge_back").gameObject;

        //StartCoroutine(StateMachine());
    }

    private void Update()
    {
        if (!isSlow)
        {
            float currentSpeed = ballRb.velocity.magnitude;

            if (currentSpeed < minSpeed && currentSpeed >= 0.5f)
            {
                isSlow = true;
            }
        }

        StartCoroutine(ballState.ToString());


        if (ballState == BallState.ROLLING)
        {
            if (Vector3.Distance(transform.position, lastPosition) < 1f)
            {
                stationaryTime += Time.deltaTime;
                if (stationaryTime >= 3f)
                {
                    ballState = BallState.GOAL;
                    stationaryTime = 0f;
                }
            }
            else
            {
                lastPosition = transform.position;
                stationaryTime = 0f;
            }
        }


    }

    private void FixedUpdate()
    {
        if (ballRb.velocity.magnitude > maxSpeed)
        {
            ballRb.velocity = ballRb.velocity.normalized * maxSpeed;
        }
    }
    /*
    IEnumerator StateMachine(){
        while(true){
           //Debug.Log("Current State: " + ballState.ToString());

            yield return StartCoroutine(ballState.ToString());
        }
    }*/



    IEnumerator WAITING(){ //게임 초기화
        gameObject.layer = 6; 
        ballRb.gravityScale = 0f;
        ballRb.velocity = Vector2.zero;
        transform.position = new Vector3(0f, -4f, 0f);
   
        //Debug.Log("Waiting State Entered");
        //Debug.Log("Position set to: " + transform.position.ToString());
        //Debug.Log("Velocity set to: " + ballRb.velocity.ToString());
        while (ballState == BallState.WAITING)
        {
            Vector2 direction = lineObject.GetComponent<gaugeRotate>().GetCurrentDirection();
          

            objSetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            ballState = BallState.ROLLING;
            gameObject.layer = 0;
            ballRb.gravityScale = 4f;
            objSetActive(false);


            // 공 발사
            launchBall(direction);
        }
    }

    public void launchBall(Vector2 direction)
    {
        ballRb.velocity = Vector2.zero;
        ballRb.AddForce(direction * throwingForce, ForceMode2D.Impulse);
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
            ballState = BallState.WAITING;
            yield return null;

        }
    }
 
    void OnCollisionEnter2D(Collision2D other)
    {
       
        if (other.gameObject.CompareTag("player1"))
        {
            anim.SetBool("isBlueBall", true);

            LastPlayerType = PlayerType.Player1;      
        }
        else if (other.gameObject.CompareTag("player2"))
        {
            anim.SetBool("isBlueBall", false);

            LastPlayerType = PlayerType.Player2;
        }

        else if (isSlow && gameObject.layer != 8)
        {
            ballRb.velocity = Vector2.zero;

            float directionX = (Random.value < 0.5f) ? 1f : -1f;
            float directionY = 0.85f;

            Vector2 randomDirection = new Vector2(directionX, directionY).normalized;
            Vector2 forceToAdd = randomDirection * 7f;

            ballRb.AddForce(forceToAdd, ForceMode2D.Impulse);

            isSlow = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player1") || other.gameObject.CompareTag("player2"))
        {
            ballRb.velocity = Vector2.zero;
            ballState = BallState.GOAL;

            if (other.gameObject.CompareTag("player1"))
            {
                anim.SetBool("isBlueBall", true);


                LastPlayerType = PlayerType.Player1;
                GameManager.Instance.AddScore(PlayerType.Player2, 60);//임시 10점


            }
            else
            {
                anim.SetBool("isBlueBall", false);

                LastPlayerType = PlayerType.Player2;
                GameManager.Instance.AddScore(PlayerType.Player1, 60);//임시 
            }

        }
    }

    private void OnBecameInvisible2D()
    {
        ballState = BallState.WAITING;
    }
}
