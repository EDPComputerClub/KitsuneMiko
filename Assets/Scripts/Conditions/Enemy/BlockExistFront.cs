using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Conditions/Enemy/Block Exist Front")]
public class BlockExistFront : Condition {
    public float height;
    public float length;

    LayerMask block;

    void Awake () {
        block = LayerMask.GetMask("Block");
    }

    public override ConditionState Check (string[] args) {
        Vector2 pos = (Vector2)transform.position;
        return new ConditionState(
            Physics2D.Linecast(
                pos + new Vector2(0.0f, height),
                pos + new Vector2((float)transform.GetFaceDir() * length, height),
                block
            ));
    }
}
