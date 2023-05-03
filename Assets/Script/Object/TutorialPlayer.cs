using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{
    private float gravity = -80;//-100
    public Vector2 velocity;
    private float jumpVelocity = 32;//25
    private float groundHeight;

    private Animator playerAni;
    public bool left = false;
    public bool right = false;
    private bool isJumping = false;
    private bool isCrouching = false;


    void Start()
    {
        playerAni = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)&&!isJumping&&!isCrouching&&!left)
        {
            right = true;
            playerAni.SetBool("IsRunning", right);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        } else if(Input.GetKeyUp(KeyCode.RightArrow)&&!isJumping&&!isCrouching&&right)
        {
            right = false;
            playerAni.SetBool("IsRunning", right);
        }
        
        if(Input.GetKeyDown(KeyCode.LeftArrow)&&!isJumping&&!isCrouching&&!right)
        {
            left = true;
            playerAni.SetBool("IsRunning", left);
            gameObject.GetComponent<SpriteRenderer>().flipX = left;
        } else if(Input.GetKeyUp(KeyCode.LeftArrow)&&!isJumping&&!isCrouching&&left)
        {
            left = false;
            playerAni.SetBool("IsRunning", left);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)&&!isJumping&&!isCrouching&&!right&&!left)
        {
            isJumping = true;
            velocity.y = jumpVelocity;
            playerAni.SetBool("IsJumping", isJumping);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)&&!isJumping&&!right&&!left)
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
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green);
        }

        if(!isJumping)
        {
            if(right)
            {
                pos.x += 5 * Time.fixedDeltaTime;
            } else if(left)
            {
                pos.x -= 5 * Time.fixedDeltaTime;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if(hit2D.collider == null)
            {
                isJumping = true;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        transform.position = pos;
    }
}
