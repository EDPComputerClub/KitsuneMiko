using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    private GameObject button;
    private GameObject pauseManager;
    private GameObject statePanel;
    private System.Type skillType1;
    private System.Type skillType2;
    private System.Type skillType3;

    // Use this for initialization
    void Start () {
        button = this.gameObject;
        pauseManager = GameObject.Find("PauseManager");
        statePanel = GameObject.Find("StatePanel");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonPush()
    {
        //ゲームスタート
        if(button.name == "StartButton")
        {
            SceneManager.LoadScene("GameScene1");
        }

        //ゲーム終了
        if(button.name == "EndButton")
        {
            Application.Quit();
        }

        //スキルセット
        if(button.name == "SkillSet")
        {
            pauseManager.GetComponent<PauseManager>().ToSkillSet1();
        }

        //タイトルへ戻る(ダイアログ表示)
        if(button.name == "ToTitle")
        {
            pauseManager.GetComponent<PauseManager>().ToDialog();
        }

        //ダイアログ
        //Yes
        if(button.name == "YesButton")
        {
            SceneManager.LoadScene("GameTitle");
        }
        //No
        if(button.name == "NoButton")
        {
            pauseManager.GetComponent<PauseManager>().ToPause();
        }

        //ゲームに戻る
        if(button.name == "ReturnGame")
        {
            pauseManager.GetComponent<PauseManager>().ExitPause();
        }

        //スキルスロット1の選択
        if(button.name == "Slot1")
        {
            pauseManager.GetComponent<PauseManager>().ToSkillSelect1();
        }

        //スキルスロット2の選択
        if (button.name == "Slot2")
        {
            pauseManager.GetComponent<PauseManager>().ToSkillSelect2();
        }

        //スキルスロット3の選択
        if (button.name == "Slot3")
        {
            pauseManager.GetComponent<PauseManager>().ToSkillSelect3();
        }

        //スキルを取得しているかどうかで処理を分ける必要あり
        //スキル1A選択時の処理
        if(button.name == "Skill1A")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(1, "Skill1A");
            skillType1 = TypeOfSkill("Skill1A");
        }

        //スキル1B選択時の処理
        if (button.name == "Skill1B")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(1, "Skill1B");
            skillType1 = TypeOfSkill("Skill1B");
        }
        
        //スキル1C選択時の処理
        if (button.name == "Skill1C")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(1, "Skill1C");
            skillType1 = TypeOfSkill("Skill1C");
        }

        //スキル1D選択時の処理
        if (button.name == "Skill1D")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(1, "Skill1D");
            skillType1 = TypeOfSkill("Skill1D");
        }

        //スキル2A選択時の処理
        if (button.name == "Skill2A")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(2, "Skill2A");
            skillType2 = TypeOfSkill("Skill2A");
        }

        //スキル2B選択時の処理
        if (button.name == "Skill2B")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(2, "Skill2B");
            skillType2 = TypeOfSkill("Skill2B");
        }

        //スキル2C選択時の処理
        if (button.name == "Skill2C")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(2, "Skill2C");
            skillType2 = TypeOfSkill("Skill2C");
        }

        //スキル2D選択時の処理
        if (button.name == "Skill2D")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(2, "Skill2D");
            skillType2 = TypeOfSkill("Skill2D");
        }
        
        //スキル3A選択時の処理
        if (button.name == "Skill3A")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(3, "Skill3A");
            skillType3 = TypeOfSkill("Skill3A");
        }

        //スキル3B選択時の処理
        if (button.name == "Skill3B")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(3, "Skill3B");
            skillType3 = TypeOfSkill("Skill3B");
        }

        //スキル3C選択時の処理
        if (button.name == "Skill3C")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(3, "Skill3C");
            skillType3 = TypeOfSkill("Skill3C");
        }

        //スキル3D選択時の処理
        if (button.name == "Skill3D")
        {
            statePanel.GetComponent<UIManager>().SetSkillImage(3, "Skill3D");
            skillType3 = TypeOfSkill("Skill3D");
        }

    }

    public System.Type TypeOfSkill(string SkillName)
    {
        System.Type targetType = System.Type.GetType(SkillName);
        return targetType;
    }
}
