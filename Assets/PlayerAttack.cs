using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Action {

    bool _isDone = true;
    public override bool IsDone()
    {
        return _isDone;
    }

    // timer that holds animation playtime
    private Timer timer;
    // flag that enables player to input attack key
    private bool isAttackKeyReceiving = true;
    // if next attack is registered or not
    private bool isNextAttackRegistered = false;
    // counts of finished animations
    private int finishedAttackNum = 0;
    // number of next attack
    private int nextAttackNum = 1;
    private enum AttackNumber : int
    {
        Idle = 0, First, Second, Third, Fourth, Reset
    }
    private int[] AttackAnimationDuration = new int[4] { 6, 6, 6, 6 };
    Animator animator;
    public override void Act(Dictionary<string, object> args)
    {
        // Attack Registration
        if (isNextAttackRegistered)
        {
            // 1st attack registration and implementation
            if (finishedAttackNum == (int)AttackNumber.Idle && nextAttackNum == (int)AttackNumber.First)
            {
                animator.SetTrigger("attack");
                timer.Begin();
                nextAttackNum = (int)AttackNumber.Second;
                Debug.Log("1 attack animation implemented");
            }
            // 2nd to 4th attack registration
            else if ((int)AttackNumber.Second <= nextAttackNum && nextAttackNum <= (int)AttackNumber.Fourth)
            {
                if (timer.ElapsedTime * 12f < AttackAnimationDuration[nextAttackNum - 2])
                {
                    isAttackKeyReceiving = false;
                    isNextAttackRegistered = true;
                }
            }
        }

        // Attack Implementation
        if (1 < nextAttackNum && nextAttackNum < 5 && timer.ElapsedTime * 12f > AttackAnimationDuration[nextAttackNum - 2])
        {
            if (isNextAttackRegistered)
            {
                // implement next attack if next attack was already registered
                finishedAttackNum += 1;
                nextAttackNum += 1;
                timer.Begin();
                animator.SetTrigger("attack");
                isAttackKeyReceiving = true;
                isNextAttackRegistered = false;
                Debug.Log((finishedAttackNum + 1) + " attack animation implemented");
            }
            else
            {
                // implement sleep procedure to set up some settings
                nextAttackNum = (int)AttackNumber.Reset;
                timer.Begin();
                isAttackKeyReceiving = false;
                isNextAttackRegistered = false;
            }
        }

        //when fourth attack animation finished
        if (nextAttackNum == (int)AttackNumber.Reset && timer.ElapsedTime * 12f > 6f * 1.5f)
        {
            isAttackKeyReceiving = true;
            finishedAttackNum = 0;
            nextAttackNum = 1;
        }
    }

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
