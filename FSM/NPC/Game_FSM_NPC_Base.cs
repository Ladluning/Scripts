using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_FSM_NPC_Base : Controller 
{
	public string mNPCID;

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
    }
}
