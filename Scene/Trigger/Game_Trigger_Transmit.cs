using UnityEngine;
using System.Collections;

public class Game_Trigger_Transmit : Controller {

	public bool mIsActive = false;
	public float mWaitTime = 0.2f;
	public string mTargetTransmitID;

	private bool mIsEnter = false;
	private float mWaitTimer;
	void Start()
	{
		if (!mIsActive)
			transform.GetChild (0).gameObject.SetActive (false);
		else
			transform.GetChild (0).gameObject.SetActive (false);
	}

	void OnTriggerEnter(Collider Col)
	{
		mIsEnter = true;
		mWaitTimer = 0f;
	}

	void Update()
	{
		if (!mIsEnter)
			return;

		mWaitTimer += Time.deltaTime;
		if (mWaitTimer > mWaitTime) 
		{
			mIsEnter = false;
			//Struct_Scene_Init TmpInit = new Struct_Scene_Init ();
			//TmpInit.TargetPassPointID = mTargetTransmitID;
			//this.SendEvent (GameEvent.FightingEvent.EVENT_FIGHT_INIT_LEVEL,TmpInit);	
		}
	}
}
