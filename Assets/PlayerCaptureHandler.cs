using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCaptureHandler : MonoBehaviour {

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<CollisionDetector>().remotedMethods += HitMethod;
    }
	
    // Update is called once per frame
    void Update () {
        
    }

    void HitMethod(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            EnemyManager enemyManager = collider.GetComponent<EnemyManager>();
            string classifiedName = enemyManager.classifiedName.ToString();
            switch(classifiedName)
            {
                case "Turtle":
                    Debug.Log("Captured successfully. Type => " + classifiedName);
                    enemyManager.DestroyEnemy();
                break;
            }
        }
    }
}
