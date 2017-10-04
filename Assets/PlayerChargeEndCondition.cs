using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeEndCondition : Condition {

    PlayerChargeStart playerChargeStart;
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
        playerChargeStart = gameObject.GetComponent<PlayerChargeStart>();
    }

    public float chargingTime = 1f;

    // Update is called once per frame
    void Update () {
        if (playerChargeStart.IsCharging && playerChargeStart.ElapsedTime > chargingTime)
        {
            playerChargeStart.IsCharging = false;
            Status.isSatisfied = true;
        }
    }
}
