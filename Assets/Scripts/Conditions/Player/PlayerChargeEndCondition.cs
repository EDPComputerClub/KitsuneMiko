using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeEndCondition : Condition
{
    PlayerChargeStart playerChargeStart;
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
        playerChargeStart = gameObject.GetComponent<PlayerChargeStart>();
    }

    public void Deactivate()
    {
        Status.isSatisfied = false;
    }

    public float chargingTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C) && playerChargeStart.IsCharging)
        {
            // Chargingをしていて、経過時間がchargeTimeを超えていてチャージボタンを離すときにとActを呼び出す
            if (playerChargeStart.ElapsedTime > chargingTime)
            {
                playerChargeStart.IsCharging = false;
                Status.isSatisfied = true;
            }
            // Charge中にー時ボタンを離されたとき初期化する
            else
            {
                Debug.Log("Initialized");
                playerChargeStart.IsCharging = false;
            }
            gameObject.GetComponent<PlayerChargeStart>()._IsDone = true;
        }
    }
}
