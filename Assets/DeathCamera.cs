using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour {
    
    [SerializeField] public Transform cameraTransform;
    bool untransformed = true;

    void Update () 
    {
        if (PlayerController.instance.dead == true && untransformed == true)
        {
            untransformed = false;
            cameraTransform.Rotate(0.0f, 0.0f, -90f, Space.World);
        }
	}
}
