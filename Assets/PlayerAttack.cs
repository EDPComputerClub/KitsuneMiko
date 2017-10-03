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

    bool isAttackAnimationFinished = true;
    void AnimationStart()
    {
        isAttackAnimationFinished = true;
    }
    void AnimationEnd()
    {
        isAttackAnimationFinished = false;
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

        _IsDone = false;

        if (!isNextAttackRegistered)
        {
            // 最初の攻撃の実行
            if (finishedAttackNum == (int)AttackNumber.Idle && nextAttackNum == (int)AttackNumber.First)
            {
                animator.SetTrigger("attack");
                presentWeapon.SetActive(true);
                nextAttackNum = (int)AttackNumber.Second;
                Debug.Log("1 attack animation implemented");
            }
            // ２回目～４回目の攻撃の予約
            else if (nextAttackNum >= (int)AttackNumber.Second && nextAttackNum <= (int)AttackNumber.Fourth)
            {
                // 今行っているアニメーションが終わっていないのであれば
                // 次の攻撃の予約を入れる
                if (!isAttackAnimationFinished)
                {
                    isAttackKeyReceived = false;
                    isNextAttackRegistered = true;
                }
            }
        }

        // ２回目～４回目の攻撃の実行
        if (1 < nextAttackNum && nextAttackNum < 5 && isAttackAnimationFinished)
        {
            // 攻撃が予約されているなら
            if (isNextAttackRegistered)
            {
                finishedAttackNum += 1;
                nextAttackNum += 1;
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
                presentWeapon.SetActive(false);
                isAttackKeyReceived = false;
                isNextAttackRegistered = false;
            }
        }
    }
    
    void AttackInitialization()
    {
        

        // ４回目の攻撃が終わった後、またはリセットが呼び出されときに初期化をする
        if (nextAttackNum == (int)AttackNumber.Reset && animationProcessedTimer.ElapsedTime > 1.5f)
        {
            presentWeapon.SetActive(false);
            isAttackKeyReceived = true;
            finishedAttackNum = (int)AttackNumber.Idle;
            nextAttackNum = (int)AttackNumber.First;
            _IsDone = true;
        }
    }

    void Update()
    {
        // 攻撃アニメーションが終了後、一定時間キー入力を受け付けず、その後初期化する
        if (nextAttackNum > 0 && !isNextAttackRegistered && isAttackAnimationFinished)
        {

        }
    }
}
