using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollisionDetector : Condition {

    public LayerMask groundLayer;

    ConditionState Status;
	public override ConditionState Check()
	{
        return Status;
    }
    
	// Use this for initialization
    void Start()
    {
        Status = new ConditionState();
    }

	// Update is called once per frame
	void Update () {
		bool isOnGround =
            Physics2D.Linecast(transform.position - (transform.right * 0.3f),
            transform.position - (transform.up * 0.1f), groundLayer) ||
            Physics2D.Linecast(transform.position + (transform.right * 0.3f),
            transform.position - (transform.up * 0.1f), groundLayer);

        Status.isSatisfied = isOnGround;
    }
}
