﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {
    public float maxMagicPoint = 100.0f;
    public float mugicPoint = 100.0f;
    public float recoveryAmount = 1.0f;

    Skill[] skillSlots = new Skill[3];
    bool isActive = false;
    float totalCost;

    void Start () {
        // 空Skillをセット
    }

    void FixedUpdate () {
        if (isActive) {
            mugicPoint -= totalCost;
            if (mugicPoint <= 0) {
                Deactivate();
                mugicPoint = 0.0f;
            }
        } else {
            if (mugicPoint < maxMagicPoint) {
                mugicPoint += recoveryAmount;
                if (mugicPoint > maxMagicPoint) {
                    mugicPoint = maxMagicPoint;
                }
            }
        }
    }

    void _ReplaceSkill (int num, string name) {
        System.Type skillType = System.Type.GetType(name);
        Destroy(skillSlots[num]);
        skillSlots[num] = gameObject.AddComponent(skillType) as Skill;
        skillSlots[num].enabled = isActive;
    }

    void UpdateTotalCost () {
        foreach (Skill skill in skillSlots) {
            totalCost += skill.cost;
        }
    }

    public void ReplaceSkill (int num, string name) {
        _ReplaceSkill(num, name);
        UpdateTotalCost();
    }

    public void ReplaceSkills (string[] names) {
        for (int i = 0; i < 3; i++) {
            _ReplaceSkill(i, names[i]);
        }
        UpdateTotalCost();
    }

    public void Activate () {
        isActive = true;
        foreach (Skill skill in skillSlots) {
            skill.enabled = true;
        }
    }

    public void Deactivate () {
        isActive = false;
        foreach (Skill skill in skillSlots) {
            skill.enabled = false;
        }
    }
}
