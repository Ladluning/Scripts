using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Struct_Data_Talent_Node
{
    public string NodeID;
    public bool Avaliable;
    public int CurrentCount;
    public int MaxCount;
}
public class Client_Talent : Controller,Client_Component
{

    private List<Struct_Data_Talent_Node> mTalentList = new List<Struct_Data_Talent_Node>();
	void OnEnable()
	{
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_TALENT_DATA,OnHandleInitTalentData);
        this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_TALENT_DATA, OnHandleInitTalentData);
	}

	void OnDisable()
	{
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_TALENT_DATA, OnHandleInitTalentData);
        this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_TALENT_DATA, OnHandleInitTalentData);
	}

    void Awake()
    {

    }

    public void Init()
    {
		Dictionary<string, object> tmpSend = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_TALENT_DATA);
		((Dictionary<string, object>)tmpSend["results"]).Add("id", Client_User.Singleton().GetID());
		this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_TALENT_DATA, tmpSend);
    }

    object OnHandleInitTalentData(object pSender)
    {
		Debug.Log (MiniJSON.Json.Serialize(pSender));
        JsonData tmpJson = new JsonData(pSender);
        if ((string)tmpJson["results"]["id"] != gameObject.name)
            return null;

        for (int i = 0; i < tmpJson["results"]["talent"].Count; ++i)
        {
            Struct_Data_Talent_Node tmpNode = GetTalentNodeWithID((string)tmpJson["results"]["talent"][i]["id"]);
            if (tmpNode == null)
            {
                tmpNode = new Struct_Data_Talent_Node();
                tmpNode.NodeID = (string)tmpJson["results"]["talent"][i]["id"];
				mTalentList.Add(tmpNode);
            }

            tmpNode.CurrentCount = (int)tmpJson["results"]["talent"][i]["count"];
            tmpNode.Avaliable = (bool)tmpJson["results"]["talent"][i]["active"];
            tmpNode.MaxCount = (int)tmpJson["results"]["talent"][i]["max"];
            
        }
		Debug.Log (mTalentList.Count+" "+tmpJson["results"]["talent"].Count);
        return null;
    }

    public Struct_Data_Talent_Node GetTalentNodeWithID(string ID)
    {
        for (int i = 0; i < mTalentList.Count; ++i)
        {
            if (mTalentList[i].NodeID == ID)
                return mTalentList[i];
        }
        return null;
    }
}

