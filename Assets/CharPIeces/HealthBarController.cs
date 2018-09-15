using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour {

    [SerializeField] public Transform healthBarTransform;

    public PlayerController playerController;
    [SerializeField] Vector3 barOffset;


    void Update()
    {
        healthBarTransform.position = playerController.transform.position + barOffset;

    }
}
