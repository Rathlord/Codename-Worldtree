using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

    [SerializeField] float jumpPadPower = 1000f;

    public PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SFXPlayer.instance.PlayJumpPad();
            playerController.ForcedMovement(jumpPadPower, 0f);
        }
    }

}
