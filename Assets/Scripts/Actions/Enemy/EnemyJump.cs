using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Actions/Enemy/Enemy Jump")]
public class EnemyJump : Action {
    public float vy;

    Rigidbody2D rbody;

    void Start () {
        rbody = GetComponent<Rigidbody2D>();
    }

    public override bool IsDone () {
        return true;
    }

    public override void Act (Dictionary<string, object> args) {
        rbody.velocity = new Vector2(rbody.velocity.x, vy);
    }
}
