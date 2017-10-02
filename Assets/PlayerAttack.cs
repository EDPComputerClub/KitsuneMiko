using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : Action {

    bool _IsDone = false;
    public override bool IsDone()
	{
        return _IsDone;
    }
    int count = 0;
	// Timer that holds animation playtime
    private Timer animationProcessedTimer;
    // flag that enables player to input attack key
    private bool isAttackKeyReceived = true;
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
	GameObject presentWeapon;
    public GameObject purificationStick;
    private Animator animator;  //アニメーター

    public override void Act(Dictionary<string, object> args)
	{
        // Attack Procedure
        Attack();

        gameObject.GetComponent<PlayerAttackCondition>().countOfAttacking = 0;
    }

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animationProcessedTimer = gameObject.GetComponents<Timer>().First(x => x.timerName == "animation");
        presentWeapon = purificationStick;
	}

	void Attack()
    {
        // TODO: キー入力が無くても時間が経てば武器のColliderが消えるようにする -> Update()を使うか、animationのflag eventを使うか。

        // Attack Registration
        if (isAttackKeyReceived && !isNextAttackRegistered)
        {
            _IsDone = false;
            // 1st attack registration and implementation
            if (finishedAttackNum == (int)AttackNumber.Idle && nextAttackNum == (int)AttackNumber.First)
            {
                animator.SetTrigger("attack");
                presentWeapon.SetActive(true);
                animationProcessedTimer.Begin();
                nextAttackNum = (int)AttackNumber.Second;
                Debug.Log("1 attack animation implemented");
            }
            // 2nd to 4th attack registration
            else if ((int)AttackNumber.Second <= nextAttackNum && nextAttackNum <= (int)AttackNumber.Fourth)
            {
                if (animationProcessedTimer.ElapsedTime * 12f < AttackAnimationDuration[nextAttackNum - 2])
                {
                    isAttackKeyReceived = false;
                    isNextAttackRegistered = true;
                }
            }
        }

        // Attack Implementation
        if (1 < nextAttackNum && nextAttackNum < 5 && animationProcessedTimer.ElapsedTime * 12f > AttackAnimationDuration[nextAttackNum - 2])
        {
            if (isNextAttackRegistered)
            {
                // implement next attack if next attack was already registered
                finishedAttackNum += 1;
                nextAttackNum += 1;
                animationProcessedTimer.Begin();
                presentWeapon.SetActive(true);
                animator.SetTrigger("attack");
                isAttackKeyReceived = true;
                isNextAttackRegistered = false;
                Debug.Log((finishedAttackNum + 1) + " attack animation implemented");
            }
            else
            {
                // implement sleep procedure to set up some settings
                nextAttackNum = (int)AttackNumber.Reset;
                animationProcessedTimer.Begin();
                presentWeapon.SetActive(false);
                isAttackKeyReceived = false;
                isNextAttackRegistered = false;
            }
        }

        //when fourth attack animation finished or reset is called
        if (nextAttackNum == (int)AttackNumber.Reset && animationProcessedTimer.ElapsedTime > 1.5f)
        {
            presentWeapon.SetActive(false);
            isAttackKeyReceived = true;
            finishedAttackNum = (int)AttackNumber.Idle;
            nextAttackNum = (int)AttackNumber.First;
            _IsDone = true;
        }
    }
}
