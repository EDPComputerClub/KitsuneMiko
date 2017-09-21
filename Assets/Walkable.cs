using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : Condition {

    ConditionState Status;
	public override ConditionState Check()
	{
        return Status;
    }
	
    void Start()
    {
        Status = new ConditionState();
    }

	// Update is called once per frame
	void Update () {
        Status.isSatisfied = Input.GetKeyDown(KeyCode.RightArrow);
	}
}
