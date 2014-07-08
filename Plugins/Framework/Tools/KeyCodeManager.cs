using UnityEngine;
using System.Collections;

public class KeyCodeManager : Controller {
	
	// Update is called once per frame
	void Update () {
		//回车键//
		if (Input.GetKey(KeyCode.Return))
		{
			OnHandle_ClickReturn(null);
		}
		
		//返回键//
		if (Input.GetKey(KeyCode.Escape))
		{
			OnHandle_ClickEscape(null);
		}
		
		//Home键//
		if (Input.GetKey(KeyCode.Home))
		{
			OnHandle_ClickHome(null);
		}
		
		//菜单键//
		if (Input.GetKey(KeyCode.Menu))
		{
			OnHandle_ClickMenu(null);
		}
	}

	void OnEnable ()
	{
		RegistEvent(GameEvent.SysEvent.EVENT_SYS_KEYCODE_RETURN, OnHandle_ClickReturn);
		RegistEvent(GameEvent.SysEvent.EVENT_SYS_KEYCODE_ESCAPE, OnHandle_ClickEscape);
		RegistEvent(GameEvent.SysEvent.EVENT_SYS_KEYCODE_HOME, OnHandle_ClickHome);
		RegistEvent(GameEvent.SysEvent.EVENT_SYS_KEYCODE_MENU, OnHandle_ClickMenu);
	}
	void OnDisable ()
	{
		UnRegistEvent(GameEvent.SysEvent.EVENT_SYS_KEYCODE_RETURN, OnHandle_ClickReturn);
		UnRegistEvent(GameEvent.SysEvent.EVENT_SYS_KEYCODE_ESCAPE, OnHandle_ClickEscape);
		UnRegistEvent(GameEvent.SysEvent.EVENT_SYS_KEYCODE_HOME, OnHandle_ClickHome);
		UnRegistEvent(GameEvent.SysEvent.EVENT_SYS_KEYCODE_MENU, OnHandle_ClickMenu);
	}
	
	//回车键//
	object OnHandle_ClickReturn (object pSender)
	{
		return null;
	}
	//退出键//
	object OnHandle_ClickEscape (object pSender)
	{
		System.Diagnostics.Process.GetCurrentProcess().Kill();
		return null;
	}
	//Home键//
	object OnHandle_ClickHome (object pSender)
	{
		return null;
	}
	//菜单键//
	object OnHandle_ClickMenu (object pSender)
	{
		return null;
	}
	
}
