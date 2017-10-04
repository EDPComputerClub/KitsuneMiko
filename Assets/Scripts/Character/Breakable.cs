using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
    static readonly Dictionary<string, HashSet<string>> DAMAGE_TAGS
        = new Dictionary<string, HashSet<string>> {
            {"Player", new HashSet<string> {"Enemy", "Neutral"}},
            {"Enemy", new HashSet<string> {"Player", "Neutral"}},
            {"Neutral", new HashSet<string> {"Player", "Enemy", "Neutral"}}
        };

    public float maxHitPoint;
    public float defencePoint = 1.0f;

    [System.NonSerialized]
    public float hitPoint;

    static readonly float CAPTURE_BOUNDARY = 10.0f;
    Capturable capturable;

    [System.NonSerialized]
    public float captureRecovery = 10.0f;

    void Awake () {
        hitPoint = maxHitPoint;
    }

    void Start () {
        capturable = GetComponent<Capturable>();
    }

    void FixedUpdate () {
        if (hitPoint <= 0.0f) {
            GetComponent<Death>().enabled = true;
        } else if (capturable != null) {
            if (!capturable.enabled && hitPoint <= CAPTURE_BOUNDARY) {
                capturable.enabled = true;
            } else if (capturable.enabled && hitPoint > CAPTURE_BOUNDARY) {
                capturable.enabled = false;
            }
        }
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (DAMAGE_TAGS[tag].Contains(col.tag)) {
            Damage damage = col.GetComponent<Damage>();
            if (damage != null) {
                damage.Apply(this);
            }
        }
    }
}
