using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    [SerializeField] GameObject ItemTemplate;
    [SerializeField] Transform itemSpawnLocation;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Item = Instantiate(ItemTemplate, transform.position, Quaternion.identity);
        print("I should be spawning an " + Item + " now");
        Item.transform.parent = itemSpawnLocation;
        // Object.Destroy(gameObject);
    }

}
