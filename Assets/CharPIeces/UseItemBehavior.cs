using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemBehavior : MonoBehaviour {

    public delegate void UseItemDelegate();
    public static event UseItemDelegate UseItem;


    void Start()
    {

    }

    void ItemPickedUp()
    {
        UseItem -= DoThing1;
        UseItem -= DoThing2;
    }

    void GotThing1()
    {
        UseItem += DoThing1;
    }

    void GotThing2()
    {
        UseItem += DoThing2;
    }

    void DoThing1()
    {
        //stuff
        UseItem -= DoThing1;    
    }

    void DoThing2()
    {
        //stuff
        UseItem -= DoThing2;
    }
}
