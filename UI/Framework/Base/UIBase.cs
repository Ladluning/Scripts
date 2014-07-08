using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
struct UIClickSendInfo
{
	public GameObject ClickObject;//按钮//
	public uint ObjectID;//按钮ID//
}
public class UIBase : Controller {
	
    protected Dictionary<uint, GameObject> m_pUINodeMap = new Dictionary<uint, GameObject>();//按钮ID 按钮//
	protected Dictionary<uint, RegistFunction> m_pUIFunctionMap = new Dictionary<uint, RegistFunction>();//按钮ID 方法//
	private UIClickSendInfo ClickInfo = new UIClickSendInfo();
	protected string mDataPath;
	protected string mPersistentPath;
	protected string mStreamAssetPath;
	
	protected UIManager m_pUIManagerInterface;
	protected UIManager m_pUIManager
	{
		set{
			m_pUIManagerInterface = value;
		}
		get{
			if(m_pUIManagerInterface == null)
				m_pUIManagerInterface = UIManager.Singleton();
			return m_pUIManagerInterface;
		}
	}

	public void InitButtonMessage(Transform target)
	{
		for (int i=0; i<target.childCount; i++) 
		{
			InitButtonMessage(target.GetChild(i));
		}
		UIButtonMessage tmpMsg = target.GetComponent<UIButtonMessage> ();
		if (tmpMsg!=null)
		{
			tmpMsg.functionName = "ClickButton";
			tmpMsg.target = gameObject;
		}
	}

	public void GetPersistentPath ()
	{
		mDataPath = Application.dataPath;
		mPersistentPath = Application.persistentDataPath;
		mStreamAssetPath = Application.streamingAssetsPath;
	}
	
	// Use this for initialization
	private List<UITweener_Extern> m_pUIActionList = new List<UITweener_Extern>();
	void GetUITweenerInChildren (Transform Target)
	{
		for(int i = 0; i < Target.childCount; i++)
		{
			GetUITweenerInChildren(Target.GetChild(i));
		}
		UITweener_Extern TmpPosition = Target.GetComponent<TweenPosition_Extern>();
		if(TmpPosition != null)
		{
			m_pUIActionList.Add(TmpPosition);
		}
		UITweener_Extern tmpAlpha = Target.GetComponent<TweenAlpha_Extern>();
		if (tmpAlpha != null)
		{
			m_pUIActionList.Add(tmpAlpha);
		}
		UITweener_Extern tmpScale = Target.GetComponent<TweenScale_Extern>();
		if (tmpScale != null)
		{
			m_pUIActionList.Add(tmpScale);
		}
	}
	
	//关闭UI//
	bool IsHide = false;
	public virtual void HideUI (string Name)
	{
		IsHide = true;
		if(m_pUIActionList.Count <= 0)
			Destroy(gameObject);
		else
			StartCoroutine(WaitDestroy(0.4f));
	}
	
	//等待销毁UI//
	IEnumerator WaitDestroy (float WaitTime)
	{
		foreach(UITweener_Extern Tmp in m_pUIActionList)
		{
			Tmp.Play(false);
		}
			
		yield return new WaitForSeconds(WaitTime);
		Destroy(gameObject);
	}
	
	//开始显示UI//
	public virtual void ShowUI (object pSender)
	{
		GetUITweenerInChildren(transform);
		IsHide = false;
	}
	
	//按钮点击事件//
	public virtual object ClickButton (object tmp)
	{
		if(IsHide)
			return null;

		foreach(uint Tmp in m_pUINodeMap.Keys)
		{
			if(m_pUINodeMap[Tmp] == tmp)
			{
				LogManager.Log("Click Button: " + m_pUINodeMap[Tmp].name);
				ClickInfo.ClickObject = tmp as GameObject;
				ClickInfo.ObjectID = Tmp;
				this.SendEvent(GameEvent.UIEvent.EVENT_UI_CLICK_BUTTON, ClickInfo);
				
				if(m_pUIFunctionMap.ContainsKey(Tmp) && m_pUIFunctionMap[Tmp] != null)
					m_pUIFunctionMap[Tmp](tmp);
			}
		}

		return null;
	}
	
	//通过按钮ID得到按钮//
	public GameObject GetGameObjectWithButtonID (uint ID)
	{
		if(m_pUINodeMap.ContainsKey(ID))
			return m_pUINodeMap[ID];
		return null;
	}
	
	//设置按钮点击事件//
	public void SetSingleGameObjMessage (GameObject vSetObj, GameObject vTargetObj, string vFunction)
	{
		if ((vSetObj == null) || (vTargetObj == null) || (string.IsNullOrEmpty(vFunction)))
			return ;
		if (vSetObj != null)
		{
			if (vSetObj.GetComponent<UIButtonMessage>() == null)
				vSetObj.AddComponent<UIButtonMessage>();
			vSetObj.GetComponent<UIButtonMessage>().target = vTargetObj;
			vSetObj.GetComponent<UIButtonMessage>().functionName = vFunction;
		}
	}
	
}
