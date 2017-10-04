using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturable : MonoBehaviour {
    public string skill;

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
