using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FafnirsHeart : ItemParent {

    public override void ItemActions()
    {
        PlayerController.instance.fafnirCharges += 1;
    }

}
