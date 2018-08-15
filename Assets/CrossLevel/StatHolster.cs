using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatHolster : MonoBehaviour {

    public static StatHolster instance;

    // Access variables and functions from elsewhere with StatHolster.instance.variableOrFunctionName

    public float moveSpeed = 30000f;

    private void Awake() //singleton pattern setup
    {
        int numberOfStatHolsters = FindObjectsOfType<StatHolster>().Length;
        if (numberOfStatHolsters > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        instance = this;
    }

    void ValueChanger()
    {
        
    }

    public void AddMoveSpeed(float changeValue)
    {
        moveSpeed += changeValue;
    }

    public void MultiplyMoveSpeed (float changeValue)
    {
        moveSpeed *= changeValue;
    }

    private void Update()
    {
        ValueChanger();
    }
}
