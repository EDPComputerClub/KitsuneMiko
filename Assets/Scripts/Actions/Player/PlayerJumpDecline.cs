using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpDecline : Action {

    Rigidbody2D rbody;
    public float fallEnhance = 15f;
    bool _IsDone = false;
    public override bool IsDone()
	{
        return _IsDone;
    }

	public override void Act(Dictionary<string, object> args)
	{
        rbody.AddForce(Vector2.down * fallEnhance * rbody.velocity.y);
        _IsDone = true;
        // 一度ジャンプの減速を行えばPlayerJumpDeclineのConditionのStatus.isSatisfiedをtrueにしてやる
        gameObject.GetComponent<PlayerJumpDeclineCondition>().Status.isSatisfied = false;
    }

	// Use this for initialization
	void Start () {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

	}
}
