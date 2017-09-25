using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

    public delegate void RemotedMethodsContainer(Collider2D collider);
    public event RemotedMethodsContainer remotedMethods;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name != "Player")
        {
            switch (collider.gameObject.tag)
            {
                case "Enemy":
                    remotedMethods(collider);
                break;
            }
        }
    }
}
