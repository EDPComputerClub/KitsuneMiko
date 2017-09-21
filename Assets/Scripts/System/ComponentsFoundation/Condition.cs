using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour {
    public string conditionName;

    public virtual ConditionState Check () { return default(ConditionState); }
}

public struct ConditionState {
    public bool isSatisfied;
    public Dictionary<string, object> args;
}