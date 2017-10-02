using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    GameObject presentWeapon;
    public GameObject purificationStick;

    public GameObject captureBox;

    public GameObject gameManager;

    public LayerMask blockLayer;//ブロックレイヤー

    private Rigidbody2D rbody;//プレイヤー制御用Rigidbody2D

    private const float MOVE_SPEED = 3;//移動速度固定値
    private float moveSpeed;//移動速度
    private Animator animator;  //アニメーター
    public enum MOVE_DIR
    {
        STOP,
        LEFT,
        RIGHT,
    };
    public AudioClip jumpSE;
    public AudioClip getSE;
    public AudioClip stampSE;

    private AudioSource audioSource;

    private GameObject touchedOrb;  // 最後に触ったオーブ 初期値は null

    // Timer that holds animation playtime
    private Timer animationProcessedTimer;
    // flag that enables player to input attack key
    private bool isAttackKeyReceived = true;
    // if next attack is registered or not
    private bool isNextAttackRegistered = false;
    // counts of finished animations
    private int finishedAttackNum = 0;
    // number of next attack
    private int nextAttackNum = 1;
    private enum AttackNumber : int
    {
        Idle = 0, First, Second, Third, Fourth, Reset
    }
    private int[] AttackAnimationDuration = new int[4] { 6, 6, 6, 6 };

    void Start()
    {
        audioSource = gameManager.GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animationProcessedTimer = gameObject.GetComponents<Timer>().First(x => x.timerName == "animation");
        presentWeapon = purificationStick;
        chargingTimer = gameObject.GetComponents<Timer>().First(x => x.timerName == "charging");
        captureBox.SetActive(false);
    }
    void Update()
    {
        //Charge(Input.GetKey(KeyCode.C));
    }
    
    enum ChargeStatus : int
    {
        Idle = 0,
        Preparing,
        Charging,
        Capturing
    }
    ChargeStatus chargeStatus = ChargeStatus.Idle;
    Timer chargingTimer;
    public float chargingPreparationTime = 1f;
    public float chargingTime = 1f;
    public float animationDuration = 0.8f;
    void Charge(bool isChargeButtonKeptPressed)
    {
        if (isChargeButtonKeptPressed && chargeStatus == ChargeStatus.Idle)
        {
            chargingTimer.Begin();
            chargeStatus = ChargeStatus.Preparing;
        }
        else if (chargeStatus == ChargeStatus.Preparing)
        {
            if (isChargeButtonKeptPressed && chargingTimer.ElapsedTime >= chargingPreparationTime)
            {
                chargingTimer.Begin(true);
                chargeStatus = ChargeStatus.Charging;
            }
            else if (!isChargeButtonKeptPressed && chargingTimer.ElapsedTime < chargingPreparationTime)
            {
                chargingTimer.Stop();
                chargeStatus = ChargeStatus.Idle;
            }
        }
        else if (chargeStatus == ChargeStatus.Charging)
        {
            Debug.Log("Charging -- " + chargingTimer.ElapsedTime + "s");
            if (!isChargeButtonKeptPressed && chargingTimer.ElapsedTime >= chargingTime)
            {
                Debug.Log("Attempt to capture");
                captureBox.SetActive(true);
                chargingTimer.Begin(true);
                chargeStatus = ChargeStatus.Capturing;

            }
            else if (!isChargeButtonKeptPressed && chargingTimer.ElapsedTime < chargingTime)
            {
                chargingTimer.Stop();
                chargeStatus = ChargeStatus.Idle;
            }
        }

        if (chargeStatus == ChargeStatus.Capturing && chargingTimer.ElapsedTime > animationDuration)
        {
            captureBox.SetActive(false);
            chargingTimer.Stop();
            chargeStatus = ChargeStatus.Idle;
        }
    }

    //衝突処理
    void OnTriggerEnter2D(Collider2D col)
    {

        if (gameManager.GetComponent<GameManager>().gameMode != GameManager.GAME_MODE.PLAY)
        {
            return;
        }
        if (col.gameObject.tag == "Trap")
        {
            gameManager.GetComponent<GameManager>().GameOver();
            DestroyPlayer();
        }
        if (col.gameObject.tag == "Goal")
        {
            gameManager.GetComponent<GameManager>().GameClear();
        }

        if (col.gameObject.tag == "Enemy")
        {
            if (transform.position.y > col.gameObject.transform.position.y + 0.4f)
            {
                audioSource.PlayOneShot(stampSE);
                rbody.velocity = new Vector2(rbody.velocity.x, 0);
                rbody.AddForce(Vector2.up * 100f);
                col.gameObject.GetComponent<EnemyManager>().DestroyEnemy();
            }
            else
            {
                gameManager.GetComponent<GameManager>().GameOver();
                DestroyPlayer();
            }
        }

        if (col.gameObject.tag == "Orb")
        {
            // スコア二重取得回避:もし最後に触ったOrbと今触ったOrbが重複してなかったら
            if (col.gameObject != touchedOrb)
            {
                touchedOrb = col.gameObject;
                audioSource.PlayOneShot(getSE);
                col.gameObject.GetComponent<OrbManager>().GetOrb();
            }
        }
    }

    //プレイヤーオブジェクト削除処理
    void DestroyPlayer()
    {
        gameManager.GetComponent<GameManager>().gameMode = GameManager.GAME_MODE.GAMEOVER;
        //コライダーを削除
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Destroy(circleCollider);
        Destroy(boxCollider);
        //死亡アニメーション
        Sequence animSet = DOTween.Sequence();
        animSet.Append(transform.DOLocalMoveY(1.0f, 0.2f).SetRelative());
        animSet.Append(transform.DOLocalMoveY(-10.0f, 1.0f).SetRelative());
        Destroy(this.gameObject, 1.2f);
    }

}
