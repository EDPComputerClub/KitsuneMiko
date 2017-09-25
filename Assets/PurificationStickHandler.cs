using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurificationStickHandler : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<CollisionDetector>().remotedMethods += HitMethod;
        gameObject.SetActive(false);
    }

    void HitMethod(Collider2D collider)
    {
        EnemyManager enemyManager = collider.GetComponent<EnemyManager>();
        int EnemyHP = enemyManager.HP;
        int EnemyPoint = enemyManager.ENEMY_POINT;

        enemyManager.HP -= 50;
    }
}
