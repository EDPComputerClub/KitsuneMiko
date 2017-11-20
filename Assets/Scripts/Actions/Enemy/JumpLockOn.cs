using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLockOn : MonoBehaviour {

    public GameObject player;
    public LayerMask blockLayer;
    
    private enum ATTACK_STATE
    {
        STOP_INITIAL,
        JUMP_MOMENT,
        JUMP_FLOATING,
        STOP_FLOATING,
        STOP_AND_LOCKON,
        STOP_AND_LOCKON_OFF,
        ATTACK_AND_LOCKON_OFF,
        SET_POSITION,
        STOP_ON_GROUND,
        WALK_TO_START,

        NONE
    }

    private ATTACK_STATE attackState = ATTACK_STATE.NONE;
    private Rigidbody2D rbody;
    private bool timerStart=false;
    private Timer timer;
    private BoxCollider2D boxCol;
    private CircleCollider2D circleCol;
    private Vector2 iniLocalScale;
    private Vector2 iniPosition;
    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 2;
        timer = GetComponent<Timer>();
        iniLocalScale = transform.localScale;

        circleCol = GetComponent<CircleCollider2D>();
        boxCol = GetComponent<BoxCollider2D>();
        iniPosition = transform.position;
	}
	
	void FixedUpdate () {

        float distance;
        Vector2 directionVector;
        float speed;
        switch (attackState)
        {
            case ATTACK_STATE.STOP_INITIAL:

                rbody.gravityScale = 2;
                
                if (timer.ElapsedTime > 1f)
                {
                    attackState = ATTACK_STATE.JUMP_MOMENT;
                }
                break;

            case ATTACK_STATE.JUMP_MOMENT:
                rbody.AddForce(new Vector2(0, 400));
                attackState = ATTACK_STATE.JUMP_FLOATING;
                break;
            case ATTACK_STATE.JUMP_FLOATING:
                //時間経過処理
                if (rbody.velocity.y < 0)
                {
                    attackState = ATTACK_STATE.STOP_AND_LOCKON_OFF;
                    timer.Begin();
                }
                break;

            case ATTACK_STATE.STOP_AND_LOCKON_OFF:
                rbody.gravityScale = 0;
                rbody.velocity = new Vector2(0, 0);
                Debug.Log("stop_and_lockon_off");
                
                if (timer.ElapsedTime > 1f)
                {
                    attackState = ATTACK_STATE.STOP_AND_LOCKON;
                    timer.Begin();
                }
                
                break;
            case ATTACK_STATE.STOP_AND_LOCKON:
                Debug.Log("stop_and_lockon");
                if (timer.ElapsedTime > 1f)
                {
                    //距離取得はいらない子
                    distance = Vector2.Distance(player.transform.position, transform.position);
                    directionVector = player.transform.position - transform.position;
                    speed = 10f;
                    rbody.velocity = directionVector.normalized * speed;
                    attackState = ATTACK_STATE.ATTACK_AND_LOCKON_OFF;
                    timer.Begin();
                }
                break;

            case ATTACK_STATE.ATTACK_AND_LOCKON_OFF:
                Debug.Log("attack_and_lockon_off");
                //ラインキャスト処理

                circleCol.isTrigger = true;
                boxCol.isTrigger = true;
                if (timer.ElapsedTime>3)
                {
                    attackState = ATTACK_STATE.SET_POSITION;
                    
                    transform.position = new Vector2(iniPosition.x,iniPosition.y+5);
                    rbody.velocity = new Vector2(0, 0);
                }
                break;
            case ATTACK_STATE.SET_POSITION:
                Debug.Log("set_position");
                
                Debug.Log(transform.position);
                rbody.gravityScale = 2;
                
                circleCol.isTrigger = false;
                boxCol.isTrigger = false;
                bool isBlock =
                     Physics2D.Linecast(transform.position - (transform.right * 0.3f),
                        transform.position - (transform.up * 0.1f), blockLayer) ||
                     Physics2D.Linecast(transform.position + (transform.right * 0.3f),
                        transform.position - (transform.up * 0.1f), blockLayer);
                if (isBlock)
                {
                    timer.Begin();
                    attackState = ATTACK_STATE.STOP_ON_GROUND;
                }

                break;
            case ATTACK_STATE.STOP_ON_GROUND:
                Debug.Log("stop_on_ground");
                
                if (timer.ElapsedTime > 3f)
                {
                    attackState = ATTACK_STATE.WALK_TO_START;
                }
                
                break;
            case ATTACK_STATE.WALK_TO_START:
                attackState = ATTACK_STATE.NONE;
                break;

            case ATTACK_STATE.NONE:
                if (Input.GetKeyDown(KeyCode.Q))//イベント代わり
                {
                    
                    attackState = ATTACK_STATE.STOP_INITIAL;
                    if (player.transform.position.x < transform.position.x)
                    {
                        transform.localScale =
                        new Vector2(iniLocalScale.x, transform.localScale.y);
                    }
                    else if(player.transform.position.x>=transform.position.x)
                    {
                        Debug.Log("test");
                        transform.localScale = new Vector2(iniLocalScale.x * -1, transform.localScale.y);
                    }
                    
                    
                    
                    timer.Begin();
                }

                
                break;
                

        }
        
	}
    /*
    void OnTriggerEnter2D(Collider2D col)
    {

        if (attackState == ATTACK_STATE.ATTACK_AND_LOCKON_OFF)
        {
            if (col.gameObject.layer.ToString() == "Block")
            {
                circleCol.enabled = false;
                boxCol.enabled = false;
            }
        }
 
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (attackState == ATTACK_STATE.ATTACK_AND_LOCKON_OFF)
        {
            if (col.gameObject.layer.ToString() == "Block")
            {
                circleCol.enabled = true;
                boxCol.enabled = true;
            }
        }
    }

    */
}
