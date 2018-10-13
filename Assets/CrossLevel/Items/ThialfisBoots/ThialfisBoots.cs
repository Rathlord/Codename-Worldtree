using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThialfisBoots : ItemParent {


    public override void ItemActions()
    {
        PlayerController.instance.bonusFlatMoveSpeed += 5000f;
    }

}
