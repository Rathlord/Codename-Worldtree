using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour {
    
    [SerializeField] public Transform cameraTransform;

    public PlayerController playerController;

    void Update () 
    {
        cameraTransform.position = playerController.transform.position;
	}
}
