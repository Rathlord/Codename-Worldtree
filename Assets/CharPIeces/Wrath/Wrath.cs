using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Wrath : PlayerController
{
    Animator animator;

    [SerializeField] GameObject daggerToThrow; // The projectile to be fired
    [SerializeField] Transform daggerPosition; // The position of said projectile

    [SerializeField] GameObject projectileHolster; // This is an empty gameobject where projectiles will be stored to not clutter the scene


    public override void Update()
    {
        base.Update();
        animator.SetFloat("MoveSpeed", Mathf.Abs(xThrow));
        animator.SetBool("Grounded", grounded);
    }

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        ability1Cooldown = .45f;
    }

    public override void AbilityOne()
    {
        animator.SetTrigger("WrathAttack");
        GameObject thisDagger = Instantiate(daggerToThrow, daggerPosition);
        thisDagger.transform.parent = projectileHolster.transform;
    }


}