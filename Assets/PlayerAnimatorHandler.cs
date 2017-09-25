using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour {

    Animator animator;
    public LayerMask groundLayer;
    Rigidbody2D rbody;
    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }
	
    // Update is called once per frame
    void FixedUpdate () {
        bool canJump =
            Physics2D.Linecast(transform.position - (transform.right * 0.3f),
            transform.position - (transform.up * 0.1f), groundLayer) ||
            Physics2D.Linecast(transform.position + (transform.right * 0.3f),
            transform.position - (transform.up * 0.1f), groundLayer);

        animator.SetBool("onGround", canJump);
        //ストップしているかどうか（アニメーションに影響）
        bool stop = Mathf.Approximately(rbody.velocity.x, 0f);
        animator.SetBool("stop", stop);
    }
}
