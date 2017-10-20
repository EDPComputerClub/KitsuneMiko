using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionFreezer : MonoBehaviour {
    bool isKinematic;
    Vector2 velocity;
    float anglerVelocity;

    int pauseCallCount = 0;

    Rigidbody2D rbody;
    Animator animator;
    ActionManager actionManager;

    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        actionManager = GetComponent<ActionManager>();
    }

    void Pause () {
        if (pauseCallCount == 0) {
            if (animator != null) {
                animator.speed = 0.0f;
            }
            if (actionManager != null) {
                actionManager.enabled = false;
            }
            if (rbody != null) {
                velocity = rbody.velocity;
                rbody.velocity = Vector2.zero;
                anglerVelocity = rbody.angularVelocity;
                rbody.angularVelocity = 0.0f;
                isKinematic = rbody.isKinematic;
                rbody.isKinematic = true;
            }
        }
        pauseCallCount++;
    }

    void Resume () {
        pauseCallCount--;
        if (pauseCallCount == 0) {
            if (rbody != null) {
                rbody.isKinematic = isKinematic;
                rbody.velocity = velocity;
                rbody.angularVelocity = anglerVelocity;
            }
            if (actionManager != null) {
                actionManager.enabled = true;
            }
            if (animator != null) {
                animator.speed = 1.0f;
            }
        }
    }

    public static void Pause (GameObject obj) {
        MotionFreezer freezer
            = obj.GetComponent<MotionFreezer>() ?? obj.AddComponent<MotionFreezer>();
        freezer.Pause();
    }

    public static void Resume (GameObject obj) {
        MotionFreezer freezer = obj.GetComponent<MotionFreezer>();
        freezer.Resume();
        if (freezer.pauseCallCount == 0) {
            Destroy(freezer);
        }
    }
}
