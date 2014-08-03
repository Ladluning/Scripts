using UnityEngine;
using System.Collections;

public class NetManager : Controller {

	//string URL_Address = "http://bbs.enveesoft.com:84/brave/index.php";
	string Http_Address = "http://192.168.18.152:8100/api";
	string Socket_Address = "http://192.168.18.152";
	int Socket_Port = 8200;

	NetPostQueue pNet;
	
	void OnEnable()
	{
		RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_VERSION, OnHandleGetVersion);
		//RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_HEART, OnHandleSendHeart);
		RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_REGIST, OnHandleRegist);
		RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_LOGIN, OnHandleSocketLogin);

		RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_TALK, OnHandle_SendTalk);
		
		//RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, OnHandle_LoginSuccess);
	}
	void OnDisable()
	{
		UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_VERSION, OnHandleGetVersion);
		//UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_HEART, OnHandleSendHeart);
		UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_REGIST ,OnHandleRegist);
        UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_LOGIN, OnHandleSocketLogin);

		UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_TALK, OnHandle_SendTalk);


        //UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_LOGIN, OnHandle_LoginSuccess);
	}
	void Awake()
	{
		pNet = gameObject.GetComponent<NetPostQueue>();
		if(!pNet)
			LogManager.LogError("Cant Find NetPostQueue in"+ gameObject.name);
		
		GlobalValueManager.UDID = SystemInfo.deviceUniqueIdentifier;
	}

	object OnHandle_SendTalk (object pSender)
	{
		pNet.PushMessage(ENetType.Socket,GlobalValueManager.Socket_Address,pSender);
		return null;
	}

	object OnHandle_LoginSuccess (object pSender)
	{
		pNet.InitSocket();
		return null;
	}

	object OnHandleGetVersion(object pSender)
	{
		pNet.PushMessage(ENetType.Html,GlobalValueManager.Http_Address, pSender);
		return null;
	}

	object OnHandleRegist(object pSender)
	{
		pNet.PushMessage(ENetType.Html, GlobalValueManager.Http_Address, pSender);
		return null;
	}
	
	object OnHandleHttpLogin (object pSender)
	{
		pNet.PushMessage(ENetType.Html, GlobalValueManager.Http_Address, pSender,256);
		return null;
	}

	object OnHandleSocketLogin(object pSender)
	{
		pNet.PushMessage(ENetType.Socket, GlobalValueManager.Socket_Address, pSender,256);
		return null;
	}

	object OnHandleSendHeart(object pSender)
	{
		if(pNet.PushMessage(ENetType.Socket,"",pSender))
		{
			Debug.Log("True");
		}
		return null;
	}

	object OnHandleSendTask(object pSender)
	{
		JsonData tmpJson = new JsonData();
		tmpJson.Dictionary.Add("task_id",(int)pSender);
		tmpJson.Dictionary.Add("reward_message","[kl]you get\na reward!");

		//this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_TASKID,tmpJson);
		return null;
	}
}
