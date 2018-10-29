using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisTaker : EnemyBehavior {

    [SerializeField] GameObject Beam;
    Animator animator;


    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void DoAttack()
    {
        animator.SetTrigger("IT_Attacking");
        Invoke("EyeBeam", .7f);
    }

    void EyeBeam()
    {
        print("I should be setting the beam active");
        Beam.SetActive(true);
        SFXPlayer.instance.PlayIrisTakerAttack();
    }

}
