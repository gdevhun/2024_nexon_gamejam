using System.Collections;
using UnityEngine;

public class swing : MonoBehaviour
{ 
    private bool isFlipped = false;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<HingeJoint2D>().useMotor = true;
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            GetComponent<HingeJoint2D>().useMotor = false;
        }
    }
}