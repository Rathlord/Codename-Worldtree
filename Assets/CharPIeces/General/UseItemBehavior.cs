using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemBehavior : MonoBehaviour {

    public static UseItemBehavior instance;
    public delegate void UseItemDelegate();
    public static event UseItemDelegate UsedItem;


    void Awake()
    {
        instance = this;
        UsedItem += DoNothing;
    }

    public void UseItem()
    {
        UsedItem();
    }

    void ItemPickedUp()
    {
        UsedItem -= DoThing1;
        UsedItem -= DoThing2;
    }

    void GotThing1()
    {
        UsedItem += DoThing1;
    }

    void GotThing2()
    {
        UsedItem += DoThing2;
    }

    void DoThing1()
    {
        //stuff
        UsedItem -= DoThing1;
        UsedItem += DoNothing;
    }

    void DoThing2()
    {
        //stuff
        UsedItem -= DoThing2;
        UsedItem += DoNothing;
    }

    void DoNothing() // Empty item to prevent nullpointer when player presses use key with no items
    {
        print("You can't use nothing, silly");
    }
}
