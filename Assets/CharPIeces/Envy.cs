using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Envy : PlayerController {


    [SerializeField] GameObject daggerToThrow;
    [SerializeField] Transform daggerPosition;

    [SerializeField] GameObject projectileHolster;


    public override void Update()
    {
        base.Update();
    }

    public override void AbilityOne()
    {
        print("Is this working?");
        GameObject thisDagger = Instantiate(daggerToThrow, daggerPosition);
        thisDagger.transform.parent = projectileHolster.transform;
    }


}
