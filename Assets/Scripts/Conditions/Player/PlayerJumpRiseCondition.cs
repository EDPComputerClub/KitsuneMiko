using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpRiseCondition : Condition {

    [System.NonSerialized]
    public ConditionState Status;
	public override ConditionState Check(string[] args)
	{
        return Status;
    }
    Animator animator;

	// Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Status = new ConditionState();
        Status.isSatisfied = false;
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && IsPlayerOnGround)
        {
            Status.isSatisfied = true;
        }

        if (IsPlayerJumping)
        {
            Status.isSatisfied = false;
        }
	}

    bool IsPlayerOnGround
    {
        get
        {
            return animator.GetBool("onGround");
        }
        
    }

    bool IsPlayerJumping
    {
        get
        {
            return !IsPlayerOnGround && Status.isSatisfied;
        }
    }
}
