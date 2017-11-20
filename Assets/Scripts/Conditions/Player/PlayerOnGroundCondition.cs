using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnGroundCondition : Condition {

	ConditionState Status;
    public override ConditionState Check(string[] args)
    {
        return Status;
    }

    public LayerMask groundLayer;
    Rigidbody2D rbody;
    Animator animator;

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
        Status = new ConditionState();
        Status.isSatisfied = false;
    }
	
    // Update is called once per frame
    void FixedUpdate () {
        if (!IsPlayerOnGround)
        {
            animator.SetBool("onGround", false);
            animator.SetBool("isRising", rbody.velocity.y > 0);
        }
        else
        {
            animator.SetBool("isRising", false);
            animator.SetBool("onGround", true);
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
}
