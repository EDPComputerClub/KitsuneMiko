using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentScript : MonoBehaviour
{

    public List<ExperimentDataClass> data;
    // Use this for initialization
    void Start()
    {
        foreach (ExperimentDataClass datum in data)
        {
            Debug.Log(datum.ListCountCheck());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public class ExperimentDataClass
{
    [System.Serializable]
    public class SubDataClass
    {
        public string testString;
        public int testInt;
    }
    protected List<string> testList = new List<string>();

    public int ListCountCheck()
    {
        return testList.Count;
    }
}
