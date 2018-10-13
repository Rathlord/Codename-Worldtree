using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Envy : PlayerController {


    [SerializeField] GameObject daggerToThrow; // The projectile to be fired
    [SerializeField] Transform daggerPosition; // The position of said projectile

    [SerializeField] GameObject projectileHolster; // This is an empty gameobject where projectiles will be stored to not clutter the scene


    public override void Update()
    {
        base.Update();
    }

    public override void Start()
    {
        base.Start();
        ability1Cooldown = .45f;
    }

    public override void AbilityOne()
    {
        GameObject thisDagger = Instantiate(daggerToThrow, daggerPosition);
        thisDagger.transform.parent = projectileHolster.transform;
    }


}
