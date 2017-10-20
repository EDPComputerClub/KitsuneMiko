using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Actions/Enemy/Move Forward")]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class MoveForward : Action {
    public float speed;
    public string animationTrigger;

    Rigidbody2D rbody;
    Animator animator;

    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public override bool IsDone () {
        return true;
    }

    public override void Act (Dictionary<string, object> args) {
        animator.SetTrigger(animationTrigger);
        rbody.velocity
            = new Vector2((float)transform.GetFaceDir() * speed, rbody.velocity.y);
    }
}
