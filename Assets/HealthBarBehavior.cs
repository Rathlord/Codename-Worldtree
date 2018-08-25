using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour {

    [SerializeField] Slider slider;
    bool active = true;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (slider.value <= 0)
        {
            gameObject.SetActive(false);
            active = false;
        }
        else if (slider.value >= 0 && active == false)
        {
            gameObject.SetActive(true);
            active = true;
        }
	}
}
