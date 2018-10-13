using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisTaker : EnemyBehavior {

    [SerializeField] GameObject Beam;
    [SerializeField] Transform attackLocation;
    [SerializeField] SpriteRenderer _SpriteRenderer;

    Color PreAttackColor = new Color(255, 0, 0);

    // Color change is a temporary measure while waiting for animations TODO replace with animation

    public override void DoAttack()
    {
        _SpriteRenderer.color = PreAttackColor;
        Invoke("EyeBeam", .5f);
    }

    void EyeBeam()
    {
        _SpriteRenderer.color = new Color(255, 255, 255, 255);
        Instantiate(Beam, attackLocation);
    }

}
