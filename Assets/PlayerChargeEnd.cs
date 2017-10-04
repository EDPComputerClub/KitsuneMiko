using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeEnd : Action {

    public GameObject captureBox;
	public override bool IsDone()
	{
        return false;
    }

	public override void Act(Dictionary<string, object> args)
	{
        Debug.Log("Attempt to capture");
        captureBox.SetActive(true);
		elapsedTime = 0f;
        isCapturing = true;
    }

	// TODO: Animationのeventで実装してcapturingTimeは消すつもり
    public float capturingTime = 0.8f;
    float elapsedTime = 0f;
    bool isCapturing = false;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
	{
		if (isCapturing)
		{
            elapsedTime += Time.deltaTime;
        }

		if (isCapturing && elapsedTime > capturingTime)
		{
			captureBox.SetActive(false);
		}
	}

}
