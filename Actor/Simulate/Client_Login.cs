using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Client_Login : Controller {

	// Use this for initialization
	public GameObject PlayerPrefab;
	public string ID;
	void OnEnable()
	{
		this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, OnHandleLogin);
		this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_SWITCH_SCENE,OnHandleLogin);
		//this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA,OnHandleUpdateVisibleData);
	}
	
	void OnDisable()
	{
		this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, OnHandleLogin);
		this.UnRegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_SWITCH_SCENE,OnHandleLogin);
		//this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA, OnHandleUpdateVisibleData);
	}
	public void InitWithID(string Target)
	{
		ID = Target;
	}
	
	void Start () {
		Server.Server_User tmpCmd = new Server.Server_User();
		ID = SystemInfo.deviceUniqueIdentifier +"_"+ GameObject.FindObjectsOfType<Client_User>().Length.ToString();
		tmpCmd.ID = ID;
		this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_LOGIN, tmpCmd);
	}

	object OnHandleLogin(object pSender)
	{
		JsonData tmpJson = new JsonData(pSender);
		Debug.Log(MiniJSON.Json.Serialize(pSender));
		
		if ((string)tmpJson ["results"] ["id"] != ID)
			return null;

		this.SendEvent (GameEvent.FightingEvent.EVENT_FIGHT_INIT_LEVEL, tmpJson);

		return pSender;
	}


}
