using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    class Data
    {
        public float hitPoint, defencePoint, magicPoint, naturalRecovery;
        public Dictionary<string, bool> skillDict;
        public bool isActive;
        public System.Type[] skills;
    }

    //定数定義
    private const int MAX_SCORE = 999999;

    public int nextStageNum;//クリア後に移るステージナンバー

    public LayerMask playerLayer;


    private int score = 0;
    private int displayScore = 0;

    public AudioClip clearSE;
    public AudioClip gameoverSE;
    private AudioSource audioSource;
    // Use this for initialization

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (score > displayScore)
        {
            displayScore += 10;

            if (displayScore > score)
            {
                displayScore = score;
            }
        }

        if (isSceneLoading)
        {
            //次のシーンが呼ばれ、Playerの存在を確認したら終了
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == "Stage" + nextStageNum)
            {
                GameObject newPlayer = Fetch("Player", playerLayer);
                if (newPlayer != null)
                {
                    isSceneLoading = false;
                    Initialize();
                }
            }
        }
    }

    // when new scene loaded, this method is called in order to set up inital settings
    void Initialize()
    {
        if (oldData != null)
        {

            GameObject newPlayer = Fetch("Player", playerLayer);
            Breakable newBreakable = newPlayer.GetComponent<Breakable>();
            SkillManager newSkillManager = newPlayer.GetComponent<SkillManager>();
            newBreakable.hitPoint = oldData.hitPoint;
            newBreakable.defencePoint = oldData.defencePoint;
            newSkillManager.magicPoint = oldData.magicPoint;
            newSkillManager.naturalRecovery = oldData.naturalRecovery;
            newSkillManager.skillDict = oldData.skillDict;
            newSkillManager.ReplaceSkills(oldData.skills);
            if (oldData.isActive)
            {
                newSkillManager.Activate();
            }
        }
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(gameoverSE);

        Invoke("GoBackGameTitle", 2.0f);
    }

    // シーン遷移
    bool isSceneLoading = false;
    Data oldData;
    public void Next()
    {
        audioSource.PlayOneShot(clearSE);
        isSceneLoading = true;

        GameObject oldPlayer = Fetch("Player", playerLayer);
        Breakable oldBreakable = oldPlayer.GetComponent<Breakable>();
        SkillManager oldSkillManager = oldPlayer.GetComponent<SkillManager>();
        oldData = new Data();
        oldData.hitPoint = oldBreakable.hitPoint;
        oldData.defencePoint = oldBreakable.defencePoint;
        oldData.magicPoint = oldSkillManager.magicPoint;
        oldData.naturalRecovery = oldSkillManager.naturalRecovery;
        oldData.skillDict = oldSkillManager.skillDict;
        oldData.isActive = oldSkillManager.isActive;
        oldData.skills = oldSkillManager.GetSetSkillTypes();

        SceneManager.LoadScene("Stage" + nextStageNum);
    }

    GameObject Fetch(string tag, LayerMask layer)
    {
        foreach (GameObject temp_object in GameObject.FindGameObjectsWithTag(tag))
        {
            if (temp_object.layer == System.Convert.ToInt32(System.Convert.ToString(layer.value, 2).Length) - 1)
            {
                return temp_object as GameObject;
            }
        }
        return null;
    }

    //スコア加算
    public void AddScore(int val)
    {
        score += val;
        if (score > MAX_SCORE)
        {
            score = MAX_SCORE;
        }
    }

    //タイトル画面に戻る
    void GoBackGameTitle()
    {
        SceneManager.LoadScene("GameTitle");
    }
}
