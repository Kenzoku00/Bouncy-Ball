    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotation : MonoBehaviour
{
    private float torqueMultiplier = 0.005f;
    private Rigidbody rb;
    private bool canMove = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (true)
        {
            ApplyTorque(collision);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (canMove)
        {
            ApplyTorque(collision);
        }
    }

    private void ApplyTorque(Collision collision)
    {
        // Calculate the direction of the torque based on the contact point and velocity
        ContactPoint contact = collision.contacts[0];
        Vector3 torqueDirection = Vector3.Cross(contact.normal, rb.velocity).normalized;

        // Apply the torque to the ball
        rb.AddTorque(torqueDirection * rb.velocity.magnitude * torqueMultiplier, ForceMode.Force);
    }
}
