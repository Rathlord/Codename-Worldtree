﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinsHelm : ItemParent {


    public override void ItemActions()
    {
        PlayerController.instance.healthMaximum += 50f;
        PlayerController.instance.Heal(50f);
    }

}
