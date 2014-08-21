using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_FSM_NPC_Base : Controller 
{
	public string mNPCID;

	public bool mIsFadeCamera;
	public Vector3 mFadePos;
	public Vector3 mFadeRotate;
	protected Camera mFadeCamera;
	protected Transform mUI;
	 
	protected virtual void OnEnable()
	{
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_NPC,OnHandleNPCUpdate);
	}

	protected virtual void OnDisable()
	{
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_NPC, OnHandleNPCUpdate);
	}

	protected virtual void Awake()
	{
		foreach (Camera tmp in Camera.allCameras) 
		{
			if(tmp.tag == "NPC")
			{
				mFadeCamera = tmp;
				return;
			}
		}

		mUI = transform.FindChild ("UI");
	}

	protected virtual object OnHandleNPCUpdate(object pSender)
	{
        JsonData tmpJson = new JsonData(pSender);
        if ((string)tmpJson["results"]["id"] != mNPCID)
            return null;

		return null;
	}

    public virtual void ActiveNPC()
    {
        Dictionary<string, object> tmpSend = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_REQUEST_NPC_DATA);
        ((Dictionary<string, object>)tmpSend["results"]).Add("id", mNPCID);
        this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_REQUEST_NPC_DATA, tmpSend);

		if (mIsFadeCamera) 
		{
			this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_STOP_MAINCHARACTER,null);	
			Game_Camera_Fade_Manager.Singleton().CrossFadeCamera(Camera.main,mFadeCamera);

			if(mUI!=null)
				mUI.gameObject.SetActive(true);
		}
    }

	public virtual void ExitNPC()
	{
		if (mIsFadeCamera) 
		{
			this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_RESUME_MAINCHARACTER,null);	
			Game_Camera_Fade_Manager.Singleton().CrossFadeCamera(mFadeCamera,Camera.main);
			
			if(mUI!=null)
				mUI.gameObject.SetActive(false);
		}
	}
}
