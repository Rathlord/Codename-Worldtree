using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehavior : MonoBehaviour
{
    public static EnemyBehavior instance;

    [SerializeField] float enemyHealth = 40f;

    [SerializeField] enum State { ApproachingLeft, ApproachingRight, Attacking, Patrolling, Falling };
    State currentState;

    [SerializeField] float attackDistance = 3f; //Distance within the enemy will stop to attack the player
    [SerializeField] float moveSpeed = 25000f; //Movement speed factor of the enemy
    [SerializeField] float fallMultiplier = 25f;
    [SerializeField] float attackDelay = 2f; //Time between attacks
    float lastAttack; //The current time of the last attack

    Transform enemyTransform;
    [SerializeField] Vector3 facingLeft = new Vector3(0f, -180f, 0f);
    [SerializeField] Vector3 facingRight = new Vector3(0f, 0f, 0f);

    bool isPatrolling = false;

    // public PlayerController playerController;

    [SerializeField] string facing;

    public Rigidbody2D rigidBody;

    bool poisoned;


    private void Awake()
    {
        instance = this;
    }

    void Start() //Basic setup and starting position checking for AI
    {
        enemyTransform = GetComponent<Transform>();
        StartCoroutine("PositionCheck");
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
    }

    void EnemyFacing() // checks facing of enemy
    {
        if (facing == "right")
        {
            enemyTransform.rotation = Quaternion.Euler(facingRight);
        }
        else if (facing == "left")
        {
            enemyTransform.rotation = Quaternion.Euler(facingLeft);
        }
    }

    public void Attack()
    {
        Vector3 enemyPos = enemyTransform.position;
        Vector3 playerPos = PlayerController.instance.playerTransform.position;

        if (lastAttack + attackDelay > Time.time)
        {
            return;
        }
        else
        {
            if (playerPos.x - enemyPos.x < 0)
            {
                facing = "left";
            }
            else
            {
                facing = "right";
            }
            lastAttack = Time.time;
            Invoke("DoAttack", .2f);
        }
    }

    public virtual void DoAttack()
    {

    }


    private void Update()
    {
        if (enemyHealth <= 0)
        {
            SFXPlayer.instance.PlayMonsterDeath();
            Destroy(gameObject);
        }
        EnemyFacing();
    }

    /*public void Falling()
    {
        print("Stop patrolling you're falling.");
        StopAllCoroutines();
        StartCoroutine("PositionCheck");
        isPatrolling = false;
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // print("Collided with player should do damage");
            PlayerController.instance.TakeDamage(10);
        }
        if (collision.gameObject.tag == "Ability1")
        {
            float damage = PlayerController.instance.ability1Damage;
            EnemyTakeDamage(damage);
        }
        if (collision.gameObject.tag == "Ability2")
        {
            float damage = PlayerController.instance.ability2Damage;
            EnemyTakeDamage(damage);
        }
        if (collision.gameObject.tag == "Ability3")
        {
            float damage = PlayerController.instance.ability3Damage;
            EnemyTakeDamage(damage);
        }
        if (collision.gameObject.tag == "Ability4")
        {
            float damage = PlayerController.instance.ability4Damage;
            EnemyTakeDamage(damage);
        }
    }

    public void EnemyTakeDamage(float damage)
    {
        if (PlayerController.instance.nineStepPoison > 0 && poisoned == false)
        {
            StartCoroutine("Poison");
        }
        enemyHealth = enemyHealth - damage;
    }

    IEnumerator Poison()
    {
        poisoned = true;
        int poisonStrength = 1; // Multiplier for poison damage. 1 seems reasonable.
        enemyHealth -= (PlayerController.instance.nineStepPoison * poisonStrength);
        yield return new WaitForSeconds(.75f);
        enemyHealth -= (PlayerController.instance.nineStepPoison * poisonStrength);
        yield return new WaitForSeconds(.75f);
        enemyHealth -= (PlayerController.instance.nineStepPoison * poisonStrength);
        yield return new WaitForSeconds(.75f);
        enemyHealth -= (PlayerController.instance.nineStepPoison * poisonStrength);
        yield return new WaitForSeconds(.75f);
        enemyHealth -= (PlayerController.instance.nineStepPoison * poisonStrength);
        yield return new WaitForSeconds(.75f);
        enemyHealth -= (PlayerController.instance.nineStepPoison * poisonStrength);
        yield return new WaitForSeconds(.75f);
        enemyHealth -= (PlayerController.instance.nineStepPoison * poisonStrength);
        yield return new WaitForSeconds(.75f);
        enemyHealth -= (PlayerController.instance.nineStepPoison * poisonStrength);
        yield return new WaitForSeconds(.75f);
        enemyHealth -= (PlayerController.instance.nineStepPoison * poisonStrength);
        yield return new WaitForSeconds(.75f);
        poisoned = false;
    }

    private void FixedUpdate()
    {
        ActionSystem();
    }

    void ActionSystem() //Checks the state of the enemy and starts the appropriate reaction
    {
        if (currentState != State.Patrolling)  //If the enemy isn't in patrol state and is patrolling, stop the patrolling coroutine
        {
            StopCoroutine("Patrolling");
            isPatrolling = false;
            //print("I should stop patrolling");
        }
        if (currentState == State.Falling)
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (currentState == State.ApproachingLeft) //Move the enemy left if it should be chasing the player left
        {
            //print("Actually moving Left");
            rigidBody.AddForce(Vector2.right * Time.deltaTime * -moveSpeed);
        }
        else if (currentState == State.ApproachingRight) //Move the enemy right if it should be chasing the player right
        {
            //print("Actually moving Right");
            rigidBody.AddForce(Vector2.right * Time.deltaTime * moveSpeed);
        }
        else if (currentState == State.Attacking) //Stops the enemy to perform its attack when it should be attacking
        {
            rigidBody.velocity = new Vector2(0, 0);
            Attack();
        }
        else if (currentState == State.Patrolling && isPatrolling == false) //If the enemy is in patrol mode and isn't patrolling yet, set it to patrol
        {
            StartCoroutine("Patrolling");
            //print("I should be patrolling");
        }
    }



    IEnumerator Patrolling()
    {
        isPatrolling = true;
        float currenttime = Time.time;
        //print(currenttime);

        while (Time.time - currenttime <= 2.5f)
        {
            facing = "right";
            var v2 = Vector2.right * 10f; // set the x velocity for patrolling
            v2.y = rigidBody.velocity.y; // set the y velocity to the same as rigidBody y velocity so you don't interupt it
            rigidBody.velocity = v2; // set the new velocity (x that you set, y that it already had)
            yield return null;
        }

        while (Time.time - currenttime > 2.5f && Time.time - currenttime < 5f)
        {
            facing = "left";
            var v2 = Vector2.left * 10f; // set the x velocity for patrolling
            v2.y = rigidBody.velocity.y; // set the y velocity to the same as rigidBody y velocity so you don't interupt it
            rigidBody.velocity = v2; // set the new velocity (x that you set, y that it already had)
            yield return null;
        }

        yield return null;
        StartCoroutine("Patrolling");
    }




    IEnumerator PositionCheck() //Check the position of the enemy relevant to player position
    {
        Vector3 enemyPos = enemyTransform.position;
        Vector3 playerPos = PlayerController.instance.playerTransform.position;


        if (rigidBody.velocity.y < -0.1f)
        {
            StopCoroutine("Patrolling");
            print("I'm in the falling state");
            currentState = State.Falling;
            yield return new WaitForSeconds(.2f);
        }
        else if (Mathf.Abs(playerPos.y - enemyPos.y) >= 8f) //If the y distance is too far apart, patrol
        {
            currentState = State.Patrolling;
            //print("I'm idling!");
            yield return new WaitForSeconds(.5f);
        }
        else if (Mathf.Abs(playerPos.x - enemyPos.x) <= attackDistance) //If x distance is small enough, go into the attack state
        {
            currentState = State.Attacking;
            //print("I'm attacking!");
            yield return new WaitForSeconds(.75f);
        }
        else if (Mathf.Abs(playerPos.x - enemyPos.x) <= 35f) //If the x distance is close enough, chase the player in the appropriate direction
        {
            if (playerPos.x - enemyPos.x < 0)
            {
                //print("I'm moving left!");
                facing = "left";
                currentState = State.ApproachingLeft;
            }
            else
            {
                //print("I'm moving right!");
                facing = "right";
                currentState = State.ApproachingRight;
            }
            yield return new WaitForSeconds(.3f);
        }
        else if (Mathf.Abs(playerPos.x - enemyPos.x) > 35f) //If the enemy is far away from the player, patrol
        {
            currentState = State.Patrolling;
            //print("I'm idling with nothing to do");
            yield return new WaitForSeconds(.75f);
        }

        StartCoroutine("PositionCheck"); //Restart the process
        yield return null;


    }

}
