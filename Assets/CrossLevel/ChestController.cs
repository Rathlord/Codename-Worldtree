using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ChestController : MonoBehaviour
{
    public static ChestController instance;
    GameObject ItemTemplate;
    [SerializeField] Transform itemSpawnLocation;
    [SerializeField] Sprite openChest;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    [SerializeField] bool inRangeOfChest = false;

    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            SpawnItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRangeOfChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRangeOfChest = false;
        }
    }

    public void SpawnItem()
    {
        if (inRangeOfChest == true)
        {
            print("I'm opening");
            spriteRenderer.sprite = openChest;
            boxCollider.enabled = false;
            SFXPlayer.instance.PlayChestOpen();
            ItemTemplate = ItemHolster.instance.RandomItemPicked();
            GameObject Item = Instantiate(ItemTemplate, transform.position, Quaternion.identity);
            print("I should be spawning an " + Item + " now");
        }
    }
}
