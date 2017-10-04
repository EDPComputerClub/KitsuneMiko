using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : MonoBehaviour {
    public GameObject player;
    SkillManager manager;
    Breakable breakable;

    void Start () {
        manager = player.GetComponent<SkillManager>();
        breakable = player.GetComponent<Breakable>();
    }

    public void Apply (Capturable capturable) {
        if (!manager.skillDict[capturable.skill]) {
            manager.skillDict[capturable.skill] = true;
        }
        manager.magicPoint += manager.captureRecovery;
        breakable.hitPoint += breakable.captureRecovery;
    }
}
