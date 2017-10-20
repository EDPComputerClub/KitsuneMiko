using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Actions/Enemy/Turn Direction")]
public class TurnDirection : Action {

    public override bool IsDone () {
        return true;
    }

    public override void Act (Dictionary<string, object> args) {
        transform.SetFaceDir(transform.GetFaceDir().Reverse());
    }
}
