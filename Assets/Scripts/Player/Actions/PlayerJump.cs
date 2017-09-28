using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : Action {

    public AudioClip jumpSE;
    public GameObject manager;
    AudioSource audioSource;
    Rigidbody2D rbody;
    public float jumpForce = 400f;//ジャンプ力
    public float fallEnhance = 15f;
    bool isJumping = false;
    public override bool IsDone()
    {
        return !isJumping;
    }

    // TODO : Fix the JumpKeyCondition.cs so that it can invoke only when it's needed
        // JumpKeyCondition is always returning true so that args can be refreshed
        // This seemingly causes a lot of lag afterwards
    // TODO : Fix the problem that player goes up so high when framerate is not stablized
        // It can be archived easily if you play and immediately hit the space button
    public override void Act(Dictionary<string, object> args)
    {
        bool isSpacePressed = (bool)args["SPACE"];
        bool onGround = (bool)args["onGround"];
        
        if (isSpacePressed && onGround && !isJumping)
        {
            audioSource.PlayOneShot(jumpSE);
            rbody.AddForce(Vector2.up * jumpForce);
            isJumping = true;
        }
        else if (isSpacePressed && isJumping)
        {
            // Do nothing whilst player keeps pressing Jump button
        }
        else if (!isSpacePressed && isJumping && rbody.velocity.y > 0)
        {
            // Deaccelerate the rising player when user presses Jump button away
            rbody.AddForce(Vector2.down * fallEnhance * rbody.velocity.y);
        }
        else if (!isSpacePressed && onGround && isJumping)
        {
            isJumping = false;
        }

        
    }

    // Use this for initialization
    void Start () {
        audioSource = manager.GetComponent<AudioSource>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }
}
