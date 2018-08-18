using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Object.Destroy(gameObject);
    }

}
