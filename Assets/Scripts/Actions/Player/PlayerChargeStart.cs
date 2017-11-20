using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerChargeStart : Action
{
    public float chargingPreparationTime = 1f;
    public float chargingTime = 1f;
    public float animationDuration = 0.8f;
    public GameObject captureBox;

    // IsDone : true => when Charge ends
    [System.NonSerialized]
    public bool _IsDone = false;
    public override bool IsDone()
    {
        return _IsDone;
    }

    void Start()
    {
        captureBox.SetActive(false);
    }

    public override void Act(Dictionary<string, object> args)
    {
        _IsDone = false;
        isCharging = true;
        elapsedTime = 0f;
        gameObject.GetComponent<PlayerChargeStartCondition>().Deactivate();
        gameObject.GetComponent<Animator>().SetBool("isCharging", true);
    }

    void Update()
    {
        if (isCharging)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log("Charging -- " + elapsedTime + "s");
        }
    }

    float elapsedTime = 0f;
    public float ElapsedTime
    {
        get
        {
            return elapsedTime;
        }
    }
    bool isCharging = false;
    public bool IsCharging
    {
        get
        {
            return isCharging;
        }
        set
        {
            isCharging = value;
        }
    }
}
