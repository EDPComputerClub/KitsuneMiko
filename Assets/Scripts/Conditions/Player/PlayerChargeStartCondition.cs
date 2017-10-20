using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeStartCondition : Condition
{
    ConditionState Status;
    public override ConditionState Check(string[] args)
    {
        return Status;
    }

    // Use this for initialization
    void Start()
    {
        Status = new ConditionState();
        Status.isSatisfied = false;
    }

    public void Deactivate()
    {
        Status.isSatisfied = false;
    }

    bool isPreparing = false;
    float elapsedTime = 0f;
    public float preparationTime = 0.8f;
    // Update is called once per frame
    void Update()
    {
        if (isPreparing)
        {
            // ChargeのPreparation中ずっとキーを押しているなら経過時間を加算
            if (Input.GetKey(KeyCode.C))
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                isPreparing = false;
            }

            // ChargeのPreparationの指定時間が経過後Chargeを開始
            if (elapsedTime > preparationTime)
            {
                isPreparing = false;
                Status.isSatisfied = true;
            }
        }
        else
        {
            // ChargeのPreparation開始のTrigger
            if (Input.GetKeyDown(KeyCode.C))
            {
                isPreparing = true;
                elapsedTime = 0f;
            }
        }

    }
}
