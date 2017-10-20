using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondJump : Skill
{
    public float jumpSpeed = 6f;
    public float fallEnhance = 15f;
    public LayerMask groundLayer;
    Rigidbody2D rbody;
    // Use this for initialization
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
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
            bool onGround =
                Physics2D.Linecast(transform.position - (transform.right * 0.3f),
                transform.position - (transform.up * 0.1f), groundLayer) ||
                Physics2D.Linecast(transform.position + (transform.right * 0.3f),
                transform.position - (transform.up * 0.1f), groundLayer);

            return onGround;
        }

    }

}
