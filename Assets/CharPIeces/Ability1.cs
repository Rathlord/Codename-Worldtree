using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability1 : MonoBehaviour {

    public float ability1Damage;

    void Update()
    {
        ability1Damage = StatHolster.instance.attackDamage;
    }
}
