using UnityEngine;

public class gagueRotate : MonoBehaviour
{
    public float startAngle = 15f; 

    public float endAngle = 165f; 
    public float rotationSpeed = 50f; 
    private float currentAngle = 90f; 
    private bool rotatingleft = true; 

    void Update()
    {
        if (rotatingleft)
        {
            currentAngle += rotationSpeed * Time.deltaTime;
            if (currentAngle >= endAngle)
            {
                currentAngle = endAngle;
                rotatingleft = false;
            }
        }
        else
        {
            currentAngle -= rotationSpeed * Time.deltaTime;
            if (currentAngle <= startAngle)
            {
                currentAngle = startAngle;
                rotatingleft = true;
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
    public Vector2 GetCurrentDirection()
    {
        Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * Vector2.right;
        return direction.normalized;
    }
}
