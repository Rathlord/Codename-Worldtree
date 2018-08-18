using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParent : MonoBehaviour {

    public Sprite SpeedShoes;




    public void CreateItem(Sprite sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }


	// Use this for initialization
	void Start () {
        Invoke("TestChange", 3f);
	}

    public void TestChange()
    {
        CreateItem(SpeedShoes);
    }
	

}
