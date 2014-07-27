using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client_User : Controller {

	// Use this for initialization
    private Vector3 LastPosition;
    void Update()
    {
        if (LastPosition != transform.position)
        {
            Dictionary<string, object> tmpCmd = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_POS);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("pos_x", transform.position.x);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("pos_y", transform.position.y);
            ((Dictionary<string, object>)tmpCmd["results"]).Add("pos_z", transform.position.z);
			((Dictionary<string, object>)tmpCmd["results"]).Add("rotate_x", transform.eulerAngles.x);
			((Dictionary<string, object>)tmpCmd["results"]).Add("rotate_y", transform.eulerAngles.y);
			((Dictionary<string, object>)tmpCmd["results"]).Add("rotate_z", transform.eulerAngles.z);
            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_UPDATE_POS, tmpCmd);
        }
        LastPosition = transform.position;
    }

    
    object OnHandleReceiveUpdatePos(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);
		if ((string)tmpJson["results"]["id"] != gameObject.name)
            return null;

        Debug.Log(MiniJSON.Json.Serialize(pSender));

        transform.position = new Vector3((float)(tmpJson["results"]["pos_x"]), (float)(tmpJson["results"]["pos_y"]), (float)(tmpJson["results"]["pos_z"]));
		transform.eulerAngles = new Vector3((float)(tmpJson["results"]["rotate_x"]), (float)(tmpJson["results"]["rotate_y"]), (float)(tmpJson["results"]["rotate_z"]));

		return null;
    }

    Dictionary<string, GameObject> mPlayerList = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> mEnemyList = new Dictionary<string, GameObject>();
    object OnHandleUpdateVisibleData(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);

        if ((string)tmpJson["results"]["id"] != gameObject.name)
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
