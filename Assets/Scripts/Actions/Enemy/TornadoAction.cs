using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoAction : MonoBehaviour {
    
    public int gameObjectNumber = 0;
    
    public GameObject prefab;

    public float timeInterval;
    [SerializeField]
    private GameObject prefabFactory;

    private int tornadoCount=0;
    
    // Use this for initialization
    void Start () {

        

        /*
        for (int i = 0; i < TORNADO_MAX_NUMBER; i++)
        {
            GameObject tmp = Instantiate(tornadoPrefab) as GameObject;
                  
            tmp.GetComponent<Tornado>().Initialize(transform,tornadoPrefab.transform);
            Debug.Log("Instantiate");
            yield return new WaitForSeconds(1.0f);
        }
        */
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject tmpFactory = Instantiate(prefabFactory) as GameObject;
            tmpFactory.GetComponent<GameObjectFactory>().
                Initialize(transform,prefab,gameObjectNumber,timeInterval,"Tornado");
        }
  



    }

    public  bool IsDone()
    {
        return ActionIsDone();
       
    }
    public  void Act()
    {

    }

    public bool ActionIsDone()
    {

        return true;
    }

}
