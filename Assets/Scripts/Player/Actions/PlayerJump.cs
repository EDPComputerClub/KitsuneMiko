using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : Action {

	public override bool IsDone()
	{
        return false;
    }

    public override void Act(Dictionary<string, object> args)
	{
        Debug.Log("Player is on the ground");
    }

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
