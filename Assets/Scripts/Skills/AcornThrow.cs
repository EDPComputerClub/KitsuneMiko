using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornThrow : Skill {

    ActionManager manager;
    RangedAttack attack;

    public override float cost {
        get {
            return 1.2f;
        }
    }

    ActionConfig[] config = new ActionConfig[] {
        new ActionConfig(
            actionName: "RangedAttack",
            order: 0,
            conditions: new ConditionConfig[] {},
            blockActions: new string[] {}
        )
    };

    protected override void Awake () {
        manager = GetComponent<ActionManager>();
        attack = gameObject.AddComponent<RangedAttack>();
        attack.actionName = "RangedAttack";
        attack.weaponPrefab = Resources.Load("Acorn") as GameObject;
        manager.AddActions(config);
        base.Awake();
    }

    void OnEnable () {
        attack.enabled = true;
    }

    void OnDisable () {
        attack.enabled = false;
    }

    void OnDestroy () {
        manager.RemoveActions(config);
        Destroy(attack);
    }
}
