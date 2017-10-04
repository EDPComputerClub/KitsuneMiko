using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour {
    public string conditionName;

    public abstract ConditionState Check ();
}

public class ConditionState {
    public bool isSatisfied;
    public Dictionary<string, object> args;
    
    public ConditionState () : this(false, new Dictionary<string, object>())
    {

    }

    public ConditionState (bool _isSatistfried, Dictionary<string, object> _args)
    {
        this.isSatisfied = _isSatistfried;
        this.args = _args;
    }
}