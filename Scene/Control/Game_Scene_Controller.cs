using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Scene_Controller : Controller 
{
    List<Game_FSM_NPC_Base> mNPCList = new List<Game_FSM_NPC_Base>();

	public Transform mInstanceNode;
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
        this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_SCENE_DATA, tmpSend);
	}

    object OnHandleInitNPC(object pSender)
    {
		Debug.Log (MiniJSON.Json.Serialize(pSender));
        JsonData tmpJson = new JsonData(pSender);
        for (int i = 0; i < tmpJson["results"]["npc"].Count; ++i)
        {
            GameObject tmpNPC = Instantiate(Game_Resources_Manager.Singleton().GetNPCWithID ((string)tmpJson["results"]["npc"][i]["id"])) as GameObject;
            tmpNPC.SendMessage("InitWithID", (string)tmpJson["results"]["npc"][i]["id"]);
			tmpNPC.transform.parent = mInstanceNode;
			Vector3 tmpPos = new Vector3((float)(tmpJson["results"]["npc"][i]["pos_x"]), (float)(tmpJson["results"]["npc"][i]["pos_y"]), (float)(tmpJson["results"]["npc"][i]["pos_z"]));
			Vector3 tmpRotate = new Vector3((float)(tmpJson["results"]["npc"][i]["rotate_x"]), (float)(tmpJson["results"]["npc"][i]["rotate_y"]), (float)(tmpJson["results"]["npc"][i]["rotate_z"]));
            tmpNPC.transform.localPosition = tmpPos;
            tmpNPC.transform.localEulerAngles = tmpRotate;

        }
        return null;
    }
}
