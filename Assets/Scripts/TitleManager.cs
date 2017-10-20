using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

    public GameObject selectedButton;

    private Button button;

	// Use this for initialization
	void Start () {
        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        button = selectedButton.GetComponent<Button>();

        button.Select();
    }
	
	// Update is called once per frame
	void Update () {

	}
    
}
