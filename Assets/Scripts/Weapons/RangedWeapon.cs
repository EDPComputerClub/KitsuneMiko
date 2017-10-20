using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Damage))]
public abstract class RangedWeapon : MonoBehaviour {

    public virtual void Init (string tag, Transform attacker) {
        this.tag = tag;
        transform.position = attacker.position;
        transform.SetFaceDir(attacker.GetFaceDir());
    }
}
