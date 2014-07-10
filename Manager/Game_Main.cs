using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Main : Controller {

	// Use this for initialization
    void OnEnable()
    {
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, OnHandleLogin);
    }

    void OnDisable()
    {
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN,OnHandleLogin);
    }
	void Start () {
        //Dictionary<string,object> tmpCmd = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_REGIST);
        //((Dictionary<string, object>)tmpCmd["results"]).Add("udid", SystemInfo.deviceUniqueIdentifier);

        Server.Server_User tmpCmd = new Server.Server_User();
        tmpCmd.ID = SystemInfo.deviceUniqueIdentifier;
        this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_LOGIN, tmpCmd);
		//this.SendEvent (GameEvent.UIEvent.EVENT_UI_SHOW_UI,"TD_UI_MainGame");
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

    object OnHandleLogin(object pSender)
    {
        Debug.Log(pSender);
        return pSender;
    }
}
