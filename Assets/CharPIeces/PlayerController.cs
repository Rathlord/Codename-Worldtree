using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    //GAMEOBJECTS//
    public static PlayerController instance;
    public SFXPlayer SFXPlayer;
    public Slider slider;
    public Rigidbody2D rigidBody;
    [SerializeField] public Transform playerTransform;
    [SerializeField] public GameObject projectileParent;

    //PLAYERSTATS//
    public float moveSpeed = 30000f;
    public float jumpVelocity = 70f;
    public int bonusJumpCharges;
    public int tempJumpCharges;
    public float healthMaximum = 100f;
    public float attackDamage = 10f;
    public float armor;
    bool grounded;
    [SerializeField] float currentHealth = 1f;
    [SerializeField] float enemyCollisionMagnitude = 200f;
    public bool dead = false;

    //CONTROLS//
    public bool freezeControls = false;
    float xThrow;
    [SerializeField] float fallMultiplier = 13f;
    [SerializeField] float lowJumpMultiplier = 5f;

    //DIRECTIONALVARIABLES//
    [SerializeField] Vector3 facingRight;
    [SerializeField] Vector3 facingLeft;
    public string facing;

    //ABILITYVARIABLES//
    public float ability1Damage;
    public float ability2Damage;
    public float ability3Damage;
    public float ability4Damage;

    //ITEMVARIABLES//
    int fafnirCharges;





    ///// SETUP /////


    void Start () 
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        currentHealth = healthMaximum;
        print("I'm at least starting right?");
        tempJumpCharges = bonusJumpCharges; //Give the player an initial jump charge
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
        MaxHealthCap();
        CharacterFacing();
        UpdateAbilityDamage();
    }

    void FixedUpdate()
    {
        HorizontalMovement();
        Jumping();
        BetterJumping();
    }

    private void LateUpdate()
    {
        DeathCheck();
        UpdateHealthSlider();
    }

    ///// STAT READING & UPDATING /////

    private void UpdateHealthSlider() // Updates slider with character health
    {
        slider.value = currentHealth;
        slider.maxValue = healthMaximum;
    }

    private void MaxHealthCap()
    {
        if (currentHealth > healthMaximum)
        {
            currentHealth = healthMaximum;
        }
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

    private void UpdateAbilityDamage()
    {
        ability1Damage = attackDamage;
        ability1Damage = attackDamage;
        ability1Damage = attackDamage;
        ability1Damage = attackDamage;
    }

    ///// PLAYER MOVEMENT CONTROL /////

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

         if (jump == true && grounded == true && dead == false)
         {
            SFXPlayer.instance.PlayJump();
            rigidBody.velocity = Vector2.up * jumpVelocity;
         }
         else if (jump == true && tempJumpCharges > 0 && dead == false)
         {
            SFXPlayer.instance.PlayJump();
            rigidBody.velocity = Vector2.up * jumpVelocity;
            tempJumpCharges--;
         }
        else
        {
            return;
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

        float horizontalMovement = xThrow * moveSpeed * Time.fixedDeltaTime; // set horizontal movement equal to horizontal throw * speed factor * time.deltatime to account for framerate

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

    private void Unfreeze()
    {
        if (dead == false)
        {
            freezeControls = false;
        }

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

    ///// SIGNALS /////

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

    public void ForcedMovement(float verticalVelocity, float horizontalVelocity)
    {
        rigidBody.AddForce(Vector2.up * verticalVelocity, ForceMode2D.Impulse);
        rigidBody.AddForce(Vector2.right * horizontalVelocity, ForceMode2D.Impulse);
    }

    public void EnemyDeath()
    {
        Fafnir();
    }

    public void AddArmor(float changeValue)
    {
        armor += changeValue;
    }

    public void AddattackDamage(float changeValue)
    {
        attackDamage += changeValue;
    }

    public void AddMaxHealth(float changeValue)
    {
        healthMaximum += changeValue;
    }

    public void AddMoveSpeed(float changeValue) //Called from other classes to add to speed value
    {
        moveSpeed += changeValue;
    }

    public void MultiplyMoveSpeed(float changeValue) //Called from other classes to multiplicatively add to speed value
    {
        moveSpeed *= changeValue;
    }

    public void AddJumpSpeed(float changeValue) //Called from other classes to add to jump value
    {
        jumpVelocity += changeValue;
    }

    public void MultiplyJumpSpeed(float changeValue) //Called from other classes to multiplicatively add to jump value
    {
        jumpVelocity *= changeValue;
    }

    ///// ITEM BEHAVIOR /////

    private void Fafnir()
    {
        if (fafnirCharges == 0)
        {
            return;
        }
        else
        {
            currentHealth = currentHealth + fafnirCharges;
            // maxHealth = 
        }
    }


    ///// COLLISION BEHAVIOR /////

    private void OnCollisionStay2D(Collision2D collision) //Allow player to jump if they're on a floor
    {
        CircleCollider2D collider = collision.otherCollider as CircleCollider2D;

        if (collider != null && collision.gameObject.tag == "Floor")
        {
            tempJumpCharges = bonusJumpCharges;
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

    private void OnCollisionExit2D(Collision2D collision) //Disallow jumping if player not on a floor
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = false;
        }
    }


}
