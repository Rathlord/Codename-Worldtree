using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] enum State { ApproachingLeft, ApproachingRight, Attacking, Patrolling, Falling };
    State currentState;

    [SerializeField] float attackDistance = 3f; //Distance within the enemy will stop to attack the player
    [SerializeField] float moveSpeed = 25000f; //Movement speed factor of the enemy
    [SerializeField] float fallMultiplier = 13f;
    [SerializeField] float attackDelay = 2f; //Time between attacks
    float lastAttack; //The current time of the last attack

    Transform enemyTransform;
    [SerializeField] Vector3 facingLeft = new Vector3(0f,-180f,0f);
    [SerializeField] Vector3 facingRight = new Vector3(0f,0f,0f);

    bool isPatrolling = false;

    public PlayerController playerController;

    float enemyHealth = 40f;

    [SerializeField] string facing;

    public Rigidbody2D rigidBody;



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
        Vector3 playerPos = playerController.playerTransform.position;

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

    void EnemyTakeDamage(int damage)
    {
        enemyHealth = enemyHealth - damage;
    }

    private void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        EnemyFacing();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            print("Fucking stop patrolling");
            StopAllCoroutines();
            StartCoroutine("PositionCheck");
            isPatrolling = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // print("Collided with player should do damage");
            playerController.TakeDamage(10);
        }
        if (collision.gameObject.tag == "Ability1")
        {
            enemyHealth -= playerController.ability1Damage;
        }
        if (collision.gameObject.tag == "Ability2")
        {
            enemyHealth -= playerController.ability2Damage;
        }
        if (collision.gameObject.tag == "Ability3")
        {
            enemyHealth -= playerController.ability3Damage;
        }
        if (collision.gameObject.tag == "Ability4")
        {
            enemyHealth -= playerController.ability4Damage;
        }
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
            rigidBody.velocity = Vector2.right * 10f;
            yield return null;
        }

        while (Time.time - currenttime > 2.5f && Time.time - currenttime < 5f)
        {
            facing = "left";
            rigidBody.velocity = Vector2.left * 10f;
            yield return null;
        }

        yield return null;
        StartCoroutine("Patrolling");
    }




    IEnumerator PositionCheck() //Check the position of the enemy relevant to player position
    {
        Vector3 enemyPos = enemyTransform.position;
        Vector3 playerPos = playerController.playerTransform.position;

        if (rigidBody.velocity.y < 0)
        {
            print("I'm in the falling state");
            currentState = State.Falling;
            yield return new WaitForSeconds(.2f);
        }
        else if (Mathf.Abs(playerPos.y - enemyPos.y) >= 5f) //If the y distance is too far apart, patrol
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
