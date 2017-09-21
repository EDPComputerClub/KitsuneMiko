using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkCondition : Condition {

    public enum WALK_DIR
    {
        IDLE, RIGHT, LEFT
    }

    WALK_DIR movingDirection = WALK_DIR.IDLE;

    ConditionState Status;
	public override ConditionState Check()
	{
        return Status;
    }
    
	// Use this for initialization
    void Start()
    {
        Status = new ConditionState();
        Status.args.Add("movingDirection", WALK_DIR.IDLE);
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movingDirection = WALK_DIR.LEFT;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            movingDirection = WALK_DIR.RIGHT;
        }
        else
        {
            movingDirection = WALK_DIR.IDLE;
        }
        // movingDirectionをstring型でPlayerWalk.csに渡してPlayerWalk.csでenum型に直す
        Status.args["movingDirection"] = movingDirection.ToString();

        // Idle => Idle時の移行では常にisSatisfiedはfalseになる.
        // それ以外の場合はActionを常に実行しなければいけないのでtrue
        Status.isSatisfied = gameObject.GetComponent<Rigidbody2D>().velocity.x != 0f || movingDirection != WALK_DIR.IDLE;
    }
}
