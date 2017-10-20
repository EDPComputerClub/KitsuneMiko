using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Character/Death")]
[DisallowMultipleComponent]
[RequireComponent(typeof(Breakable))]
public class Death : MonoBehaviour {

    protected virtual void OnEnable () {
        GetComponent<Breakable>().enabled = false;
        ActionManager actionManager = GetComponent<ActionManager>();
        if (actionManager != null) {
            actionManager.enabled = false;
        }
        SkillManager skillManager = GetComponent<SkillManager>();
        if (skillManager != null) {
            skillManager.enabled = false;
        }
        Capturable capturable = GetComponent<Capturable>();
        if (capturable != null) {
            capturable.enabled = false;
        }
        Damage[] damages = GetComponentsInChildren<Damage>();
        foreach (Damage damage in damages) {
            damage.gameObject.SetActive(false);
        }
    }

    protected virtual void FixedUpdate () {
        Destroy(gameObject);
    }
}
