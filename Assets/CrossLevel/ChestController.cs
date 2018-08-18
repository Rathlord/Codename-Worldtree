using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    GameObject ItemTemplate;
    [SerializeField] Transform itemSpawnLocation;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemTemplate = ItemHolster.instance.RandomItemPicked();
        GameObject Item = Instantiate(ItemTemplate, transform.position, Quaternion.identity);
        print("I should be spawning an " + Item + " now");
        Object.Destroy(gameObject);
    }

}
