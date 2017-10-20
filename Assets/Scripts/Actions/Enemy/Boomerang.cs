using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour {
    
    private const int OVER_RUN = 3;
    private Rigidbody2D rbody;
    private bool returnToThrower=false;
    private bool returnAtThrower = false;
        
    private Vector2 targetPosition;
    private enum SCALE_FLIP
    {
        BEFORE_FLIP,
        FLIPPING,
        FLIPPED
    }
    private enum THROW_DIRECTION
    {
        RIGHT,
        LEFT,
        
        NONE      
    }
    private SCALE_FLIP scaleFlip = SCALE_FLIP.BEFORE_FLIP;
    private THROW_DIRECTION throwDirection = THROW_DIRECTION.NONE;
    private Vector2 directionVector;
    private Vector2 returnAtVector;
    private Vector2 iniVector;
    private int direction;
    public float boomerangSpeed;
    public float offset_y;

    public void Ini(Transform transform,Vector2 targetPosition)
    {
        iniVector = transform.position;
        iniVector.y += offset_y;
        this.transform.position = new Vector2(transform.position.x,transform.position.y+offset_y);

        this.targetPosition= targetPosition;
        if (transform.localScale.x<0)
        {
            throwDirection = THROW_DIRECTION.LEFT;
            returnAtVector = new Vector2(targetPosition.x - OVER_RUN, targetPosition.y);
        }
        else
        {
            throwDirection = THROW_DIRECTION.RIGHT;
            returnAtVector = new Vector2(targetPosition.x + OVER_RUN,targetPosition.y);
        }
    }

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        directionVector = returnAtVector - (Vector2)transform.position;

        
        switch (throwDirection)
        {
            case THROW_DIRECTION.RIGHT:
                if (directionVector.x>0)
                {
                    direction = 1;
                }
                else
                {
                    returnToThrower = true;
                }

                if (returnToThrower)
                {
                    direction = -1;
                    switch (scaleFlip)
                    {
                        case SCALE_FLIP.BEFORE_FLIP:
                            scaleFlip = SCALE_FLIP.FLIPPING;
                            break;
                        case SCALE_FLIP.FLIPPING:
                            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                            scaleFlip = SCALE_FLIP.FLIPPED;
                            break;
                        case SCALE_FLIP.FLIPPED:
                            break;
                    }
                    if (iniVector.x > transform.position.x)
                    {
                        returnAtThrower = true;
                    }
                }
               
                break;
            case THROW_DIRECTION.LEFT:
                if (directionVector.x < 0)
                {
                    direction = -1;

                }else
                {
                    returnToThrower = true;
                    
                }

                if(returnToThrower)
                {
                    switch (scaleFlip)
                    {
                        case SCALE_FLIP.BEFORE_FLIP:
                            scaleFlip = SCALE_FLIP.FLIPPING;
                            break;
                        case SCALE_FLIP.FLIPPING:
                            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                            scaleFlip = SCALE_FLIP.FLIPPED;
                            break;
                        case SCALE_FLIP.FLIPPED:
                            //何もしない
                            break;
                    }
                    direction = 1;
                    if (iniVector.x < transform.position.x)
                    {
                        returnAtThrower = true;
                    }
                }
                break;
        }

        if (returnAtThrower)
        {
            Destroy(this.gameObject);
        }
        rbody.velocity =new Vector2(direction * boomerangSpeed,0);
        Debug.Log(rbody.velocity);

	}

   
}
