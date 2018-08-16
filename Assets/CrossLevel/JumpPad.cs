using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

    [SerializeField] float jumpPadPower = 30f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.instance.ForcedMovement(jumpPadPower, 0f);
    }

}
