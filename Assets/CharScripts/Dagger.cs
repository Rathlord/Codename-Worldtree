using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour {

    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float projectileSpeed = 30f;


    private void Start()
    {
        if (PlayerController.instance.facing == "right")
        {
            rigidBody.velocity = Vector3.right * 10f;
        }
        if (PlayerController.instance.facing == "left")
        {
            rigidBody.velocity = Vector3.left * 10f;
        }
        else
        {
            rigidBody.velocity = Vector3.right * 10f;
        }
    }


}
