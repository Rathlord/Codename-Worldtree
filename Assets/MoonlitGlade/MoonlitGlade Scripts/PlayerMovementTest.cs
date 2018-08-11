using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementTest : MonoBehaviour {

    Rigidbody2D rigidBody;
    float xThrow;
    [SerializeField] [Tooltip("Player's speed tuning number")]public float speed = 10f;
    [SerializeField] [Tooltip("Player's jump heigh number")]public float jumpHeight = 10f;

    bool jumped = false;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        HorizontalMovement();
        Jumping();
	}

    private void Jumping()
    {
        bool jump = CrossPlatformInputManager.GetButtonDown("Jump");

        if (jump == true && jumped == false)
        {
            jumped = true;
            rigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumped = false;
        }
    }

    private void HorizontalMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // Use horizontal cross-platform input

        float horizontalMovement = xThrow * speed * Time.fixedDeltaTime; // set horizontal movement equal to horizontal throw * speed factor * time.deltatime to account for framerate

        rigidBody.AddForce(Vector2.right * horizontalMovement);
    }


}
