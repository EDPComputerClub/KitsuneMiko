using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Character/Player Searcher")]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSearcher : MonoBehaviour {

    List<Transform> players = new List<Transform>();

    public Transform SearchPlayer () {
        return players.Count == 0 ? null : players[0];
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (
                col.tag == "Player"
                && col.GetComponentInParent<PlayerIdentity>() != null
                && col.transform.parent != null
            ) {
            players.Add(col.transform.parent);
        }
    }

    void OnTriggerExit2D (Collider2D col) {
        if (
            col.tag == "Player"
            && col.GetComponentInParent<PlayerIdentity>() != null
            && col.transform.parent != null
        ) {
            players.Remove(col.transform.parent);
        }
    }
}
