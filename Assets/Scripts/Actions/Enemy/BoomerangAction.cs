using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangAction : MonoBehaviour {

    public int gameObjectNumber = 0;

    public GameObject target;

    public float timeInterval;

    public GameObject boomerangPrefab;

    private int boomerangCount = 0;
    private int time = 0;
    private bool trigger = false;

    // Use this for initialization
    void Start()
    {



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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            trigger = true;
        }
        


    }

    void FixedUpdate()
    {
        if (trigger)
        {
            if (boomerangCount < gameObjectNumber)
            {
                if (time % timeInterval == 0)
                {
                    GameObject tmp = Instantiate(boomerangPrefab) as GameObject;
                    tmp.GetComponent<Boomerang>().Ini(transform, target.transform.position);
                    boomerangCount++;
                }
            }
            time++;
        }
        
    }

    public bool IsDone()
    {
        return ActionIsDone();

    }
    public void Act()
    {

    }

    public bool ActionIsDone()
    {

        return true;
    }
}
