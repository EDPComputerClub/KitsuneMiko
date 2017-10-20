using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Actions/Common/Ranged Attack")]
[RequireComponent(typeof(Animator))]
public class RangedAttack : Action {

    public GameObject weaponPrefab;
    public Vector2 positionDiff = Vector2.zero;
    public string animationTrigger;

    Animator animator;

    void Start () {
        animator = GetComponent<Animator>();
    }

    bool _isDone = true;
    public override bool IsDone () {
        return _isDone;
    }

    public void RangedAttackEnd () {
        _isDone = true;
    }

    public override void Act (Dictionary<string, object> args) {
        if (!_isDone) {
            return;
        }
        _isDone = false;
        animator.SetTrigger(animationTrigger);
    }

    public void GenerateWeapon () {
        Vector3 diff = new Vector3(
            (float)transform.GetFaceDir() * positionDiff.x,
            positionDiff.y
        );
        Instantiate(
                weaponPrefab,
                transform.position + diff,
                Quaternion.identity
            ).GetComponent<RangedWeapon>().Init(transform);
    }
}
