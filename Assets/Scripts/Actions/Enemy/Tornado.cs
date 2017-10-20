using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado:MonoBehaviour{

    
    public LayerMask blockLayer;
    public float moveSpeed = 1;

    private Rigidbody2D rbody;
    private const int TORNADO_NUM_MAX = 10;
    private const float OFFSET_Y = 0.5f;//たつ巻が出るy座標のオフセット
    private enum DIRECTION
    {
        RIGHT = 1,
        LEFT = -1
    }

    private DIRECTION direction = DIRECTION.RIGHT;
    //オブジェクト生成時の初期化処理
    public void Initialize(Transform transform,Transform prefab)
    {
        this.transform.position = transform.position;

        this.transform.localScale = prefab.transform.localScale;
        rbody = GetComponent<Rigidbody2D>();

        if (transform.localScale.x < 0)
        {
            direction = DIRECTION.RIGHT;

        }
        else
        {
            direction = DIRECTION.LEFT;

        }
        rbody.velocity = new Vector2((float)direction * moveSpeed, 0);
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y+OFFSET_Y);

    }


    void FixedUpdate() 
    {

        //消える計算
        bool isBlock;

        switch (direction)
        {
            case DIRECTION.LEFT:
                rbody.velocity = new Vector2(moveSpeed * -1, rbody.velocity.y);
                
                isBlock = Physics2D.Linecast(
                    new Vector2(transform.position.x, transform.position.y),
                    new Vector2(transform.position.x - 0.3f, transform.position.y),
                    blockLayer);

                if (isBlock)
                {
                    direction = DIRECTION.RIGHT;
                }
                break;
            case DIRECTION.RIGHT:
                rbody.velocity = new Vector2(moveSpeed, rbody.velocity.y);
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

                isBlock = Physics2D.Linecast(
                    new Vector2(transform.position.x, transform.position.y ),
                    new Vector2(transform.position.x + 0.3f, transform.position.y ),
                    blockLayer);

                if (isBlock)
                {
                    DisappearTornado();
                }
                break;
        }

    }

    void DisappearTornado()
    {
        Destroy(this.gameObject);
    }
}
