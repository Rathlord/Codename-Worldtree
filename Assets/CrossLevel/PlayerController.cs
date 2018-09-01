using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;
    public Slider slider;

    [SerializeField] public Transform playerTransform;

    float playerSpeed = 10f;
    float jumpVelocity = 10f;
    bool grounded;
    [SerializeField] float currentHealth = 1f;
    [SerializeField] float enemyCollisionMagnitude = 200f;

    public bool freezeControls = false;

    Rigidbody2D rigidBody;
    float xThrow;

    int jumpCharges = 1;

    [SerializeField] float fallMultiplier = 13f;
    [SerializeField] float lowJumpMultiplier = 5f;

    public bool dead = false;

    public string facing;


    void Start () 
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        currentHealth = StatHolster.instance.healthMaximum;
	}
    

    private void Awake()
    {
        int numberOfPlayerMovements = FindObjectsOfType<PlayerController>().Length;
        if (numberOfPlayerMovements > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        instance = this;
    }

    void Update()
    {
        UpdateHealthSlider();
        SetSpeed();
        SetJump();
    }

    private void LateUpdate()
    {
        DeathCheck();
    }

    private void DeathCheck()
    {
        if (currentHealth <= 0 && dead == false)
        {
            freezeControls = true;
            rigidBody.freezeRotation = false;
            playerTransform.Rotate(0.0f, 0.0f, 90f, Space.World);
            dead = true;
            gameObject.layer = 8;
        }
    }

    public void ChangeHealth(float healthChange)
    {
        currentHealth -= healthChange;
        print("I should be changing health by -" + healthChange);
    }

    private void UpdateHealthSlider()
    {
        slider.value = currentHealth;
    }

    private void SetJump()
    {
        jumpVelocity = StatHolster.instance.jumpVelocity;
    }

    private void SetSpeed() //Grabs speed from StatHolster and sets it
    {
        playerSpeed = StatHolster.instance.moveSpeed;
    }

    void FixedUpdate () {
        HorizontalMovement();
        Jumping();
        BetterJumping();
    }

    private void BetterJumping() //Increases fall speed and increases jump height when holding the jump button with clever physics
    {
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigidBody.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump"))
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Jumping()
     {
         bool jump = CrossPlatformInputManager.GetButtonDown("Jump");

         if (jump == true && jumpCharges >= 1 && dead == false)
         {
             rigidBody.velocity = Vector2.up * jumpVelocity;
            jumpCharges--;
         }
     } 

    private void OnCollisionStay2D(Collision2D collision) //Allow player to jump if they're on a floor
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpCharges = StatHolster.instance.jumpCharges;
            grounded = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && dead == false)
        {
            rigidBody.velocity = new Vector2(0, 0);
            freezeControls = true;
            Invoke("Unfreeze", .4f);
            Vector2 direction = collision.transform.position - gameObject.transform.position;
            direction.Normalize();
            //print(direction);
            rigidBody.AddForce(Vector2.right * -direction * enemyCollisionMagnitude, ForceMode2D.Impulse);
        }
    }

    private void Unfreeze()
    {
        if (dead == false){
            freezeControls = false;
        }

    }

    private void OnCollisionExit2D(Collision2D collision) //Disallow jumping if player not on a floor
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpCharges = StatHolster.instance.jumpCharges - 1;
            grounded = false;
        }
    }

    private void HorizontalMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // Use horizontal cross-platform input

        if (xThrow >= 0)
        {
            facing = "right";
        }
        else if (xThrow < 0)
        {
            facing = "left";
        }

        float horizontalMovement = xThrow * playerSpeed * Time.fixedDeltaTime; // set horizontal movement equal to horizontal throw * speed factor * time.deltatime to account for framerate

        if (freezeControls == false)
        {
            if (grounded == true)
            {
                rigidBody.AddForce(Vector2.right * horizontalMovement);
            }
            else if (grounded == false)
            {
                rigidBody.AddForce(Vector2.right * horizontalMovement / 3); //Reduce horizontal movespeed while in the air
            }
            else
            {
                print("something went wrong and the jump bool isn't set.");
            }
        }
        else
        {
            return;
        }

    }

    public void ForcedMovement(float verticalVelocity, float horizontalVelocity)
    {
        rigidBody.AddForce(Vector2.up * verticalVelocity, ForceMode2D.Impulse);
        rigidBody.AddForce(Vector2.right * horizontalVelocity, ForceMode2D.Impulse);
    }


}
