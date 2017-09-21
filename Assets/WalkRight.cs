using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkRight : Action {

	public override bool IsDone()
	{
        return (gameObject.GetComponent<Rigidbody2D>().velocity.x == 0f);
    }

	public override void Act(Dictionary<string, object> args)
	{
        int moveSpeed = 3;
        Rigidbody2D rbody = gameObject.GetComponent<Rigidbody2D>();
        gameObject.transform.localScale = new Vector2(-1, 1);
		rbody.velocity = new Vector2(moveSpeed, rbody.velocity.y);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
