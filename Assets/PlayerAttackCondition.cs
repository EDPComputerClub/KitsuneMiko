using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCondition : Condition
{

    public int countOfAttacking = 0;

    ConditionState Status;
    public override ConditionState Check()
    {
        return Status;
    }

    // Use this for initialization
    void Start()
    {
        countOfAttacking = 0;
        Status = new ConditionState();
        Status.isSatisfied = false;
    }


    // Update is called once per frame
    void Update()
    {
        // detect when player inputs attack code into the game
        if (Input.GetKeyDown(KeyCode.C))
        {
            // detect if the attack count is below the value of three because queue only has to have less than three in integer
            if (countOfAttacking < 3)
            {
                countOfAttacking++;
            }
        }

        Status.isSatisfied = countOfAttacking > 0;

    }

}
