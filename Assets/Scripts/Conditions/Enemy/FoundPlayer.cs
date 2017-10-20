using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Conditions/Enemy/Found Player")]
public class FoundPlayer : Condition {
    public string rangeName;

    PlayerSearcher searcher;

    void Start () {
        searcher = transform.Find(rangeName).GetComponent<PlayerSearcher>();
    }

    public override ConditionState Check (string[] args) {
        Transform player = searcher.SearchPlayer();
        if (player == null) {
            return new ConditionState();
        } else {
            return new ConditionState(
                true,
                new Dictionary<string, object> {{"target", player}}
            );
        }
    }
}
