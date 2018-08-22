using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehavior : MonoBehaviour {

    enum State { ApproachingLeft, ApproachingRight, Attacking, Idling };
    State currentState;

    [SerializeField] float attackDistance = 3f; //Distance within the enemy will stop to attack the player
    [SerializeField] float moveSpeed = 2500f; //Movement speed factor of the enemy

    Transform enemyTransform;

    bool isPatrolling = false;


    Rigidbody2D rigidBody;


	void Start () //Basic setup and starting position checking for AI
    {
        enemyTransform = GetComponent<Transform>();
        StartCoroutine("PositionCheck");
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
    }

    void Attack()
    {
        print("I should be attacking");
    }

    private void FixedUpdate()
    {
        ActionSystem();
    }

    void ActionSystem() //Checks the state of the enemy and starts the appropriate reaction
    {
        if (currentState == State.Idling && isPatrolling == false) //If the enemy is in idle mode and isn't patrolling yet, set it to patrol
        {
            isPatrolling = true;
            StartCoroutine(Patrolling());
            print("I should be patrolling");
        }
        else if (currentState != State.Idling && isPatrolling == true) //If the enemy isn't idling and is patrolling, stop the patrolling coroutine
        {
            StopCoroutine(Patrolling());
            isPatrolling = false;
            print("I should stop patrolling");
        }
        if (currentState == State.ApproachingLeft) //Move the enemy left if it should be chasing the player left
        {
            print("Actually moving Left");
            rigidBody.AddForce(Vector2.right * Time.deltaTime * -moveSpeed);
        }
        if (currentState == State.ApproachingRight) //Move the enemy right if it should be chasing the player right
        {
            print("Actually moving Right");
            rigidBody.AddForce(Vector2.right * Time.deltaTime * moveSpeed);
        }
        if (currentState == State.Attacking) //Stops the enemy to perform its attack when it should be attacking
        {
            rigidBody.velocity = new Vector2(0, 0);
            Attack();
        }
    }

   IEnumerator Patrolling() //Begin patrolling to the right, wait for two seconds, stop patrolling right and start patrolling left. Wait two seconds and stop patrolling. Then restart the cycle.
    {
        print("Patrolling Started");

        StartCoroutine(PatRight());
        print("Starting Right");
        yield return new WaitForSeconds(2f);
        print("Stopping Right");
        StopCoroutine(PatRight());
        print("Starting Left");
        StartCoroutine(PatLeft());
        yield return new WaitForSeconds(2f);
        print("Stopping Left");
        StopCoroutine(PatLeft());


        StartCoroutine(Patrolling());
    }

    IEnumerator PatRight() //Patrol right for a time
    {
        print("Patrolling right");
        for (int i = 0; i <= 50; i++)
        {
            rigidBody.AddForce(Vector2.right * Time.deltaTime * 10000f);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
    }

    IEnumerator PatLeft() //Patrol left for a time
    {
        print("Patrolling left");
        for (int i = 0; i <= 50; i++)
        {
            rigidBody.AddForce(Vector2.right * Time.deltaTime * -10000f);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
    }


    IEnumerator PositionCheck() //Check the position of the enemy relevant to player position
    {
        Vector3 enemyPos = enemyTransform.position;
        Vector3 playerPos = PlayerController.instance.playerTransform.position;

 
        if (Mathf.Abs(playerPos.x - enemyPos.x) <= attackDistance) //If x distance is small enough, go into the attack state
        {
            currentState = State.Attacking;
            //print("I'm attacking!");
            yield return new WaitForSeconds(.75f);
        }
        else if (Mathf.Abs(playerPos.y - enemyPos.y) >= 5f) //If the y distance is too far apart, idle
        {
            currentState = State.Idling;
            //print("I'm idling!");
            yield return new WaitForSeconds(.5f);
        }
        else if (Mathf.Abs(playerPos.x - enemyPos.x) <= 35f) //If the x distance is close enough, chase the player in the appropriate direction
        {
            if (playerPos.x - enemyPos.x < 0)
            {
                print("I'm moving left!");
                currentState = State.ApproachingLeft;
            }
            else
            {
                print("I'm moving right!");
                currentState = State.ApproachingRight;
            }
            yield return new WaitForSeconds(.3f);
        }
        else if (Mathf.Abs(playerPos.x - enemyPos.x) > 35f) //If the enemy is far away from the player, idle
        {
            currentState = State.Idling;
            print("I'm idling with nothing to do");
            yield return new WaitForSeconds(.75f);
        }

        yield return null;
        StartCoroutine("PositionCheck"); //Restart the process

    }

}
