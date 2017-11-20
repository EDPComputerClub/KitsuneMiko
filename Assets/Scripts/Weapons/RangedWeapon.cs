using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Damage))]
public abstract class RangedWeapon : MonoBehaviour {

    public virtual void Init (Transform attacker) {
        tag = attacker.tag;
        transform.SetFaceDir(attacker.GetFaceDir());
    }
}
