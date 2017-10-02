using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeCondition : Condition {

    
    ConditionState Status;
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
        Status.isSatisfied = Input.GetKey(KeyCode.C);
    }
}
