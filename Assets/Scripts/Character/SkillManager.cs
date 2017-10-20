using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Character/Skill Manager")]
[DisallowMultipleComponent]
public class SkillManager : MonoBehaviour {
    [System.NonSerialized]
    public float maxMagicPoint = 100.0f;
    [System.NonSerialized]
    public float magicPoint;
    [System.NonSerialized]
    public float naturalRecovery = 1.0f;

    static readonly int MP_UPD_FRAME_NUM = 10;
    int mpUpdateFrameCnt = 0;

    [System.NonSerialized]
    public Dictionary<string, bool> skillDict = new Dictionary<string, bool> {
        {"EmptySkill", true},
        {"DurableBody", false},
        {"Rapidity", false},
        {"AcornThrow", false}
    };

    Skill[] skillSlots = new Skill[3];
    bool isActive = false;
    float totalCost;

    void Awake () {
        magicPoint = maxMagicPoint;
    }

    void Start () {
        System.Type empty = typeof(EmptySkill);
        ReplaceSkills(new System.Type[] {empty, empty, empty});
    }

    void FixedUpdate () {
        mpUpdateFrameCnt++;
        if (mpUpdateFrameCnt != MP_UPD_FRAME_NUM) {
            return;
        }
        if (isActive) {
            magicPoint -= totalCost;
            if (magicPoint <= 0.0f) {
                Deactivate();
                magicPoint = 0.0f;
            }
        } else {
            if (magicPoint < maxMagicPoint) {
                magicPoint += naturalRecovery;
                if (magicPoint > maxMagicPoint) {
                    magicPoint = maxMagicPoint;
                }
            }
        }
    }

    public void ReleaseSkill (string skillName) {
        if (!skillDict[skillName]) {
            skillDict[skillName] = true;
        }
    }

    void _ReplaceSkill (int num, System.Type type) {
        if (skillSlots[num] != null) {
            Destroy(skillSlots[num]);
        }
        skillSlots[num] = gameObject.AddComponent(type) as Skill;
        skillSlots[num].enabled = isActive;
    }

    void UpdateTotalCost () {
        totalCost = skillSlots.Sum(skill => skill.cost);
    }

    public void ReplaceSkill (int num, System.Type type) {
        _ReplaceSkill(num, type);
        UpdateTotalCost();
    }

    public void ReplaceSkills (System.Type[] types) {
        for (int i = 0; i < 3; i++) {
            _ReplaceSkill(i, types[i]);
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
