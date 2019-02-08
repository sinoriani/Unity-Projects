using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{

    float speed = 50; // moveSpeed
    Animator animator;
    public Transform ball;
    public Transform aimTarget; // aiming gameObject

    float force = 13; // ball impact force
    Vector3 targetPosition; // position to where the bot will want to move

    void Start()
    {
        targetPosition = transform.position; // initialize the targetPosition to its initial position in the court
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        targetPosition.x = ball.position.x; // update the target position to the ball's x position so the bot only moves on the x axis
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // lerp it's position
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // if it collides with the ball
        {
            Vector3 dir = aimTarget.position - transform.position; // get the direction to where to send the ball
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6, 0); // set force to the ball

            Vector3 ballDir = ball.position - transform.position; // get the ball direction from the bot's position
            if (ballDir.x >= 0) // if it is on the right
            {
                animator.Play("forehand"); // play a forehand animation
            }
            else
            {
                animator.Play("backhand"); // otherwise play a backhand animation
            }


        }
    }
}
