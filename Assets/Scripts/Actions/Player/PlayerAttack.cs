using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : Action
{

    // 攻撃後のクールダウン時間
    public float sleepTime = 1.5f;

    bool _IsDone = false;
    public override bool IsDone()
    {
        return _IsDone;
    }
    // flag that enables player to input attack key
    private bool isKeyReceived = true;
    // if next attack is registered or not
    private bool isNextRegistered = false;
    // number of next attack
    private int nextNumber = (int)Situations.Idle;
    private enum Situations : int
    {
        Reset = -1, Idle = 0
    }
    GameObject presentWeapon;
    public GameObject purificationStick;
    Animator animator;
    PlayerAttackCondition playerAttackCondition;
    RuntimeAnimatorController attackOriginalAnimation;
    public AnimatorOverrideController[] attackOverrideAnimations;

    public override void Act(Dictionary<string, object> args)
    {
        // Attack Procedure
        AttackRegister();
        if (playerAttackCondition.Count > 1)
        {
            Invoke("AttackRegister", playerAttackCondition.Interval);
        }

        playerAttackCondition.Count = 0;
    }

    bool isAttackAnimationFinished = true;
    // condition : 0 => Animation Started, 1 => Animation Ended
    void AnimationFlag(int condition)
    {
        isAttackAnimationFinished = condition == 1;
        if (condition == 1)
        {
            presentWeapon.SetActive(false);
        }
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        attackOriginalAnimation = animator.runtimeAnimatorController;
        playerAttackCondition = gameObject.GetComponent<PlayerAttackCondition>();
        presentWeapon = purificationStick;
    }

    void AttackRegister()
    {
        _IsDone = false;

        if (!isNextRegistered)
        {
            // 最初の攻撃の実行
            if (nextNumber == (int)Situations.Idle)
            {
                nextNumber = 1;
            }
            // ２回目～４回目の攻撃の予約
            else if (nextNumber > 1 && nextNumber < 5)
            {
                // 今行っているアニメーションが終わっていないのであれば
                // 次の攻撃の予約を入れる
                if (!isAttackAnimationFinished)
                {
                    isKeyReceived = false;
                    isNextRegistered = true;
                }
            }
        }
    }

    void Implementation()
    {
        // １回目の攻撃の処理
        if (nextNumber == 1)
        {
            animator.runtimeAnimatorController = attackOriginalAnimation;
            animator.SetTrigger("attack");
            presentWeapon.SetActive(true);
            nextNumber++;
            isKeyReceived = true;
            isNextRegistered = false;
        }
        // ２回目～４回目の攻撃の処理
        else if (1 < nextNumber && nextNumber < 6 && isAttackAnimationFinished)
        {
            // 攻撃が予約されているなら攻撃実行
            if (isNextRegistered)
            {
                animator.runtimeAnimatorController = attackOverrideAnimations[nextNumber - 2];
                animator.SetTrigger("attack");
                presentWeapon.SetActive(true);
                nextNumber++;
                isKeyReceived = true;
                isNextRegistered = false;
            }
            // 攻撃が予約されていないなら状態を初期化する
            else
            {
                presentWeapon.SetActive(false);
                nextNumber = (int)Situations.Reset;
                isKeyReceived = false;
                isNextRegistered = false;
                Invoke("Initialization", sleepTime);
            }
        }
    }

    void Initialization()
    {
        isKeyReceived = true;
        nextNumber = (int)Situations.Idle;
        _IsDone = true;
    }

    void FixedUpdate()
    {
        Implementation();
    }
}
