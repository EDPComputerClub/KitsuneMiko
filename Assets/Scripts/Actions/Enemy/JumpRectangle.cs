using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class JumpRectangle : Action
{
    private Rigidbody2D rbody;

    public float horizontalSpeed = 2;//水平移動の速度
    public float verticalSpeed = 1;//垂直移動の速度
    private Vector3 bossPosition;//ボスの座標

    //移動方向
    public enum MOVE_DIR
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

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //上移動
        if (moveDirection == MOVE_DIR.UP)
        {
            rbody.velocity = new Vector2(0, verticalSpeed);
            bossPosition = transform.position;

            if (bossPosition.y > 3.3f)
            {
                if (bossPosition.x < 0)
                {
                    moveDirection = MOVE_DIR.RIGHT;
                    transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    moveDirection = MOVE_DIR.LEFT;
                    transform.localScale = new Vector2(1, 1);

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
                transform.localScale = new Vector2(-1, 1);
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
                transform.localScale = new Vector2(1, 1);

            }

        }

        //下移動
        if (moveDirection == MOVE_DIR.DOWN)
        {
            rbody.velocity = new Vector2(0, -verticalSpeed);
            transform.localScale = new Vector2(1, 1);
            bossPosition = transform.position;
        }
    }

    public override bool IsDone()
    {
        return true;
    }

    public override void Act(Dictionary<string, object> args)
    {

    }

}
