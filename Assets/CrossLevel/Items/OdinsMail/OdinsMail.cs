﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinsMail : ItemParent {

    public override void ItemActions()
    {
        PlayerController.instance.armor += 3f;
    }

}
