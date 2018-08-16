using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    float playerSpeed = 10f;
    float jumpVelocity = 10f;

    Rigidbody2D rigidBody;
    float xThrow;

    int jumpCharges = 1;

    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
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
        SetSpeed();
        SetJump();
        BetterJumping();
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

         if (jump == true && jumpCharges >= 1)
         {
             rigidBody.velocity = Vector2.up * jumpVelocity;
            jumpCharges--;
         }
     } 

    private void OnCollisionEnter2D(Collision2D collision) //Allow player to jump if they're on a floor
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpCharges = StatHolster.instance.jumpCharges;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //Disallow jumping if player not on a floor
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpCharges = StatHolster.instance.jumpCharges - 1;
        }
    }

    private void HorizontalMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // Use horizontal cross-platform input

        float horizontalMovement = xThrow * playerSpeed * Time.fixedDeltaTime; // set horizontal movement equal to horizontal throw * speed factor * time.deltatime to account for framerate

        rigidBody.AddForce(Vector2.right * horizontalMovement);
    }

    public void ForcedMovement(float verticalVelocity, float horizontalVelocity)
    {
        rigidBody.AddForce(Vector2.up * verticalVelocity, ForceMode2D.Impulse);
        print(verticalVelocity);
        rigidBody.AddForce(Vector2.right * horizontalVelocity, ForceMode2D.Impulse);
    }


}
