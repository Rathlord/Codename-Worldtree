using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLaser : MonoBehaviour {
    
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float attackKnockUp = 1f;
    [SerializeField] float attackKnockBack = 100f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("Why am I not doing a better collision?");
        if(collision.gameObject.tag.Equals("Player"))
        {
            print("I did a collision");
            PlayerController.instance.TakeDamage(attackDamage);
            PlayerController.instance.ForcedMovement(attackKnockUp, attackKnockBack);
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
