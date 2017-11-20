using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Weapons/Acorn")]
[RequireComponent(typeof(Rigidbody2D))]
public class Acorn : RangedWeapon {
    static readonly float V_IX = 5.0f;
    static readonly float V_IY = 10.0f;
    static readonly float OMEGA = 1000.0f;

    public override void Init (Transform attacker) {
        base.Init(attacker);
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        float dir = (float)attacker.GetFaceDir();
        rbody.velocity = new Vector2(dir * V_IX, V_IY);
        rbody.angularVelocity = dir * OMEGA;
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (col.tag != tag) {
            Destroy(gameObject);
        }
    }
}
