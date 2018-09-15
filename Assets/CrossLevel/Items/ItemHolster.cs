using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolster : MonoBehaviour {

    public static ItemHolster instance;

    public GameObject[] ItemList;

    public GameObject RandomItemPicked()
    {
        return ItemList[Random.Range(0, ItemList.Length)];
    }

    private void Awake()
    {
        instance = this;
    }


}
