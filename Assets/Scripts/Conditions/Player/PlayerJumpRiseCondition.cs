using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpRiseCondition : Condition {

    public LayerMask groundLayer;

    [System.NonSerialized]
    public ConditionState Status;
	public override ConditionState Check(string[] args)
	{
        return Status;
    }
    
	// Use this for initialization
    void Start()
    {
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
            bool onGround =
                Physics2D.Linecast(transform.position - (transform.right * 0.3f),
                transform.position - (transform.up * 0.1f), groundLayer) ||
                Physics2D.Linecast(transform.position + (transform.right * 0.3f),
                transform.position - (transform.up * 0.1f), groundLayer);

            return onGround;
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
