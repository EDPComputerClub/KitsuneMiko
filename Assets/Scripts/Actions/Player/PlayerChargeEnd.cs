using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeEnd : Action {

    public GameObject captureBox;
    Animator animator;
    bool _IsDone = false;
	public override bool IsDone()
	{
        return _IsDone;
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

	public override void Act(Dictionary<string, object> args)
	{
        animator.SetBool("isCharging", false);
        animator.SetTrigger("release");
        gameObject.GetComponent<PlayerChargeEndCondition>().Deactivate();
    }

    bool isChargeAnimationFinished = true;
    // condition : 0 => Animation Started, 1 => Animation Ended
    void ChargeAnimationFlag(int condition)
    {
        isChargeAnimationFinished = condition == 1;
        if (!isChargeAnimationFinished)
        {
            captureBox.SetActive(true);
            _IsDone = false;
        }
        else if (isChargeAnimationFinished)
        {
            captureBox.SetActive(false);
            _IsDone = true;
        }
    }

}
