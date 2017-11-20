using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Character/Capture")]
[DisallowMultipleComponent]
public class Capture : MonoBehaviour {
    static float hitPointRecovery = 70.0f;
    static float magPointRecovery = 120.0f;

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
