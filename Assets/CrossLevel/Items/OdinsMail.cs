using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinsMail : ItemParent {

    public override void ItemActions()
    {
        StatHolster.instance.AddArmor(3f);
    }

}
