using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCondition : Condition
{

    int countOfAttacking = 0;
    public int Count
    {
        get
        {
            return countOfAttacking;
        }
        set
        {
            if (value == 0)
            {
                countOfAttacking = 0;
                interval = 0;
            }
        }
    }

    float interval = 0f;
    public float Interval
    {
        get { return interval; }
    }

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
        if (countOfAttacking == 1)
        {
            interval += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (countOfAttacking < 2)
            {
                countOfAttacking++;
            }
        }

        Status.isSatisfied = countOfAttacking > 0;
    }

}
