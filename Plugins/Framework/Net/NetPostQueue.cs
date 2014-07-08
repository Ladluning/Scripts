using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class NetPostQueue : Controller 
{
	public static List<PostStruct> sendList = new List<PostStruct>();
	public static List<string> receiveList = new List<string>();
	private List<string> lastReceiveList = new List<string>();
	NetMobile.CSharpSocketTalkInterface pTalkSocket;
	NetMobile.CSharpHtmlInterface pHtml;
	long CurrentSendID = 0;
	void Awake()
	{
		pTalkSocket = gameObject.AddComponent<NetMobile.CSharpSocketTalkInterface>();
		pHtml = gameObject.AddComponent<NetMobile.CSharpHtmlInterface>();

	}
	
	void OnDisable()
	{
		pTalkSocket.Close();
	}
	
	void Start()
	{
//		if(!pTalkSocket.InitSocket(GlobalValueManager.Socket_Address,GlobalValueManager.Socket_Port,512))
//		{
//			Debug.Log("Init Talk Socket Error");
//		}
		
//		InitSocket();
	}
	
	public void InitSocket ()
	{
		if(!pTalkSocket.InitSocket(GlobalValueManager.Socket_Address, GlobalValueManager.Socket_Port, 512))
		{
			Debug.Log("Init Talk Socket Error");
		}
	}
	
	public bool PushMessage(ENetType eNetType,string URL,object pSender,uint nLayer = 0)
	{
		for(int i=0;i<sendList.Count;i++)
		{
			if(nLayer>sendList[i].m_nLayer)
			{
				sendList.Insert(i,new PostStruct(eNetType,URL,pSender,nLayer));
				
				if(i==0)
					return true;
				
				return false;
			}
		}
		sendList.Add(new PostStruct(eNetType,URL,pSender,nLayer));
		sendList[sendList.Count-1].MakeMD5(CurrentSendID++);
		if(sendList.Count==1)
			return true;
		return false;
	}
	
	public string PullReceiveMessage()
	{
		if(receiveList.Count>0)
		{
			string tmpMsg = receiveList[0];
			receiveList.RemoveAt(0);
			return tmpMsg;
		}
		return null;
	}
	
	void DispatcherEvent()
	{
		string Msg = PullReceiveMessage();
		
		if(Msg !=null&&Msg!="")
		{
			if(GetIsInsertNewReceive(Msg))
			{
				Debug.Log (Msg);
				JsonData ReceiveData = new JsonData(MiniJSON.Json.Deserialize(Msg));

				if((Int32)ReceiveData["success"]==0)
				{
					this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR,ReceiveData);
				}
				else
				{
					this.SendEvent((uint)ReceiveData["event"],ReceiveData["results"]);
				}
			}
		}
	}

	bool GetIsInsertNewReceive(string Msg)
	{
		foreach(string pTalkSocketMsg in lastReceiveList)
		{
			if(pTalkSocketMsg.CompareTo(Msg)==0)
				return false;
		}
			
		if(lastReceiveList.Count>=5)
		{
			lastReceiveList.RemoveAt(0);
		}
			
		lastReceiveList.Add(Msg);
		return true;
	}
	
	void Update()
	{
		DispatcherEvent();
	}
}
