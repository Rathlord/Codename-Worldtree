using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    GameObject ItemTemplate;
    [SerializeField] Transform itemSpawnLocation;
    [SerializeField] Sprite openChest;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    int IOnlyGiveOneItem = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SpawnItemItem();
            spriteRenderer.sprite = openChest;
            boxCollider.enabled = false;
            SFXPlayer.instance.PlayChestOpen();
        }

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
