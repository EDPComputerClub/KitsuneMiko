using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerChargeStart : Action
{

    enum ChargeStatus : int
    {
        Idle = 0,
        Preparing,
        Charging,
        Capturing
    }
    ChargeStatus chargeStatus = ChargeStatus.Idle;
    Timer chargingTimer;
    public float chargingPreparationTime = 1f;
    public float chargingTime = 1f;
    public float animationDuration = 0.8f;
    public GameObject captureBox;

    public override bool IsDone()
    {
        return false;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        chargingTimer = gameObject.GetComponents<Timer>().First(x => x.timerName == "charging");
        captureBox.SetActive(false);
    }

    public override void Act(Dictionary<string, object> args)
    {
        // TODO: Chargeメソッドの引数をGetKeyとしているのでActionManagerには不適合. GetKeyDown及びGetKeyUpを使うべき
        isCharging = true;
        elapsedTime = 0f;

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
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
