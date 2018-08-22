using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    GameObject ItemTemplate;
    [SerializeField] Transform itemSpawnLocation;

    int IOnlyGiveOneItem = 1;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnItemItem();
        Object.Destroy(gameObject);
    }

    private void SpawnItemItem()
    {
        if (IOnlyGiveOneItem == 1)
        {
            IOnlyGiveOneItem = 0;
            ItemTemplate = ItemHolster.instance.RandomItemPicked();
            GameObject Item = Instantiate(ItemTemplate, transform.position, Quaternion.identity);
            print("I should be spawning an " + Item + " now");
        }
        else
        {
            return;
        }
    }
}
