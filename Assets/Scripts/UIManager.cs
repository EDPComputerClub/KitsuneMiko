using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    
    GameObject player;

    private float MAX_HP = 1000;
    private float MAX_MP = 1000;
    
    private Sprite skill1A;
    private Sprite skill1B;
    private Sprite skill1C;
    private Sprite skill1D;
    private Sprite skill2A;
    private Sprite skill2B;
    private Sprite skill2C;
    private Sprite skill2D;
    private Sprite skill3A;
    private Sprite skill3B;
    private Sprite skill3C;
    private Sprite skill3D;

    private Sprite bagSprite;

    private GameObject state;
    private GameObject pauseManager;
    private Slider hpBar;
    private Slider mpBar;
    private Slider hpDamageBar;
    //private Slider mpDamageBar;
    private Image skill1;
    private Image skill2;
    private Image skill3;
    private Image bag;
    private Text hpText;
    private Text mpText;

    private float maxHP;
    private float maxMP;
    private float hp;
    private float mp;

    private int num = 0;
    private int fnum;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectsWithTag("Player").First(obj => obj.GetComponent<PlayerIdentity>() != null);

        skill1A = Resources.Load<Sprite>("Images/block");
        skill1B = Resources.Load<Sprite>("Images/bonz_run1");
        skill1C = Resources.Load<Sprite>("Images/bonz_jump");
        skill1D = Resources.Load<Sprite>("Images/bonz_run");
        skill2A = Resources.Load<Sprite>("Images/block");
        skill2B = Resources.Load<Sprite>("Images/bonz_run1");
        skill2C = Resources.Load<Sprite>("Images/bonz_jump");
        skill2D = Resources.Load<Sprite>("Images/bonz_run");
        skill3A = Resources.Load<Sprite>("Images/block");
        skill3B = Resources.Load<Sprite>("Images/bonz_run1");
        skill3C = Resources.Load<Sprite>("Images/bonz_jump");
        skill3D = Resources.Load<Sprite>("Images/bonz_run");
        bagSprite = Resources.Load<Sprite>("Images/block");

        state = this.gameObject;
        pauseManager = GameObject.Find("PauseManager");

        hpBar = state.transform.Find("HPBar").GetComponent<Slider>();
        mpBar = state.transform.Find("MPBar").GetComponent<Slider>();
        hpDamageBar = hpBar.transform.Find("Background/DamageBar").GetComponent<Slider>();
        //mpDamageBar = mpBar.transform.Find("Background/DamageBar").GetComponent<Slider>();
        skill1 = state.transform.Find("Skill1").GetComponent<Image>();
        skill2 = state.transform.Find("Skill2").GetComponent<Image>();
        skill3 = state.transform.Find("Skill3").GetComponent<Image>();
        bag = state.transform.Find("Bag").GetComponent<Image>();
        hpText = hpBar.transform.Find("HPText").GetComponent<Text>();
        mpText = mpBar.transform.Find("MPText").GetComponent<Text>();

        hpBar.maxValue = MAX_HP;
        mpBar.maxValue = MAX_MP;

        hpBar.value = MAX_HP;
        mpBar.value = MAX_MP;

        hp = hpBar.value;
        mp = mpBar.value;

        hpDamageBar.maxValue = MAX_HP;
        hpDamageBar.minValue = 0;
        hpDamageBar.value = MAX_HP;

        //mpDamageBar.maxValue = MAX_MP;
        //mpDamageBar.minValue = 0;
        //mpDamageBar.value = MAX_MP;

        hpText.text = hpBar.value + "/" + MAX_HP;
        mpText.text = mpBar.value + "/" + MAX_MP;

        //MPを1減らすフレーム数を設定
        fnum = 10;

        //鞄スプライト設定
        bag.sprite = bagSprite;
    }

    
    // Update is called once per frame
    void Update() {

        //ポーズ時にrigidbody以外の変化を止める方法
        //ここでは強引にifで囲っている
        if (pauseManager.GetComponent<PauseManager>().isPause == false)
        {
            //test
            if (Input.GetKeyDown(KeyCode.K))
            {
                SetHP(80);
                DecreaseMP(MAX_MP);
                if (hp > MAX_HP) hp = MAX_HP;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                //SetBagImage("item1");
                SetHP(-10);
                if (hp < 0) hp = 0;
            }
            //test end

            //hp管理
            if (hp < 0) hp = 0;

            if (hpBar.value != hp)
            {
                if (hpBar.value > hp)
                {
                    hpBar.value = hp;
                }
                else
                {
                    hpBar.value = hp;
                }
                hpText.text = hpBar.value + "/" + MAX_HP;
            }

            //hpダメージバー管理
            if (hpDamageBar.value > hp)
            {
                hpDamageBar.value -= (MAX_HP / 200f);
            }

            //mp管理
            if (mpBar.value != mp)
            {
                if (mpBar.value > mp)
                {
                    if (num >= fnum)
                    {
                        mpBar.value -= 1;
                        num = 0;
                    }
                }
                else
                {
                    mpBar.value += 1;
                }
                mpText.text = mpBar.value + "/" + MAX_MP;
            }
            
            num++;
        }
	}
    

    //スキルスロット(1~3)の指定:skillNum
    //スキル名:skillName
    public void SetSkillImage(int skillNum, string skillName)
    {
        switch (skillNum)
        {
            case 1:
                //スプライト名を取得するためspriteが空だとエラー
                //すでにスプライトが設定されている場合再設定しない(必要?)
                if (skill1.sprite.name != skillName)
                {
                    switch (skillName)
                    {
                        case "Skill1A":
                            skill1.sprite = skill1A;
                            break;
                        case "Skill1B":
                            skill1.sprite = skill1B;
                            break;
                        case "Skill1C":
                            skill1.sprite = skill1C;
                            break;
                        case "Skill1D":
                            skill1.sprite = skill1D;
                            break;
                    }
                }
                break;
            case 2:
                if (skill2.sprite.name != skillName)
                {
                    switch (skillName)
                    {
                        case "Skill2A":
                            skill2.sprite = skill2A;
                            break;
                        case "Skill2B":
                            skill2.sprite = skill2B;
                            break;
                        case "Skill2C":
                            skill2.sprite = skill2C;
                            break;
                        case "Skill2D":
                            skill2.sprite = skill2D;
                            break;
                    }
                }
                break;
            case 3:
                if (skill3.sprite.name != skillName)
                {
                    switch (skillName)
                    {
                        case "Skill3A":
                            skill3.sprite = skill3A;
                            break;
                        case "Skill3B":
                            skill3.sprite = skill3B;
                            break;
                        case "Skill3C":
                            skill3.sprite = skill3C;
                            break;
                        case "Skill3D":
                            skill3.sprite = skill3D;
                            break;
                    }
                }
                break;
        }
    }

    /*
    public void SetBagImage(string Name)
    {
        if(bag.sprite.name != Name)
        {
            if(Name == "bagSprite")
            {
                bag.sprite = bagSprite;
            }
        }
    }
    */

    private void DecreaseHP(float val)
    {
        hp -= val;
    }
    
    private void IncreaseHP(float val)
    {
        hpDamageBar.value += val;
        hp += val;
    }
    
    //基本的には全MPを消費
    //減らす速度を遅くすることになる?
    private void DecreaseMP(float val)
    {
        mp -= val;
    }

    private void IncreaseMP(float val)
    {
        //mpDamageBar.value += val;
        mp += val;
    }

    //他スクリプトから参照
    public void SetHP(float val)
    {
        if(hp < val)
        {
            hpDamageBar.value = val;
        }
        hp = val;
    }

    //スロットに設定しているスプライトを返す
    //num : 1~3
    public Sprite SpriteOfSlot(int num)
    {
        switch (num)
        {
            case 1:
                return skill1.sprite;
            case 2:
                return skill2.sprite;
            case 3:
                return skill3.sprite;
            default:
                return null;
        }
    }

    void SetMP(float val)
    {
        this.mp = val;
    }

    void FixedUpdate()
    {
        SetHP(player.GetComponent<Breakable>().hitPoint);
    }
}
