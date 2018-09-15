using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour {

    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float projectileSpeed = 40f;
    [SerializeField] float destroyAfterTime = 3f;

    void Awake()
    {

        GameObject player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();


        if (playerController.facing == "right")
        {
            rigidBody.velocity = Vector3.right * projectileSpeed;
        }
        else if (playerController.facing == "left")
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
