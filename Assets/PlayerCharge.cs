using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCharge : Action {

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
    }

	void Charge(bool isChargeButtonKeptPressed)
    {
        if (isChargeButtonKeptPressed && chargeStatus == ChargeStatus.Idle)
        {
            chargingTimer.Begin();
            chargeStatus = ChargeStatus.Preparing;
        }
        else if (chargeStatus == ChargeStatus.Preparing)
        {
            if (isChargeButtonKeptPressed && chargingTimer.ElapsedTime >= chargingPreparationTime)
            {
                chargingTimer.Begin(true);
                chargeStatus = ChargeStatus.Charging;
            }
            else if (!isChargeButtonKeptPressed && chargingTimer.ElapsedTime < chargingPreparationTime)
            {
                chargingTimer.Stop();
                chargeStatus = ChargeStatus.Idle;
            }
        }
        else if (chargeStatus == ChargeStatus.Charging)
        {
            Debug.Log("Charging -- " + chargingTimer.ElapsedTime + "s");
            if (!isChargeButtonKeptPressed && chargingTimer.ElapsedTime >= chargingTime)
            {
                Debug.Log("Attempt to capture");
                captureBox.SetActive(true);
                chargingTimer.Begin(true);
                chargeStatus = ChargeStatus.Capturing;

            }
            else if (!isChargeButtonKeptPressed && chargingTimer.ElapsedTime < chargingTime)
            {
                chargingTimer.Stop();
                chargeStatus = ChargeStatus.Idle;
            }
        }

        if (chargeStatus == ChargeStatus.Capturing && chargingTimer.ElapsedTime > animationDuration)
        {
            captureBox.SetActive(false);
            chargingTimer.Stop();
            chargeStatus = ChargeStatus.Idle;
        }
    }
}
