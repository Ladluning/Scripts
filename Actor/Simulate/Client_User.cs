using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client_User : Controller {

	// Use this for initialization
    private Vector3 LastPosition;
    public string ID;
    
    void OnEnable()
    {
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, OnHandleLogin);
    }

    void OnDisable()
    {
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, OnHandleLogin);
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

    
    void Update()
    {
        if (LastPosition != transform.position)
        {
            Dictionary<string, object> tmpCmd = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_POS);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("pos_x", transform.position.x);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("pos_y", transform.position.y);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("pos_z", transform.position.z);
            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_POS, tmpCmd);
        }
        LastPosition = transform.position;
    }

    object OnHandleLogin(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);
        Debug.Log(MiniJSON.Json.Serialize(pSender));

        transform.position = new Vector3((float)(tmpJson["results"]["pos_x"]), (float)(tmpJson["results"]["pos_y"]), (float)(tmpJson["results"]["pos_z"]));

        return pSender;
    }

    object OnHandleReceiveUpdatePos(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);
        Debug.Log(MiniJSON.Json.Serialize(pSender));

        transform.position = new Vector3((float)(tmpJson["results"]["pos_x"]), (float)(tmpJson["results"]["pos_y"]), (float)(tmpJson["results"]["pos_z"]));
        return null;
    }

}
