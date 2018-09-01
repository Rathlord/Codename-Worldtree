using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Envy : PlayerController {

    void Update()
    {
        Ability1();
    }

    void Ability1()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            
        }
    }


}
