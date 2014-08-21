using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Scene_Controller : Controller 
{
    List<Game_FSM_NPC_Base> mNPCList = new List<Game_FSM_NPC_Base>();
	void OnEnable()
	{
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_SCENE_NPC,OnHandleInitNPC);
	}

	void OnDisable()
	{
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_SCENE_NPC, OnHandleInitNPC);
	}

    void Awake()
    {

    }

	public virtual void Init()
	{
        Dictionary<string, object> tmpSend = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_SCENE_DATA);
        ((Dictionary<string, object>)tmpSend["results"]).Add("id", Client_User.Singleton().GetID());
        ((Dictionary<string, object>)tmpSend["results"]).Add("target", gameObject.name);
        this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_SCENE_DATA, tmpSend);
	}

    object OnHandleInitNPC(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);
        for (int i = 0; i < tmpJson["results"]["npc"].Count; ++i)
        {
            //Game_Resources_Pool.Singleton().GetUnusedActorWithID((string)tmpJson["results"]["npc"][i]["id"]);
        }
        return null;
    }
}
