using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKeyCondition : Condition {

    ConditionState Status;
	public override ConditionState Check()
	{
        return Status;
    }

    // Use this for initialization
    void Start () {
        Status = new ConditionState();
        Status.isSatisfied = true;
        Status.args.Add("SPACE", false);
    }
	
    // Update is called once per frame
    void Update () {
        Status.args["SPACE"] = Input.GetKey(KeyCode.Space);
    }
}
