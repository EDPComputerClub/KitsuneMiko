using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Character/Player Searcher")]
[DisallowMultipleComponent]
public class PlayerSearcher : MonoBehaviour {

    List<Transform> players = new List<Transform>();

    public Transform SearchPlayer () {
        return players.Count == 0 ? null : players[0];
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (col.tag == "Player" && col.GetComponentInParent<Breakable>() != null) {
            players.Add(col.transform.parent);
        }
    }

    void OnTriggerExit2D (Collider2D col) {
        if (col.tag == "Player" && col.GetComponentInParent<Breakable>() != null) {
            players.Remove(col.transform.parent);
        }
    }
}
