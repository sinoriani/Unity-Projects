using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform aimTarget; // the target where we aim to land the ball
    float speed = 3f; // move speed
    float force = 13; // ball impact force

    bool hitting; // boolean to know if we are hitting the ball or not 

    public Transform ball; // the ball 
    Animator animator;

    Vector3 aimTargetInitialPosition; // initial position of the aiming gameObject which is the center of the opposite court

    private void Start()
    {
        animator = GetComponent<Animator>(); // referennce out animator
        aimTargetInitialPosition = aimTarget.position; // initialise the aim position to the center( where we placed it in the editor )
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // get the horizontal axis of the keyboard
        float v = Input.GetAxisRaw("Vertical"); // get the vertical axis of the keyboard

        if(Input.GetKeyDown(KeyCode.F))
        {
            hitting = true; // we are trying to hit the ball and aim where to make it land
        }else if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false;
        }

        if (hitting)  // if we are trying to hit the ball
        {
            aimTarget.Translate(new Vector3(h, 0, 0) * speed * Time.deltaTime); //translate the aiming gameObject on the court horizontallly
        }


        if( (h != 0 || v != 0  ) && !hitting) // if we want to move and we are not hitting the ball
        {
            transform.Translate( new Vector3(h , 0 , v ) * speed * Time.deltaTime ); // move on the court
        }

        

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // if we collide with the ball 
        {
            Vector3 dir = aimTarget.position - transform.position; // get the direction to where we want to send the ball
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0 , 6 ,0) ; //add force to the ball
                                                                                                        // plus some upward force
            Vector3 ballDir = ball.position - transform.position; // get the direction of the ball compared to us to know if it is
            if( ballDir.x >= 0)                                   // on out right or left side 
            {
                animator.Play("forehand");                        // play a forhand animation if the ball is on our right
            }
            else                                                  // otherwise play a backhand animation 
            {
                animator.Play("backhand");
            }

            aimTarget.position = aimTargetInitialPosition; // reset the position of the aiming gameObject to it's original position ( center)

        }
    }


}
