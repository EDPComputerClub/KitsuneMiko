using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameObjectFactory : MonoBehaviour {
    [SerializeField]
    public int gameObjectNumber=0;
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public float timeInterval;
    [System.NonSerialized]
    public GameObject temporary;


    private System.Type type;
    private int objectCount=0; 
    // Use this for initialization
    IEnumerator Start()
    {
        for (int i = 0; i < gameObjectNumber; i++)
        {
            temporary=Instantiate(prefab) as GameObject;
            
            Component comp = temporary.GetComponent(type) as Component;

            //ここに使いたい生産するタイプ別gameObjectの初期化を追加
            if (comp is Tornado)
            {
                Tornado tornado = (Tornado)comp;
                tornado.Initialize(transform,prefab.transform);

            }
            //ここまで
            objectCount++;
            yield return new WaitForSeconds(timeInterval);
        }
    }
    public void Initialize(
        Transform transform,
        GameObject prefab,
        int gameObjectNumber,
        float timeInterval,
        string typeName
        )
    {
        this.transform.localScale = transform.localScale;
        this.transform.position = transform.position;
        this.gameObjectNumber = gameObjectNumber;
        this.prefab = prefab;
        this.timeInterval = timeInterval;
        this.type = StringToType(typeName);

    }
    // Update is called once per frame
    void Update () {

        if(objectCount > gameObjectNumber)
        {
            Destroy(this.gameObject);
        }
	}

    private System.Type StringToType(string typeAsString)
    {
        System.Type typeAsType = System.Type.GetType(typeAsString);
        return typeAsType;
    }

    
}
