using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpRise : Action
{

    Rigidbody2D rbody;
    public float jumpForce = 200f;//ジャンプ力
	[System.NonSerialized]
    public bool isJumping = false;

    bool _IsDone = true;
    public override bool IsDone()
    {
        return _IsDone;
    }

    public override void Act(Dictionary<string, object> args)
    {
        rbody.AddForce(Vector2.up * jumpForce);
        isJumping = true;
        _IsDone = true;
    }

    // Use this for initialization
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
