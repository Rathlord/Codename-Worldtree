using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    //GAMEOBJECTS//
    public static PlayerController instance;
    public SFXPlayer SFXPlayer;
    public Slider slider;
    public Rigidbody2D rigidBody;
    [SerializeField] public Transform playerTransform;
    [SerializeField] public GameObject projectileParent;

    //PLAYERSTATS//
    [SerializeField] public float currentHealth = 1f;
    public float baseMoveSpeed = 30000f;
    public float bonusFlatMoveSpeed = 0f;
    public float bonusMultMoveSpeed = 1f;
    public float baseJumpVelocity = 70f;
    public float bonusFlatJumpVelocity;
    public int bonusJumpCharges;
    public int tempJumpCharges;
    public float healthMaximum = 100f;
    public float baseAttackDamage = 10f;
    float attackDamage;
    public float bonusFlatAttackDamage;
    public float bonusMultAttackDamage = 1;
    public float armor;
    bool grounded;
    [SerializeField] float enemyCollisionMagnitude = 200f;
    public bool dead = false;
    public float iFrameDuration = .7f;

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
    public float ability1Cooldown = 1f;
    public float ability2Cooldown = 1f;
    public float ability3Cooldown = 1f;
    public float ability4Cooldown = 1f;
    public bool ability1Up = true;
    public bool ability2Up = true;
    public bool ability3Up = true;
    public bool ability4Up = true;

    //ITEMVARIABLES//
    public bool activeItemUp = true;
    public float activeItemCooldown = 1f;
    public int fafnirCharges;
    public int hrymsCharges;
    public int hrymsTempCharges;
    public int nineStepPoison;
    public int yggDewCharges;






    ///// SETUP /////


    public virtual void Start()
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
        attackDamage = (baseAttackDamage + bonusFlatAttackDamage) * bonusMultAttackDamage;
        MaxHealthCap();
        CharacterFacing();
        UpdateAbilityDamage();
        Controls();
        CalculateAttackDamage();
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

    private void CalculateAttackDamage()
    {
        attackDamage = (baseAttackDamage + bonusFlatAttackDamage) * bonusMultAttackDamage;
    }

    public virtual void UpdateAbilityDamage()
    {
        ability1Damage = attackDamage;
        ability2Damage = attackDamage;
        ability3Damage = attackDamage;
        ability4Damage = attackDamage;
    }


    ///// PLAYER CONTROLS /////


    void Controls()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            if (ability1Up == true) // if it's off cooldown, use the ability, put it on cooldown
            {
                Invoke("AbilityOne", .2f); // Invoking it at a delay to help it line up with animation?
                ability1Up = false;
                Invoke("AbilityOneCooldown", ability1Cooldown);
            }
            else // if it's on cooldown, do nothing
            {
                return;
            }
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            if (ability2Up == true) // if it's off cooldown, use the ability, put it on cooldown
            {
                AbilityTwo();
                ability2Up = false;
                Invoke("AbilityTwoCooldown", ability2Cooldown);
            }
            else // if it's on cooldown, do nothing
            {
                return;
            }
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire3"))
        {
            if (ability3Up == true) // if it's off cooldown, use the ability, put it on cooldown
            {
                AbilityThree();
                ability3Up = false;
                Invoke("AbilityThreeCooldown", ability3Cooldown);
            }
            else // if it's on cooldown, do nothing
            {
                return;
            }
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire4"))
        {
            if (ability4Up == true) // if it's off cooldown, use the ability, put it on cooldown
            {
                AbilityFour();
                ability4Up = false;
                Invoke("AbilityFourCooldown", ability4Cooldown);
            }
            else // if it's on cooldown, do nothing
            {
                return;
            }
        }
        if (CrossPlatformInputManager.GetButtonDown("ActiveItem"))
        {
            if (activeItemUp == true) // if it's off cooldown, use the ability, put it on cooldown
            {
                ActivatedItemBehavior.instance.ActivateItem(); // Use the activated item
                activeItemUp = false;
                Invoke("ActiveItemCooldown", activeItemCooldown);
            }
            else // if it's on cooldown, do nothing
            {
                return;
            }

        }
        if (CrossPlatformInputManager.GetButtonDown("UseItem"))
        {
            UseItemBehavior.instance.UseItem(); // Use the use item
        }
    }

    void AbilityOneCooldown()
    {
        ability1Up = true;
    }

    void AbilityTwoCooldown()
    {
        ability2Up = true;
    }

    void AbilityThreeCooldown()
    {
        ability3Up = true;
    }

    void AbilityFourCooldown()
    {
        ability4Up = true;
    }

    void ActiveItemCooldown()
    {
        activeItemUp = true;
    }


    public virtual void AbilityOne()
    {

    }

    public virtual void AbilityTwo()
    {

    }

    public virtual void AbilityThree()
    {

    }

    public virtual void AbilityFour()
    {

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

        if (jump == true && grounded == true && dead == false)
        {
            SFXPlayer.instance.PlayJump();
            rigidBody.velocity = Vector2.up * (baseJumpVelocity + bonusFlatJumpVelocity);
        }
        else if (jump == true && tempJumpCharges > 0 && dead == false)
        {
            SFXPlayer.instance.PlayJump();
            rigidBody.velocity = Vector2.up * (baseJumpVelocity + bonusFlatJumpVelocity);
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

        float horizontalMovement = xThrow * ((baseMoveSpeed + bonusFlatMoveSpeed) * bonusMultMoveSpeed) * Time.fixedDeltaTime; // set horizontal movement equal to horizontal throw * speed factor * time.deltatime to account for framerate

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

    public void TakeDamage(float healthChange) // called from other scripts to change character health. Checks against Hryms and armor.
    {
        StartCoroutine("Invulnerable"); // Took damage, get your iFrames
        if (hrymsCharges > 0)
        {
            if (hrymsTempCharges / (11 - hrymsCharges) >= 1)
            {
                print("You got saved by Hrym's Shield!!");
                hrymsTempCharges = 0;
            }
            else
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
            hrymsTempCharges++;
        }
        else
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
            healthMaximum = healthMaximum + fafnirCharges;
        }
    }

    public void YggDew() // Begin Ygg healing when item first picked up
    {
        InvokeRepeating("YggRegen", 5f, 5f);
    }

    private void YggRegen()
    {
        currentHealth += yggDewCharges;
    }

    ///// COLLISION BEHAVIOR /////

    IEnumerator Invulnerable()
    {
        print("iFrame start");
        gameObject.layer = 11;
        yield return new WaitForSeconds(iFrameDuration);
        gameObject.layer = 10;
        print("iFrame finish");
    }

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
