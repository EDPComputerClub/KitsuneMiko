using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class JumpRectangle : Action
{
    private Rigidbody2D rbody;

    private float horizontalSpeed = 2;//水平移動の速度
    private float verticalSpeed = 1;//垂直移動の速度
    

    private OnGround onGround;

    bool isDoing = false;


    //移動方向
    private enum MOVE_DIR
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }

    private MOVE_DIR moveDirection = MOVE_DIR.UP;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        onGround = GetComponent<OnGround>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!isDoing)
        {
            return;
        }

        Vector3 bossPosition = transform.position;//ボスの座標

        //上移動
        if (moveDirection == MOVE_DIR.UP)
        {
            rbody.velocity = new Vector2(0, verticalSpeed);

            if (bossPosition.y > 3.3f)
            {
                if (bossPosition.x < 0)
                {
                    moveDirection = MOVE_DIR.RIGHT;
                }
                else
                {
                    moveDirection = MOVE_DIR.LEFT;
                }
            }
        }

        //右移動
        if (moveDirection == MOVE_DIR.RIGHT)
        {
            rbody.velocity = new Vector2(horizontalSpeed, 0);
            bossPosition = transform.position;

            if (bossPosition.x > 6.0f)
            {
                moveDirection = MOVE_DIR.DOWN;
                transform.SetFaceDir(transform.GetFaceDir().Reverse());
            }

        }

        //左移動
        if (moveDirection == MOVE_DIR.LEFT)
        {
            rbody.velocity = new Vector2(-verticalSpeed, 0);
            bossPosition = transform.position;

            if (bossPosition.x < -6.0f)
            {
                moveDirection = MOVE_DIR.DOWN;
                transform.SetFaceDir(transform.GetFaceDir().Reverse());

            }

        }

        //下移動
        if (moveDirection == MOVE_DIR.DOWN)
        {
            rbody.velocity = new Vector2(0, -verticalSpeed);
            if(onGround.Check(new string[0]).isSatisfied)
            {
                Debug.Log("OnGrond.");
                rbody.isKinematic = false;
                rbody.velocity = Vector2.zero;
                moveDirection = MOVE_DIR.UP;
                isDoing = false;
            }
        }
    }

    public override bool IsDone()
    {
        return !isDoing;
    }

    public override void Act(Dictionary<string, object> args)
    {
        isDoing = true;
        rbody.isKinematic = true;
    }

}
