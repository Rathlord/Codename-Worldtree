using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineStepPoison : ItemParent {

    public override void ItemActions()
    {
        PlayerController.instance.nineStepPoison += 1;
    }

}
