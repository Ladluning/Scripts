using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client_Package : Game_Storage_Manager_Base ,Client_Component
{
    void OnEnable()
    {
		this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_STORAGE, OnHandleReceiveInitPackage);
        this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_Add_USER_STORAGE, OnHandleReceiveUpdatePackage);
		this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_USER_STORAGE,OnHandleReceiveUpdatePackage);
	}

    void OnDisable()
    {
		this.UnRegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_STORAGE, OnHandleReceiveInitPackage);
        this.UnRegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_Add_USER_STORAGE, OnHandleReceiveUpdatePackage);
		this.UnRegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_USER_STORAGE,OnHandleReceiveUpdatePackage);
	}

	void Awake()
	{
		Dictionary<string, object> tmpSend = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_USER_STORAGE);
		((Dictionary<string, object>)tmpSend["results"]).Add("id", Client_User.Singleton().GetID());
		this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_USER_STORAGE,tmpSend);
	}

	public void Init()
	{

	}

	object OnHandleReceiveInitPackage(object pSender)
	{
		Debug.Log(MiniJSON.Json.Serialize(pSender));
		JsonData tmpJson = new JsonData(pSender);
		if ((string)tmpJson["results"]["id"] != gameObject.name)
			return null;
		
		if(tmpJson["results"].Dictionary.ContainsKey("size"))
			mStorageSlotCount = (int)tmpJson["results"]["size"];
		if(tmpJson["results"].Dictionary.ContainsKey("maxSize"))
			mStorageSlotMaxCount = (int)tmpJson["results"]["maxSize"];
		
		for (int i = 0; i < tmpJson["results"]["packages"].Count; ++i)
		{
			Game_Item_Base tmpItem = Game_Item_Serialize.ConvertJsonToItem(tmpJson["results"]["packages"][i]);
			tmpItem.mItemOwner = gameObject.name;
			this.InsertItemWithInStorage(tmpItem);
		}
		
		return null;
	}

    object OnHandleReceiveUpdatePackage(object pSender)
    {
		Debug.Log(MiniJSON.Json.Serialize(pSender));
        JsonData tmpJson = new JsonData(pSender);
        if ((string)tmpJson["results"]["id"] != gameObject.name)
            return null;

		if(tmpJson["results"].Dictionary.ContainsKey("size"))
			mStorageSlotCount = (int)tmpJson["results"]["size"];
		if(tmpJson["results"].Dictionary.ContainsKey("maxSize"))
			mStorageSlotCount = (int)tmpJson["results"]["maxSize"];

        for (int i = 0; i < tmpJson["results"]["packages"].Count; ++i)
        {
			if(GetItemWithID((long)tmpJson["results"]["packages"][i]["id"])!=null)
			{
				Game_Item_Base tmpItem = GetItemWithID((long)tmpJson["results"]["packages"][i]["id"]);
				tmpItem.mItemMainType = (int)tmpJson["results"]["packages"][i]["type"];
				tmpItem.mItemSubType = (int)tmpJson["results"]["packages"][i]["subtype"];
				tmpItem.mItemID = (long)tmpJson["results"]["packages"][i]["id"];
				tmpItem.mItemPosID = (int)tmpJson["results"]["packages"][i]["pos"];
				tmpItem.mCurrentCount = (int)tmpJson["results"]["packages"][i]["num"];
				tmpItem.mMaxCount = (int)tmpJson["results"]["packages"][i]["max"];
				tmpItem.mUse = (int)tmpJson["results"]["packages"][i]["use"];
			}
			else
			{
           		Game_Item_Base tmpItem = Game_Item_Serialize.ConvertJsonToItem(tmpJson["results"]["packages"][i]);
				tmpItem.mItemOwner = gameObject.name;
				this.InsertItemWithInStorage(tmpItem);
			}
        }

        return null;
    }

    void SerializePakcage()
    {

    }
}
