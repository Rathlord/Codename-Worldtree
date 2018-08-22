using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoots : ItemParent {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StatHolster.instance.AddMoveSpeed(5000f);
            StopAllCoroutines();
            Object.Destroy(gameObject);
            print("I collided with speed boots");
        }

    }


}
