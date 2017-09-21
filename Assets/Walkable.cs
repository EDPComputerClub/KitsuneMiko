using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : Condition {

    ConditionState Status;
	public override ConditionState Check()
	{
        Debug.Log("CALLED");
        return Status;
    }
	
	// Update is called once per frame
	void Update () {
        Status.isSatisfied = Input.GetKeyDown(KeyCode.RightArrow);
	}
}
