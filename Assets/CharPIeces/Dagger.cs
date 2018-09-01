using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour {

    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float projectileSpeed = 30f;

    public PlayerController playerController;

    private void Start()
    {
        print(playerController.facing);
        if (playerController.facing == "right")
        {
            rigidBody.velocity = Vector3.right * 10f;
        }
        if (playerController.facing == "left")
        {
            rigidBody.velocity = Vector3.left * 10f;
        }
        else
        {
            rigidBody.velocity = Vector3.right * 10f;
        }
    }


}
