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
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA,OnHandleUpdateVisibleData);
    }

    void OnDisable()
    {
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, OnHandleLogin);
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA, OnHandleUpdateVisibleData);
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

		if ((string)tmpJson ["results"] ["id"] != ID)
			return null;

        transform.position = new Vector3((float)(tmpJson["results"]["pos_x"]), (float)(tmpJson["results"]["pos_y"]), (float)(tmpJson["results"]["pos_z"]));

        return pSender;
    }

    object OnHandleReceiveUpdatePos(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);
        if ((string)tmpJson["results"]["id"] != ID)
            return null;

        Debug.Log(MiniJSON.Json.Serialize(pSender));

        transform.position = new Vector3((float)(tmpJson["results"]["pos_x"]), (float)(tmpJson["results"]["pos_y"]), (float)(tmpJson["results"]["pos_z"]));
        return null;
    }

    Dictionary<string, GameObject> mPlayerList = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> mEnemyList = new Dictionary<string, GameObject>();
    object OnHandleUpdateVisibleData(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);

        if ((string)tmpJson["results"]["id"] != ID)
            return null;
		//Debug.Log(MiniJSON.Json.Serialize(pSender));
        for (int i = 0; i < tmpJson["results"]["visi_player"].Count; ++i)
        {
            HandlePlayerVisible(tmpJson["results"]["visi_player"][i]);
        }
        for (int i = 0; i < tmpJson["results"]["visi_enemy"].Count; ++i)
        {
            HandleEnemyVisible(tmpJson["results"]["visi_enemy"][i]);
        }

        return null;
    }

    void HandlePlayerVisible(JsonData Target)
    {
        GameObject tmpObject;
        if (mPlayerList.ContainsKey((string)Target["id"]))
        {
            tmpObject = mPlayerList[(string)Target["id"]];
        }
        else
        {
            tmpObject = Instantiate(Resources.Load("Client/Actor/"+(string)Target["mesh"])) as GameObject;
            mPlayerList.Add((string)Target["id"], tmpObject);
        }
        tmpObject.transform.position = new Vector3((float)Target["pos_x"], (float)Target["pos_y"], (float)Target["pos_z"]);
        //tmpObject.transform.localEulerAngles = new Vector3((float)Target["rotate_x"], (float)Target["rotate_y"], (float)Target["rotate_z"]);

        //Target["id"];
        //Target["hp"];
        //Target["mp"];
        //Target["exp"];
        //Target["mesh"];
    }

    void HandleEnemyVisible(JsonData Target)
    {
        GameObject tmpObject;
        if (mEnemyList.ContainsKey((string)Target["id"]))
        {
            tmpObject = mEnemyList[(string)Target["id"]];
        }
        else
        {
            tmpObject = Instantiate(Resources.Load("Client/Actor/"+(string)Target["mesh"])) as GameObject;
			mEnemyList.Add((string)Target["id"], tmpObject);
        }
        tmpObject.transform.position = new Vector3((float)Target["pos_x"], (float)Target["pos_y"], (float)Target["pos_z"]);
        tmpObject.transform.localEulerAngles = new Vector3((float)Target["rotate_x"], (float)Target["rotate_y"], (float)Target["rotate_z"]);

        //Target["scene"]
        //Target["fsm"]
        //Target["ani"]
        //Target["hp"]
        //Target["maxHP"]
        //Target["mp"]
        //Target["maxMP"]
        //Target["attack"]
        //Target["defend"]
        //Target["exp"]
    }
}
