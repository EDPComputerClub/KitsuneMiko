using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollisionDetector : Condition {

    public LayerMask groundLayer;

    ConditionState Status;
    public override ConditionState Check(string[] args)
    {
        return Status;
    }
    
    // Use this for initialization
    void Start()
    {
        Status = new ConditionState();
        Status.args.Add("onGround", false);
        Status.isSatisfied = true;
    }

    // Update is called once per frame
    void Update () {
        bool onGround =
            Physics2D.Linecast(transform.position - (transform.right * 0.3f),
            transform.position - (transform.up * 0.1f), groundLayer) ||
            Physics2D.Linecast(transform.position + (transform.right * 0.3f),
            transform.position - (transform.up * 0.1f), groundLayer);
        Status.args["onGround"] = onGround;
    }
}
