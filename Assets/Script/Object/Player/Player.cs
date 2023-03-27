using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity = -100;
    public Vector2 velocity;
    public float jumpVelocity = 25;

    public float maxXVelocity = 70;
    public float maxAcceleration = 5;
    public float acceleration = 5;
    public float distance = 0;

    private float groundHeight;

    private bool isRunning = true;
    private bool isJumping = false;
    private bool isCrouching = false;

    private Animator playerAni;

    void Start()
    {
        playerAni = GetComponent<Animator>();
        playerAni.SetBool("IsRunning", isRunning);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)&&!isJumping&&!isCrouching)
        {
            isJumping = true;
            velocity.y = jumpVelocity;
            playerAni.SetBool("IsJumping", isJumping);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)&&!isJumping)
        {
            isCrouching = true;
            playerAni.SetBool("IsCrouching", isCrouching);
        }else if(Input.GetKeyUp(KeyCode.DownArrow)&&!isJumping&&isCrouching)
        {
            isCrouching = false;
            playerAni.SetBool("IsCrouching", isCrouching);
        }    
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if(isJumping)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;

            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if(hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if(ground != null)
                {
                    groundHeight = ground.groundHeight;
                    pos.y = groundHeight;
                    isJumping = false;
                    playerAni.SetBool("IsJumping", isJumping);
                    Debug.Log("Running on the ground: " + groundHeight);
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green);
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if(!isJumping)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);
            velocity.x += acceleration * Time.fixedDeltaTime;

            if(velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }

            // Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            // Vector2 rayDirection = Vector2.up;
            // float rayDistance = velocity.y * Time.fixedDeltaTime;
            // RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            // if(hit2D.collider == null)
            // {
            //     isJumping = true;
            //     Debug.Log("Jump over the ground");
            // }
            // Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        transform.position = pos;
    }
}
