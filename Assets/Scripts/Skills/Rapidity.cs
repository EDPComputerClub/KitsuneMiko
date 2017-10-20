using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapidity : Skill {
    static readonly float MAGNIFICATION = 1.5f;

    //MoveAtion move;

    public override float cost {
        get {
            return 0.5f;
        }
    }

    protected override void Awake() {
        //move = GetComponent<MoveAction>();
    }

    void OnEnable () {
        //move.speed *= MAGNIFICATION;
    }

    void OnDisable () {
        //move.speed /= MAGNIFICATION;
    }
}