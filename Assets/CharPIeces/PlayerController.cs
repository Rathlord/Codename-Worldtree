using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;
    public SFXPlayer SFXPlayer;
    public Slider slider;

    [SerializeField] public Transform playerTransform;

    float playerSpeed = 10f;
    float jumpVelocity = 10f;
    float armor;
    bool grounded;
    [SerializeField] float currentHealth = 1f;
    [SerializeField] float maxHealth;
    [SerializeField] float enemyCollisionMagnitude = 200f;

    public bool freezeControls = false;

    public Rigidbody2D rigidBody;
    float xThrow;

    int jumpCharges = 1;

    [SerializeField] float fallMultiplier = 13f;
    [SerializeField] float lowJumpMultiplier = 5f;

    [SerializeField] Vector3 facingRight;
    [SerializeField] Vector3 facingLeft;
    public string facing;

    public bool dead = false;

    [SerializeField] public GameObject projectileParent;

    public float ability1Damage;
    public float ability2Damage;
    public float ability3Damage;
    public float ability4Damage;


    void Start () 
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        currentHealth = StatHolster.instance.healthMaximum;
        print("I'm at least starting right?");
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

    public virtual void Update()
    {
        UpdateMaxHealth();
        SetSpeed();
        SetJump();
        CharacterFacing();
        SetAbilityDamage();
        SetArmor();
    }



    private void UpdateMaxHealth()
    {
        maxHealth = StatHolster.instance.healthMaximum;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void FixedUpdate()
    {
        HorizontalMovement();
        Jumping();
        BetterJumping();
    }

    private void SetAbilityDamage()
    {
        ability1Damage = StatHolster.instance.attackDamage;
        ability2Damage = StatHolster.instance.attackDamage;
        ability3Damage = StatHolster.instance.attackDamage;
        ability4Damage = StatHolster.instance.attackDamage;
    }

    void CharacterFacing() // checks facing of character
    {
        if (dead == false)
        {
            if (facing == "right")
            {
                playerTransform.rotation = Quaternion.Euler(facingRight);
            }
            else if (facing == "left")
            {
                playerTransform.rotation = Quaternion.Euler(facingLeft);
            }
        }
        else
        {
            return;
        }

    }

    private void LateUpdate()
    {
        DeathCheck();
        UpdateHealthSlider();
    }

    private void DeathCheck() // checks if player is dead and does stuff
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

    public void TakeDamage(float healthChange) // called from other scripts to change character health
    {
        if (healthChange > armor)
        {
            currentHealth = currentHealth - (healthChange - armor);
        }
        else
        {
            currentHealth = currentHealth - 1f;
        }
    }

    public void Heal(float healthChange)
    {
        currentHealth += healthChange;
    }

    private void UpdateHealthSlider() // Updates slider with character health
    {
        slider.value = currentHealth;
        slider.maxValue = maxHealth;
    }

    private void SetArmor()
    {
        armor = StatHolster.instance.armor;
    }

    private void SetJump()
    {
        jumpVelocity = StatHolster.instance.jumpVelocity;
    }

    private void SetSpeed() //Grabs speed from StatHolster and sets it
    {
        playerSpeed = StatHolster.instance.moveSpeed;
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
            SFXPlayer.instance.PlayJump();
            rigidBody.velocity = Vector2.up * jumpVelocity;
            jumpCharges--;
         }
     } 

    private void OnCollisionStay2D(Collision2D collision) //Allow player to jump if they're on a floor
    {
        CircleCollider2D collider = collision.otherCollider as CircleCollider2D;

        if (collider != null && collision.gameObject.tag == "Floor")
        {
            jumpCharges = StatHolster.instance.jumpCharges;
            grounded = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // freeze character controls and knock them back on being hit
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

        if (xThrow > 0)
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
