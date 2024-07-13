using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouniness : MonoBehaviour
{
    public float reflectForce = 1f;
    private Rigidbody2D ballRb;


    void OnCollisionEnter2D(Collision2D collision)
    {
        ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
        Vector2 reflectDirection = CalculateReflection(collision);
        ApplyReflectForce(reflectDirection);
    }

    Vector2 CalculateReflection(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 normal = contact.normal;

        Vector2 incomingVector = -collision.relativeVelocity.normalized;
        Vector2 reflectDirection = Vector2.Reflect(incomingVector, normal);

        return reflectDirection;
    }

    void ApplyReflectForce(Vector2 reflectDirection)
    {
        if (ballRb.velocity.magnitude < 10f)
        {
            ballRb.AddForce(reflectDirection * reflectForce, ForceMode2D.Impulse);
        }
    }
}
