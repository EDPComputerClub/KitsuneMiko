using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurableBody : Skill {
    static readonly float MAGNIFICATION = 2.0f;

    Breakable breakable;

    public override float cost {
        get {
            return 0.1f;
        }
    }

    protected override void Awake () {
        breakable = GetComponent<Breakable>();
        base.Awake();
    }

    void OnEnable () {
        breakable.defencePoint /= MAGNIFICATION;
    }

    void OnDisable () {
        if (disableByAwake) {
            return;
        }
        breakable.defencePoint *= MAGNIFICATION;
    }
}
