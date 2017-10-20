using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Conditions/Common/On Ground")]
public class OnGround : Condition {
    public float depth;
    public float width;

    LayerMask block;

    void Awake () {
        block = LayerMask.GetMask("Block");
    }

    public override ConditionState Check (string[] args) {
        Vector2 pos = (Vector2)transform.position;
        Vector2 widthVec = new Vector2(width/2, 0.0f);
        Vector2 depthVec = new Vector2(0.0f, -depth);
        return new ConditionState(
            Physics2D.Linecast(pos + widthVec, pos + depthVec, block)
            || Physics2D.Linecast(pos - widthVec, pos + depthVec, block)
        );
    }
}
