using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Weapons/Acorn")]
public class Acorn : RangedWeapon {
    static readonly float V_IX = 0.5f;
    static readonly float V_IY = 0.5f;
    static readonly float OMEGA = 1.0f;

    Rigidbody2D rbody;

    void Start () {
        rbody = GetComponent<Rigidbody2D>();
    }

    public override void Init (string tag, Transform attacker) {
        base.Init(tag, attacker);
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
