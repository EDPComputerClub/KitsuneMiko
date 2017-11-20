using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Character/Breakable")]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
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

    Capturable capturable;

    bool isInvincible = false;
    int invCallCount = 0;

    public int invFrameNum = 0;
    int invFrameCount = 0;
    bool isInvByDamage = false;

    public int hitStopFrameNum = 16;
    int hitStopFrameCnt = 0;
    bool isHitStopping = false;

    void Awake () {
        hitPoint = maxHitPoint;
    }

    void Start () {
        capturable = GetComponent<Capturable>();
    }

    void FixedUpdate () {
        if (hitPoint <= 0.0f) {
            GetComponent<Death>().enabled = true;
            return;
        }
        if (capturable != null) {
            float boundary = capturable.hitPointBoundary;
            if (capturable.enabled) {
                if (hitPoint > boundary) {
                    capturable.enabled = false;
                }
            } else {
                if (hitPoint <= boundary) {
                    capturable.enabled = true;
                }
            }
        }
        if (isInvByDamage) {
            invFrameCount++;
            if (invFrameCount > invFrameNum) {
                isInvByDamage = false;
                invFrameCount = 0;
                EndInvincible();
            }
        }
        if (isHitStopping) {
            hitStopFrameCnt++;
            if (hitStopFrameCnt > hitStopFrameNum) {
                isHitStopping = false;
                hitStopFrameCnt = 0;
                MotionFreezer.Resume(gameObject);
            }
        }
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (!enabled || isInvincible) {
            return;
        }
        if (DAMAGE_TAGS[tag].Contains(col.tag)) {
            Damage damage = col.GetComponent<Damage>();
            if (damage != null) {
                damage.Apply(this);
                if (invFrameNum != 0) {
                    isInvByDamage = true;
                    BeginInvincible();
                }
                if (hitStopFrameNum != 0) {
                    if (isHitStopping) {
                        hitStopFrameCnt = 0;
                    } else {
                        isHitStopping = true;
                        MotionFreezer.Pause(gameObject);
                    }
                }
            }
        }
    }

    void OnDisable () {
        if (isInvByDamage) {
            isInvByDamage = false;
            invFrameCount = 0;
            EndInvincible();
        }
        if (isHitStopping) {
            isHitStopping = false;
            hitStopFrameCnt = 0;
            MotionFreezer.Resume(gameObject);
        }
    }

    public void BeginInvincible () {
        if (invCallCount == 0) {
            isInvincible = true;
        }
        invCallCount++;
    }

    public void EndInvincible () {
        invCallCount--;
        if (invCallCount == 0) {
            isInvincible = false;
        }
    }
}
