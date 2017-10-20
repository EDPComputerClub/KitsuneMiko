using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Actions/Common/Ranged Attack")]
public class RangedAttack : Action {

    public GameObject weaponPrefab;

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
        Instantiate(weaponPrefab)
            .GetComponent<RangedWeapon>().Init(tag, transform);
    }
}
