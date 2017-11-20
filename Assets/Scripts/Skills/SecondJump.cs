using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondJump : Skill
{
    float jumpSpeed = 6f;
    float fallEnhance = 15f;
    Rigidbody2D rbody;
    Animator animator;
    // Use this for initialization
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    public override float cost
    {
        get
        {
            return 0.1f;
        }
    }

    protected override void Awake()
    {

    }

    bool isPlayerSecondJumping = false;
    void Update()
    {

        if (!IsPlayerOnGround)
        {
            Vector2 velocity = rbody.velocity;
            if (Input.GetKeyDown(KeyCode.Space) && !isPlayerSecondJumping)
            {
                isPlayerSecondJumping = true;
                rbody.velocity = new Vector2(velocity.x, jumpSpeed);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                rbody.AddForce(Vector2.down * fallEnhance * rbody.velocity.y);
            }
        }
        else 
        {
            if (isPlayerSecondJumping)
            {
                isPlayerSecondJumping = false;
            }
        }
    }

    void FixedUpdate()
    {

    }

    bool IsPlayerOnGround
    {
        get
        {
            return animator.GetBool("onGround");
        }

    }

}
