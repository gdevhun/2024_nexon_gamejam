using System.Collections;
using UnityEngine;

public class Swing : MonoBehaviour
{ 

    private HingeJoint2D hinge;
    private JointMotor2D motor;
    public KeyCode activationKey;

    public float speed = 1000f;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        motor = hinge.motor;
    }    

    void Update()
    {        
        if(Input.GetKeyDown(activationKey))
        {
            motor.motorSpeed = -speed;
            hinge.motor = motor;        
        }
        if(Input.GetKeyUp(activationKey))
        {
            motor.motorSpeed = speed;
            hinge.motor = motor;       
        }
    }
}