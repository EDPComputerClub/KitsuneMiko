﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Capture : MonoBehaviour {
    static float hitPointRecovery = 10.0f;
    static float magPointRecovery = 10.0f;

    public GameObject player;
    SkillManager manager;
    Breakable breakable;

    void Start () {
        manager = player.GetComponent<SkillManager>();
        breakable = player.GetComponent<Breakable>();
    }

    public void Apply (Capturable capturable) {
        manager.ReleaseSkill(capturable.skill);
        manager.magicPoint += magPointRecovery;
        breakable.hitPoint += hitPointRecovery;
    }
}
