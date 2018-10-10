using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoots : ItemParent {


    public override void ItemActions()
    {
        PlayerController.instance.bonusFlatMoveSpeed += 5000f;
    }

}
