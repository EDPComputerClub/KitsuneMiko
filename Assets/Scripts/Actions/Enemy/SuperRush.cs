using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRush : Action {

    private Rigidbody2D rbody;
    private float moveSpeed = 4;//移動速度
    private int count;//ボスが静止したフレーム数 
    private int waitFrame = 100;//ボスが静止するフレーム数
    private Vector3 bossPosition;//ボスの位置座標


    bool isDoing = false;


    //移動方向を定義
    private enum MOVE_DIR
    {
        LEFT,
        RIGHT,
    }

    //ボスの移動状態を定義
    private enum MOVE_STATUS
    {
        MOVING,
        WAITING,
    }

    private MOVE_DIR moveDirection = MOVE_DIR.LEFT;//移動方向
    private MOVE_STATUS moveStatus = MOVE_STATUS.WAITING;//移動状態

    public override void Act(Dictionary<string, object> args)
    { 
        isDoing = true;

        bossPosition = transform.position;
        count = 0;

        if (bossPosition.x > 0)
        {
            moveDirection = MOVE_DIR.LEFT;
        }
        else
        {
            moveDirection = MOVE_DIR.RIGHT;
        }
    }

    public override bool IsDone()
    {
        return !isDoing;
    }

    

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
       
    }

    void FixedUpdate()
    {

        if (!isDoing)
        {
            return;
        }

        if (moveStatus == MOVE_STATUS.WAITING)
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
                    isDoing = false;


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
                    transform.SetFaceDir(transform.GetFaceDir().Reverse());
                    isDoing = false;
                }

            }


        }

        

    }
}
