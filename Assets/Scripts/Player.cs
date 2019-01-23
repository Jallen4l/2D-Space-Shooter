using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]

public class Player : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJump = .4f;
    public float moveSpeed = 6;
    public float accelerationTimeAirbourne = .2f;
    public float accelerationTimeGrounded = .1f;

    float jumpVelocity;
    float gravity;
    float velocityXsmoothing;
    Vector3 velocity;

    //Create reference to Controller2D Script
    Controller2D controller;
    

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJump, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJump;
        print("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXsmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirbourne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
