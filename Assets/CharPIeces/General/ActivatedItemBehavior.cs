using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedItemBehavior : MonoBehaviour {

    public static ActivatedItemBehavior instance;
    public delegate void ActivatedItemDelegate();
    public static event ActivatedItemDelegate ActivatedItem;

    // Each item needs a GotThing to add it to the ActivatedItem event, and a DoThing to tell it what to actually do

    void Awake()
    {
        instance = this;
        ActivatedItem += DoNothing;
    }

    public void ActivateItem()
    {
        ActivatedItem();
    }

    void ItemPickedUp() // Remove all DoThings when an item is picked up
    {
        ActivatedItem -= DoNothing;
        ActivatedItem -= DoTyrfing;
        ActivatedItem -= DoThingTemplate;
    }

    public void GotTyrfing() // Got an item, clear item list and then add it to ActivatedItem
    {
        ItemPickedUp();
        ActivatedItem += DoTyrfing;
    }

    public void GotThingTemplate() // Got an item, clear item list and then add it to ActivatedItem
    {
        ItemPickedUp();
        ActivatedItem += DoThingTemplate;
    }

    void DoNothing() // Empty item to start game with... achieve for finishing with it? Prevents nullpointer
    {
        print("you can't use nothing!");
    }

    void DoTyrfing() // Do thing associated with picking up Tyrfing
    {
        print("Tyrfing activated");
        PlayerController.instance.bonusMultAttackDamage+=1;
        int outcome = Random.Range(0, 10);
        if (outcome == 0)
        {
            PlayerController.instance.currentHealth = PlayerController.instance.currentHealth * .3f;
        }
        SFXPlayer.instance.PlayTyrfing();
        Invoke("EndTyrfing", 7f);
    }

    void DoThingTemplate() // Do thing associated with picking up Thing2
    {

    }

    void EndTyrfing()
    {
        PlayerController.instance.bonusMultAttackDamage -= 1;
    }

}
