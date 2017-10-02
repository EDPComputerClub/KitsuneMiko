using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpDeclineCondition : Condition {


    ConditionState Status;
	public override ConditionState Check()
	{
        return Status;
    }
    
	// Use this for initialization
    void Start()
    {
        Status = new ConditionState();
        Status.isSatisfied = false;
    }

    // TODO : Status.isSatisfiedはPlayerJumpDecline.csから行うべき

	// Update is called once per frame
	void Update () {
        bool isJumping = gameObject.GetComponent<PlayerJumpRise>().isJumping;
        if (Status.isSatisfied)
        {
            Debug.Log(Status.isSatisfied);
        }
        if (isJumping && Input.GetKeyUp(KeyCode.Space) && gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            Status.isSatisfied = true;
        }
        else
        {
            Status.isSatisfied = false;
        }
    }
}
