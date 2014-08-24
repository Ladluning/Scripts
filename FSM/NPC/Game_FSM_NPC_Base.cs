using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_FSM_NPC_Base : Controller 
{
	public string mNPCID;

	public bool mIsFadeCamera;
	protected Camera mFadeCamera;
	protected Transform mUI;
	 
	protected virtual void OnEnable()
	{
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_NPC,OnHandleNPCUpdate);
        this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_TRIGGER_ENTER_NPC, OnHandleActiveNPC);
	}

	protected virtual void OnDisable()
	{
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_NPC, OnHandleNPCUpdate);
        this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_TRIGGER_ENTER_NPC, OnHandleActiveNPC);
	}

	protected virtual void Awake()
	{
		mFadeCamera = Game_Camera_Fade_Manager.Singleton ().GetCameraWithType (E_Camera_Type.NPC);

		mUI = transform.FindChild ("UI");
	}

    public virtual void InitWithID(string ID)
    {
        mNPCID = ID;
        gameObject.name = ID;
    }


	protected virtual object OnHandleNPCUpdate(object pSender)
	{
        JsonData tmpJson = new JsonData(pSender);
        if ((string)tmpJson["results"]["id"] != mNPCID)
            return null;

		return null;
	}

    protected object OnHandleActiveNPC(object pSender)
    {
//		Debug.Log ("Active");
        ActiveNPC();
        return null;
    }

    public virtual void ActiveNPC()
    {
        Dictionary<string, object> tmpSend = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_REQUEST_NPC_DATA);
        ((Dictionary<string, object>)tmpSend["results"]).Add("id", Client_User.Singleton().GetID());
		((Dictionary<string, object>)tmpSend["results"]).Add("npc", mNPCID);
        this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_REQUEST_NPC_DATA, tmpSend);

		if (mIsFadeCamera) 
		{
			this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_STOP_MAINCHARACTER,null);
			mFadeCamera.transform.position = transform.FindChild("CameraPos").position;
			mFadeCamera.transform.eulerAngles = transform.FindChild("CameraPos").eulerAngles;
			Game_Camera_Fade_Manager.Singleton().CrossFadeCamera(E_Camera_Type.Main,E_Camera_Type.NPC);

			if(mUI!=null)
			{
				mUI.gameObject.SetActive(true);
			}
		}
    }

	public virtual void ExitNPC()
	{
		if (mIsFadeCamera) 
		{
			this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_RESUME_MAINCHARACTER,null);	
			Game_Camera_Fade_Manager.Singleton().CrossFadeCamera(E_Camera_Type.NPC,E_Camera_Type.Main);
			
			if(mUI!=null)
				mUI.gameObject.SetActive(false);
		}
	}
}
