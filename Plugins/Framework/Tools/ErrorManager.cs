using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ErrorManager : Controller {
	
	void OnEnable()
	{
		this.RegistEvent(GameEvent.SysEvent.EVENT_SYS_ERROR,OnHandleError);
		//this.RegistListen(GameEvent.WebEvent.EVENT_WEB_CARRIER_TIME_OUT,OnListenTimeOut);
		this.RegistListen(GameEvent.WebEvent.EVENT_WEB_NOT_REACHABLE,OnListenNotReachable);
	}
	void OnDisable()
	{
		this.UnRegistEvent(GameEvent.SysEvent.EVENT_SYS_ERROR,OnHandleError);
		//this.UnRegistListen(GameEvent.WebEvent.EVENT_WEB_CARRIER_TIME_OUT,OnListenTimeOut);
		this.UnRegistListen(GameEvent.WebEvent.EVENT_WEB_NOT_REACHABLE,OnListenNotReachable);
	}
	
	void Awake()
	{
		LogManager.InitLog();
	}
	
	object OnListenTimeOut(object pSender)
	{
		return null;
	}

	object OnListenNotReachable(object pSender)
	{
		return null;
	}
	
	object ClickNotReachableOK(object pSender)
	{
		return null;
	}
	
	object OnHandleError(object pSender)
	{
		JsonData tmpData = (JsonData)pSender;
		return 0;
	}
}
