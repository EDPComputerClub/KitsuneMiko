using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour {

    public GameObject Black;
    private Rigidbody2D rbody;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.tag == "Player")
        {
            Black.SetActive(true);
            rbody.velocity = new Vector2(30, 0);
            
        }
        if(collision.gameObject.tag=="Finish")
        {
            rbody.velocity = new Vector2(0, 0);
            //GameObject.Find("GameManager").GetComponent<GameManager>().Next();
            //上の//を削除してください
        }
    }

    // Use this for initialization
    void Start () {
        rbody = Black.GetComponent<Rigidbody2D>();
        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
