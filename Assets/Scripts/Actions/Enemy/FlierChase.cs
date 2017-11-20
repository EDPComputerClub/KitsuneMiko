using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Actions/Enemy/Flier Chase")]
[RequireComponent(typeof(Rigidbody2D))]
public class FlierChase : Action {
    public float speed;

    Rigidbody2D rbody;

    void Start () {
        rbody = GetComponent<Rigidbody2D>();
    }

    public override bool IsDone () {
        return true;
    }

    public override void Act (Dictionary<string, object> args) {
        Transform target = (Transform)args["target"];
        Vector2 dir = ((Vector2)(target.position - transform.position)).normalized;
        if (dir.x != 0.0f) {
            transform.SetFaceDir(dir.GenerateFaceDir());
        }
        rbody.velocity = speed * dir;
    }
}
