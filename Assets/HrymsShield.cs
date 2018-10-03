using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HrymsShield : ItemParent {

    public override void ItemActions()
    {
        PlayerController.instance.hrymsCharges++;
    }

}
