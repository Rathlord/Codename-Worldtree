using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    enum State { Approaching };
    State currentState;

    Transform playerPos = PlayerController.instance.playerTransform;

	void Start () 
    {

	}

    IEnumerator PositionCheck()
    {
        yield return null;
    }

}
