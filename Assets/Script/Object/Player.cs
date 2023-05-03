using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float gravity = -80;//-100
    public Vector2 velocity;
    private float jumpVelocity = 32;//25

    private float maxXVelocity = 70;
    private float maxAcceleration = 7;
    private float acceleration = 7;
    public float distance = 0;
    public int bonus = 1;
    public int doubleBonus = 2;

    private float groundHeight;


    private Animator playerAni;
    private bool isRunning = true;
    private bool isJumping = false;
    private bool isCrouching = false;
    public bool death = false;

    public int maxHealth = 3;
    public int health;

    private float invincibleTimer;
    private float timeInvincible = 2.5f;
    public bool isInvincible = false;

    private AudioSource audioSource;
    public AudioClip hitAC;
    public AudioClip healAC;
    public AudioClip runningAC;
    public AudioClip jumpingAC;
    public AudioClip slidingAC;
    public AudioClip gameoverAC;

    private UIController UI;
    public GameObject endGamePanel;

    private Vector2 orgColliderSize;

    void Start()
    {
        health = maxHealth;
        playerAni = GetComponent<Animator>();
        playerAni.SetBool("IsRunning", isRunning);
        audioSource= GetComponent<AudioSource>();
        UI = GameObject.Find("UI").GetComponent<UIController>();
        orgColliderSize = gameObject.GetComponent<BoxCollider2D>().size;
    }

    void Update()
    {
        if(death)
        {
            return;
        }

        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        if(isInvincible)
        {
            Invincible();
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)&&!isJumping&&!isCrouching)
        {
            isJumping = true;
            velocity.y = jumpVelocity;
            playerAni.SetBool("IsJumping", isJumping);
            audioSource.clip = null;
            PlaySound(jumpingAC);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)&&!isJumping)
        {
            isCrouching = true;
            playerAni.SetBool("IsCrouching", isCrouching);
            audioSource.clip = null;
            PlaySound(slidingAC);
        }else if(Input.GetKeyUp(KeyCode.DownArrow)&&!isJumping&&isCrouching)
        {
            // UpdatePlayerCollider();
            isCrouching = false;
            playerAni.SetBool("IsCrouching", isCrouching);
            audioSource.clip = runningAC;
            audioSource.Play();
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
                    audioSource.clip = runningAC;
                    audioSource.Play();
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

            Vector2 rayOrigin = new Vector2(pos.x - 0.5f, pos.y);
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

    private void Invincible()
    {
        invincibleTimer -= Time.deltaTime;
        if(invincibleTimer < 0)
        {
            Color color = this.GetComponent<SpriteRenderer>().color;
            color.a = 1;
            this.GetComponent<SpriteRenderer>().color = color;
            isInvincible = false;
        } else
        {
            Color color = this.GetComponent<SpriteRenderer>().color;
            if (color.a == 0)
            {
                color.a = 1;
            } else
            {
                color.a = 0;
            }
            this.GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            PlaySound(hitAC);
        } else
        {
            PlaySound(healAC);
        }
        
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        UI.updateHearts();
        if (health <= 0)
        {
            death = true;
            playerAni.SetBool("Death", death);
            PlaySound(gameoverAC);
            endGamePanel.SetActive(true);
            audioSource.clip = null;
            audioSource.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void UpdatePlayerCollider()
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        gameObject.AddComponent<BoxCollider2D>();
    }

}
