using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Action : MonoBehaviour {
    public string actionName;

    public virtual bool IsDone() { return false; }
    public virtual void Act (Dictionary<string, object> args) { return; }
}
