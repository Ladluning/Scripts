using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client_Package : Game_Storage_Manager_Base 
{
    void OnEnable()
    {
        this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_Add_USER_STORAGE, OnHandleReceiveUpdatePackage);
		this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_STORAGE,OnHandleReceiveUpdatePackage);
	}

    void OnDisable()
    {
        this.UnRegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_Add_USER_STORAGE, OnHandleReceiveUpdatePackage);
		this.UnRegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_STORAGE,OnHandleReceiveUpdatePackage);
	}

	void Awake()
	{

	}

    object OnHandleReceiveUpdatePackage(object pSender)
    {
		Debug.Log(MiniJSON.Json.Serialize(pSender));
        JsonData tmpJson = new JsonData(pSender);
        if ((string)tmpJson["results"]["id"] != gameObject.name)
            return null;

        for (int i = 0; i < tmpJson["results"]["packages"].Count; ++i)
        {
            Game_Item_Base tmpItem = Game_Item_Serialize.ConvertJsonToItem(tmpJson["results"]["packages"][i]);
            this.InsertItemWithInStorage(tmpItem);
        }

        return null;
    }

    void SerializePakcage()
    {

    }
}
