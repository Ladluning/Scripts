using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Trigger_Transmit : Controller {

	public bool mIsActive = false;
	public float mWaitTime = 0.2f;
	public string mTargetTransmitID;
	
	private bool  mIsEnter;
	private float mWaitTimer;

	void Start()
	{
		if (!mIsActive)
			transform.GetChild (0).gameObject.SetActive (false);
		else
			transform.GetChild (0).gameObject.SetActive (true);
	}

	void OnTriggerEnter(Collider Col)
	{
		if (Col.gameObject.tag != "MainCharacter")
			return;

		if (!Col.gameObject.GetComponent<Client_Transform> ().mIsTransmit)
			return;

		mIsEnter = true;
		mWaitTimer = 0f;

		Debug.Log ("Transmit");
	}

	void OnTriggerExit(Collider Col)
	{
		mIsEnter = false;
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

			Dictionary<string,object> tmpSend = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_SWITCH_SCENE);
			((Dictionary<string, object>)tmpSend["results"]).Add("id", Client_User.Singleton().GetID());
			((Dictionary<string, object>)tmpSend["results"]).Add("current", gameObject.name);
			((Dictionary<string, object>)tmpSend["results"]).Add("target", mTargetTransmitID);
			this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_SWITCH_SCENE,tmpSend);

			//Struct_Scene_Init TmpInit = new Struct_Scene_Init ();
			//TmpInit.TargetPassPointID = mTargetTransmitID;
			//this.SendEvent (GameEvent.FightingEvent.EVENT_FIGHT_INIT_LEVEL,TmpInit);	
		}
	}
}
