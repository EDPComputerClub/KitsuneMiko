using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpRise : Action
{

    Rigidbody2D rbody;
    public float jumpSpeed = 4f;
	[System.NonSerialized]
    public bool isJumping = false;

    Animator animator;

    bool _IsDone = true;
    public override bool IsDone()
    {
        return _IsDone;
    }

    public override void Act(Dictionary<string, object> args)
    {
        Vector2 velocity = rbody.velocity;
        rbody.velocity = new Vector2(velocity.x, jumpSpeed);
        isJumping = true;
        _IsDone = true;
    }

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }
}
