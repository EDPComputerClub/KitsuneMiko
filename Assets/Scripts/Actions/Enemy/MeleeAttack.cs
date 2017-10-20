using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Actions/Enemy/Melee Attack")]
public class MeleeAttack : Action {
    public string animationTrigger;

    Animator animator;

    void Start () {
        animator = GetComponent<Animator>();
    }

    bool _isDone = true;
    public override bool IsDone () {
        return _isDone;
    }

    public void MeleeAttackEnd () {
        _isDone = true;
    }

    public override void Act (Dictionary<string, object> args) {
        if (!_isDone) {
            return;
        }
        _isDone = false;
        animator.SetTrigger(animationTrigger);
    }
}
