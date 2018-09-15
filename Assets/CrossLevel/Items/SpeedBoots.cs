using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoots : ItemParent {

    public override void ItemActions()
    {
        StatHolster.instance.AddMoveSpeed(5000f);
    }

}
