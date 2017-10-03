using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpDeclineCondition : Condition {


    public ConditionState Status;
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


	// Update is called once per frame
	void Update () {
        bool isJumping = gameObject.GetComponent<PlayerJumpRise>().isJumping;
        // プレイヤーがジャンプ中にスペースバーを離したかどうか調べる
        if (isJumping && Input.GetKeyUp(KeyCode.Space) && gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            Status.isSatisfied = true;
        }
    }
}
