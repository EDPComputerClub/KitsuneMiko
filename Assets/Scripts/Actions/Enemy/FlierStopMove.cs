using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Actions/Enemy/Flier Stop Move")]
public class FlierStopMove : Action {
    Rigidbody2D rbody;

    void Start () {
        rbody = GetComponent<Rigidbody2D>();
    }

    public override bool IsDone () {
        return true;
    }

    public override void Act (Dictionary<string, object> args) {
        rbody.velocity = Vector2.zero;
    }
}
