using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyrfing : ItemParent {

    public override void ItemActions()
    {
        ActivatedItemBehavior.instance.GotTyrfing();
    }

}
