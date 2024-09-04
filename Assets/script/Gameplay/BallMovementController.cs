using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementController : MonoBehaviour
{
    public float speed = 5.0f;
    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = movement * speed;

        // Memastikan animasi berjalan saat bola bergerak
        if (movement != Vector3.zero)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BounceAnimation"))
            {
                animator.Play("BounceAnimation");
            }
        }
    }
}
