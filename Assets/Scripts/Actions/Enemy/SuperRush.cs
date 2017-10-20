using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRush : Action {

    private Rigidbody2D rbody;
    public float moveSpeed = 4;//移動速度
    private int count;//ボスが静止したフレーム数 
    public int waitFrame = 120;//ボスが静止するフレーム数
    private Vector3 bossPosition;//ボスの位置座標

    //移動方向を定義
    public enum MOVE_DIR
    {
        LEFT,
        RIGHT,
    }

    //ボスの移動状態を定義
    public enum MOVE_STATUS
    {
        MOVING,
        WAITING,
    }

    private MOVE_DIR moveDirection = MOVE_DIR.LEFT;//移動方向
    private MOVE_STATUS moveStatus = MOVE_STATUS.WAITING;//移動状態

    public override bool IsDone()
    {
        return true;
    }

    public override void Act(Dictionary<string, object> args)
    {

    }

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        bossPosition = transform.position;
        count = 0;

        if (bossPosition.x > 0)
        {
            moveDirection = MOVE_DIR.LEFT;
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            moveDirection = MOVE_DIR.RIGHT;
            transform.localScale = new Vector2(-1, 1);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if(moveStatus == MOVE_STATUS.WAITING)
        {
            if (count < waitFrame)
            {
                count++;
            }

            if (count == waitFrame)
            {
                moveStatus = MOVE_STATUS.MOVING;
            }     
        }

        if(moveStatus == MOVE_STATUS.MOVING)
        {
            if (moveDirection == MOVE_DIR.RIGHT)
            {
                rbody.velocity = new Vector2(moveSpeed, 0);
                bossPosition = transform.position;

                if (bossPosition.x > 6.0f)
                {
                    moveStatus = MOVE_STATUS.WAITING;
                    rbody.velocity = new Vector2(0, 0);


                }

            }

            if (moveDirection == MOVE_DIR.LEFT)
            {
                rbody.velocity = new Vector2(-moveSpeed, 0);
                bossPosition = transform.position;

                if (bossPosition.x < -6.0f)
                {
                    moveStatus = MOVE_STATUS.WAITING;
                    rbody.velocity = new Vector2(0, 0);

                }

            }


        }

        

    }
}
