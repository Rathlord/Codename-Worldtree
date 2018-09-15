using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatHolster : MonoBehaviour {

    public static StatHolster instance;

    // Access variables and functions from elsewhere with StatHolster.instance.variableOrFunctionName

    public float moveSpeed = 30000f;
    public float jumpVelocity = 70f;
    public int jumpCharges = 1;
    public float healthMaximum = 100f;
    public float attackDamage = 10f;
    public float armor;

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

    public void AddArmor(float changeValue)
    {
        armor += changeValue;
    }

    public void AddattackDamage(float changeValue)
    {
        attackDamage += changeValue;
    }

    public void AddMaxHealth(float changeValue)
    {
        healthMaximum += changeValue;
    }

    public void AddMoveSpeed(float changeValue) //Called from other classes to add to speed value
    {
        moveSpeed += changeValue;
    }

    public void MultiplyMoveSpeed (float changeValue) //Called from other classes to multiplicatively add to speed value
    {
        moveSpeed *= changeValue;
    }

    public void AddJumpSpeed(float changeValue) //Called from other classes to add to jump value
    {
        jumpVelocity += changeValue;
    }

    public void MultiplyJumpSpeed(float changeValue) //Called from other classes to multiplicatively add to jump value
    {
        jumpVelocity *= changeValue;
    }


}
