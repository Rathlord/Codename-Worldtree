using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolster : ItemParent {

    public static ItemHolster instance;

    public string PickItem()
    {
        string itemPicked;
        itemPicked = "ItemTemplate";
        return itemPicked;
    }

    private void Awake()
    {
        instance = this;
    }


}
