using UnityEngine;
using System.Collections;

public class Game_FSM_NPC_Data_Package : Game_Storage_Manager_Base
{

	protected virtual void OnEnable()
	{
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_NPC_PACKAGE, OnHandleUpdateNPCPackage);
	}
	
	protected virtual void OnDisable()
	{
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_NPC_PACKAGE, OnHandleUpdateNPCPackage);
	}


	protected object OnHandleUpdateNPCPackage(object pSender)
	{
        JsonData tmpJson = new JsonData(pSender);
		Debug.Log (MiniJSON.Json.Serialize(pSender));
        if ((string)tmpJson["results"]["id"] != gameObject.name)
            return null;

        if (tmpJson["results"].Dictionary.ContainsKey("size"))
            mStorageSlotCount = (int)tmpJson["results"]["size"];
        if (tmpJson["results"].Dictionary.ContainsKey("maxSize"))
            mStorageSlotCount = (int)tmpJson["results"]["maxSize"];

        for (int i = 0; i < tmpJson["results"]["packages"].Count; ++i)
        {
            if (GetItemWithID((long)tmpJson["results"]["packages"][i]["id"]) != null)
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
}
