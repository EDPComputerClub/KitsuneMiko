using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour {

    private GameObject uiManager;
    private GameObject pauseCanvas;
    private GameObject dialogPanel;
    private string selectedButton;
    private Text helpText;

    private Button setSkillButton;
    private Button returnButton;
    private Button slotButton1;
    private Button slotButton2;
    private Button slotButton3;
    private Button skillButton1;
    private Button skillButton2;
    private Button skillButton3;
    private Button noButton;

    private Image slot1;
    private Image slot2;
    private Image slot3;

    public bool isPause; //他クラスから参照出来たほうが良い?
    private bool selSlot;

    //どのスロットを選択しているか
    private bool selSlot1;
    private bool selSlot2;
    private bool selSlot3;

    //スキル選択中か
    private bool selSkill;

	// Use this for initialization
	void Start () {
        uiManager = GameObject.Find("StatePanel");
        pauseCanvas = GameObject.Find("PauseCanvas");
        dialogPanel = GameObject.Find("DialogPanel");
        helpText = pauseCanvas.transform.Find("PausePanel/HelpPanel/Text").GetComponent<Text>();
        setSkillButton = pauseCanvas.transform.Find("PausePanel/SkillSet").GetComponent<Button>();
        returnButton = pauseCanvas.transform.Find("PausePanel/ReturnGame").GetComponent<Button>();
        slotButton1 = pauseCanvas.transform.Find("PausePanel/Slot1").GetComponent<Button>();
        slotButton2 = pauseCanvas.transform.Find("PausePanel/Slot2").GetComponent<Button>();
        slotButton3 = pauseCanvas.transform.Find("PausePanel/Slot3").GetComponent<Button>();
        skillButton1 = pauseCanvas.transform.Find("PausePanel/Skill1A").GetComponent<Button>();
        skillButton2 = pauseCanvas.transform.Find("PausePanel/Skill2A").GetComponent<Button>();
        skillButton3 = pauseCanvas.transform.Find("PausePanel/Skill3A").GetComponent<Button>();
        slot1 = pauseCanvas.transform.Find("PausePanel/Slot1").GetComponent<Image>();
        slot2 = pauseCanvas.transform.Find("PausePanel/Slot2").GetComponent<Image>();
        slot3 = pauseCanvas.transform.Find("PausePanel/Slot3").GetComponent<Image>();
        noButton = dialogPanel.transform.Find("NoButton").GetComponent<Button>();

        selectedButton = "NULL";

        isPause = false;
        selSlot = false;
        
        selSlot1 = false;
        selSlot2 = false;
        selSlot3 = false;
        
        selSkill = false;

        pauseCanvas.SetActive(false);
        dialogPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (isPause)
        {
            //現在選択中のSelectableオブジェクトを取得(ここではボタン)
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                selectedButton = EventSystem.current.currentSelectedGameObject.name;
            }
        }

        //timescaleを0にするとrigidbodyなどは止まるがUpdate()は実行され続ける
        //スキル変更の際はポーズ中に変更される部分とされない部分で分ける?
        if (Input.GetKeyDown(KeyCode.P))
        {
            //ポーズ中
            if (isPause == true)
            {
                ExitPause();
            }
            else //ゲームプレイ中
            {
                ToPause();
            }
        }

        //キャンセルキー入力
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (selSlot) //スキルスロット選択中
            {
                ToPause();
                selSlot = false;
            }else if (selSkill) //スキル選択中
            {
                if (selSlot1)
                {
                    ToSkillSet1();
                    selSlot1 = false;
                }else if (selSlot2)
                {
                    ToSkillSet2();
                    selSlot2 = false;
                }else if (selSlot3)
                {
                    ToSkillSet3();
                    selSlot3 = false;
                }
            }
        }

        //選択されているボタンに応じた説明文の出力
        SetText(selectedButton);

        //ポーズ画面でのスキルスロット画像の変更
        slot1.sprite = uiManager.GetComponent<UIManager>().SpriteOfSlot(1);
        slot2.sprite = uiManager.GetComponent<UIManager>().SpriteOfSlot(2);
        slot3.sprite = uiManager.GetComponent<UIManager>().SpriteOfSlot(3);

    }

    //ポーズ画面移行処理
    public void ToPause()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        setSkillButton.Select();
        isPause = true;
        selSkill = false;
        selSlot = false;
        selSlot1 = false;
        selSlot2 = false;
        selSlot3 = false;
        dialogPanel.SetActive(false);
    }

    //ポーズ画面からの復帰処理
    public void ExitPause()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        returnButton.Select();
        isPause = false;
    }

    //スキルスロット1の選択へ
    public void ToSkillSet1()
    {
        slotButton1.Select();
        selSlot = true;
        selSkill = false;
    }

    //スキルスロット2の選択へ
    public void ToSkillSet2()
    {
        slotButton2.Select();
        selSlot = true;
        selSkill = false;
    }

    //スキルスロット3の選択へ
    public void ToSkillSet3()
    {
        slotButton3.Select();
        selSlot = true;
        selSkill = false;
    }

    //スキルスロット1のスキル選択へ
    public void ToSkillSelect1()
    {
        skillButton1.Select();
        selSlot = false;
        selSkill = true;
        selSlot1 = true;
    }

    //スキルスロット2のスキル選択へ
    public void ToSkillSelect2()
    {
        skillButton2.Select();
        selSlot = false;
        selSkill = true;
        selSlot2 = true;
    }

    //スキルスロット3のスキル選択へ
    public void ToSkillSelect3()
    {
        skillButton3.Select();
        selSlot = false;
        selSkill = true;
        selSlot3 = true;
    }

    //説明文の設定
    //スキルを取得していなければ別途ほかのテキストを表示
    public void SetText(string ObjectName)
    {
        switch (ObjectName)
        {
            case "SkillSet":
                helpText.text = "スキルの設定を行います";
                break;
            case "ToTitle":
                helpText.text = "タイトルに戻ります";
                break;
            case "ReturnGame":
                helpText.text = "ゲームに戻ります";
                break;
            case "Slot1":
                helpText.text = "1つ目のスキルスロットを設定します";
                break;
            case "Slot2":
                helpText.text = "2つ目のスキルスロットを設定します";
                break;
            case "Slot3":
                helpText.text = "3つ目のスキルスロットを設定します";
                break;
            case "Skill1A":
                helpText.text = "<スキル説明>";
                break;
            case "Skill1B":
                helpText.text = "<スキル説明>";
                break;
            case "Skill1C":
                helpText.text = "<スキル説明>";
                break;
            case "Skill1D":
                helpText.text = "<スキル説明>";
                break;
            case "Skill2A":
                helpText.text = "<スキル説明>";
                break;
            case "Skill2B":
                helpText.text = "<スキル説明>";
                break;
            case "Skill2C":
                helpText.text = "<スキル説明>";
                break;
            case "Skill2D":
                helpText.text = "<スキル説明>";
                break;
            case "Skill3A":
                helpText.text = "<スキル説明>";
                break;
            case "Skill3B":
                helpText.text = "<スキル説明>";
                break;
            case "Skill3C":
                helpText.text = "<スキル説明>";
                break;
            case "Skill3D":
                helpText.text = "<スキル説明>";
                break;
            default:
                helpText.text = "選択されているボタンがありません";
                break;
        }
    }

    public void ToDialog()
    {
        dialogPanel.SetActive(true);
        noButton.Select();
    }
}
