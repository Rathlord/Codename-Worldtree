using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLaser : MonoBehaviour {
    
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float attackKnockUp = 1f;
    [SerializeField] float attackKnockBack = 100f;

    bool didDamage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (didDamage == false)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                didDamage = true;
                PlayerController.instance.TakeDamage(attackDamage);
                PlayerController.instance.ForcedMovement(attackKnockUp, attackKnockBack);
            }
        }
        else
        {
            return;
        }
    }

    private void Start()
    {
        Invoke("Suicide", .5f);
    }

    private void Suicide()
    {
        Destroy(gameObject);
    }
}
