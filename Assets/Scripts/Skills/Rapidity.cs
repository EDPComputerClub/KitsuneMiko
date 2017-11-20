using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapidity : Skill {
    static readonly float MAGNIFICATION = 3f;

    PlayerWalk move;

    public override float cost {
        get {
            return 0.5f;
        }
    }

    protected override void Awake() {
        move = GetComponent<PlayerWalk>();
    }

    void OnEnable () {
        move.walkSpeed *= MAGNIFICATION;
    }

    void OnDisable () {
        move.walkSpeed /= MAGNIFICATION;
    }
}