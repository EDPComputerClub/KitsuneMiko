using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Character/Capturable")]
[DisallowMultipleComponent]
public class Capturable : MonoBehaviour {
    public readonly float hitPointBoundary = 10.0f;

    public string skill;

    void Start () {}

    void OnTriggerEnter2D (Collider2D col) {
        if (!enabled) {
            return;
        }
        if (col.tag == "Player") {
            Capture capture = col.GetComponent<Capture>();
            if (capture != null) {
                capture.Apply(this);
                Destroy(gameObject);
            }
        }
    }
}
