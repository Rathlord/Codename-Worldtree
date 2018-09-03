using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour {

    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float projectileSpeed = 40f;
    [SerializeField] float destroyAfterTime = 3f;
    [SerializeField] int damage = 10;


    //public PlayerController playerController;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();


        if (playerController.facing == "right")
        {
            rigidBody.velocity = Vector3.right * projectileSpeed;
        }
        if (playerController.facing == "left")
        {
            rigidBody.velocity = Vector3.left * projectileSpeed;
        }
        else
        {
            rigidBody.velocity = Vector3.right * projectileSpeed;
        }

        Invoke("Suicide", destroyAfterTime);
    }

    void Suicide()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }

}
