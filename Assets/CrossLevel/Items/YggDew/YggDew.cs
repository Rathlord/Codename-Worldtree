using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YggDew : ItemParent {

    public override void ItemActions()
    {
        if (PlayerController.instance.yggDewCharges == 0)
        {
            PlayerController.instance.YggDew();
        }
        PlayerController.instance.yggDewCharges += 1;
    }

}
