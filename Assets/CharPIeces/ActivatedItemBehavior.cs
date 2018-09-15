using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedItemBehavior : MonoBehaviour {

    public delegate void ActivatedItemDelegate();
    public static event ActivatedItemDelegate ActivatedItem;


    void Start()
    {
        
    }

    void ItemPickedUp()
    {
        ActivatedItem -= DoThing1;
        ActivatedItem -= DoThing2;
    }

    void GotThing1()
    {
        ActivatedItem += DoThing1;
    }

    void GotThing2()
    {
        ActivatedItem += DoThing2;
    }

    void DoThing1()
    {
        
    }

    void DoThing2()
    {
        
    }

}
