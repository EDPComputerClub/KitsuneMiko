using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkManager : MonoBehaviour {

    private Text dispText;

    private TextAsset talkTextAsset;
    private string[] talkText;

    private int talkNum;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
        dispText = GameObject.Find("Canvas/TextBack/TalkText").GetComponent<Text>();

        talkTextAsset = Resources.Load("TestText") as TextAsset;
        talkText = talkTextAsset.text.Split(char.Parse("\n"));

        dispText.text = talkText[0];
        talkNum = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (talkNum < talkText.Length)
            {
                dispText.text = talkText[talkNum];
                talkNum++;
            }
            else
            {
                Time.timeScale = 1;
                SceneManager.UnloadSceneAsync("TalkScene");
            }
        }

	}

}
