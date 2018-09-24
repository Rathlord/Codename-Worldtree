using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisTaker : EnemyBehavior {

    [SerializeField] GameObject Beam;
    [SerializeField] Transform attackLocation;

    public override void DoAttack()
    {
        print("Attack?");
        Instantiate(Beam, attackLocation);
    }
}
