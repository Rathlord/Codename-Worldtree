using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Envy : PlayerController {


    [SerializeField] GameObject daggerToThrow;
    [SerializeField] Transform daggerSpawnRight;
    [SerializeField] Transform daggerSpawnLeft;


    public override void Update()
    {
        base.Update();
        AbilityOne();
    }

    void AbilityOne()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            if (facing == "right")
            {
                Instantiate(daggerToThrow, daggerSpawnRight);
            }
            else if (facing == "left")
            {
                Instantiate(daggerToThrow, daggerSpawnLeft);
            }
        }
    }


}
