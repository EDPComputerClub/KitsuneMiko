using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkillManager))]
public abstract class Skill : MonoBehaviour {

    public abstract float cost {get;}

    bool _disableByAwake = true;
    protected bool disableByAwake {
        get {
            if (_disableByAwake) {
                _disableByAwake = false;
                return true;
            } else {
                return false;
            }
        }
    }

    protected virtual void Awake () {
        enabled = false;
    }
}
