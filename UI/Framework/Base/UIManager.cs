using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[System.Serializable]
public class ShowUIInfo
{
	public string UIName;
	public object pSender;
}

public class UIManager : Controller {
	
	public List<GameObject> UIPrefabArray = new List<GameObject>();
	public UIRelationship m_pRelationship;
	
	private Dictionary<Camera, UIBase> m_pUIMap = new Dictionary<Camera, UIBase>();
	private Dictionary<Camera, Transform> m_pUIAnchorMap = new Dictionary<Camera, Transform>();
	private List<Camera> m_pCameraList = new List<Camera>();

	private Counter m_pRetainCount = new Counter();
	private List<object> ShowUIList = new List<object>();
	
	private static UIManager m_pInterface;
	public static UIManager Singleton ()
	{
		return m_pInterface;
	}
	
	void Awake ()
	{
		m_pInterface = this;
		
		for(int i = 0; i < transform.childCount; i++)
		{
			m_pCameraList.Add(transform.FindChild("UICamera_" + i).GetComponent<Camera>());
		}
		for(int i = 0; i < m_pCameraList.Count; i++)
		{
			m_pUIMap.Add(m_pCameraList[i], null);
		}
		for(int i = 0; i < m_pCameraList.Count; i++)
		{
			m_pUIAnchorMap.Add(m_pCameraList[i], m_pCameraList[i].transform.FindChild("Anchor"));
		}
	}
	
	void Start ()
	{
//		this.SendEvent(GameEvent.UIEvent.EVENT_UI_SHOWUI, GlobalConst.UINAME_LOGINMANAGER);
	}

	void OnEnable ()
	{
		RegistEvent(GameEvent.UIEvent.EVENT_UI_SHOW_UI, OnHandleShowUI);
		RegistEvent(GameEvent.UIEvent.EVENT_UI_HIDE_UI, OnHandleHideUI);
		RegistEvent(GameEvent.UIEvent.EVENT_UI_COULD_TOUCH, OnHandleCanTouchUI);
		RegistEvent(GameEvent.UIEvent.EVENT_UI_COULD_NOT_TOUCH, OnHandleCantTouchUI);
		RegistEvent(GameEvent.UIEvent.EVENT_UI_ALL_COULD_TOUCH, OnHandleAllUICanTouch);
		RegistEvent(GameEvent.UIEvent.EVENT_UI_ALL_COULD_NOT_TOUCH, OnHandleAllUICantTouch);
	}
	
	void OnDisable ()
	{
		UnRegistEvent(GameEvent.UIEvent.EVENT_UI_SHOW_UI, OnHandleShowUI);
		UnRegistEvent(GameEvent.UIEvent.EVENT_UI_HIDE_UI, OnHandleHideUI);
		UnRegistEvent(GameEvent.UIEvent.EVENT_UI_COULD_TOUCH, OnHandleCanTouchUI);
		UnRegistEvent(GameEvent.UIEvent.EVENT_UI_COULD_NOT_TOUCH, OnHandleCantTouchUI);
		UnRegistEvent(GameEvent.UIEvent.EVENT_UI_ALL_COULD_TOUCH, OnHandleAllUICanTouch);
		UnRegistEvent(GameEvent.UIEvent.EVENT_UI_ALL_COULD_NOT_TOUCH, OnHandleAllUICantTouch);
	}
	
	//开启UI//
	object OnHandleShowUI (object pSender)
	{
		try
		{
			if(UIPrefabArray.Count > 0)
			{
				CreateTargetUI_Process(pSender);
			}
		}
		catch(UnityException ex)
		{
			Debug.Log ("ShowUI Error:"+ ex);
		}		
		catch(SystemException ex)
		{
			Debug.Log ("ShowUI Error:"+ ex);		
		}
		return null;
	}
	
	//关闭UI//
	object OnHandleHideUI (object pSender)
	{
		string tmpHideUIName = (string)pSender;
		Camera TmpCamera = GetCameraLayerWithUIName(tmpHideUIName);
		if(TmpCamera == null)
			return null;
		
		(m_pUIMap[TmpCamera] as UIBase).HideUI(tmpHideUIName);
		m_pUIMap[TmpCamera] = null;
		
		UIProperty TmpProperty = m_pRelationship.GetUIPropertyWithUIName(tmpHideUIName);
		TmpProperty.CloseCurrentUI();
		return null;
	}
	
	object OnHandleCanTouchUI (object pSender)
	{
		Camera TmpCamera = GetCameraLayerWithUIName((string)pSender);
		if(TmpCamera == null)
			return null;
		TmpCamera.GetComponent<UICamera>().enabled = true;
		return null;
	}
	
	object OnHandleCantTouchUI (object pSender)
	{
		Camera TmpCamera = GetCameraLayerWithUIName((string)pSender);
		if(TmpCamera == null)
			return null;
		TmpCamera.GetComponent<UICamera>().enabled = false;
		return null;
	}
	
	object OnHandleAllUICanTouch (object pSender)
	{
		if(!m_pRetainCount.Remove())
			return null;
		
		LogManager.Log(pSender + "'" + m_pRetainCount.m_nCounter);
		
		UIProperty TmpLastProperty = null;
		for(int i = m_pCameraList.Count - 1; i >= 0; i--)
		{
			if(m_pUIMap[m_pCameraList[i]] != null)
			{
				if(TmpLastProperty!=null)
				{
					if(TmpLastProperty.IsFatherUITouchEnable)
					{
						TmpLastProperty = m_pRelationship.GetUIPropertyWithUIName(((UIBase)m_pUIMap[m_pCameraList[i]]).name);
						m_pCameraList[i].GetComponent<UICamera>().enabled = true;
					}
				}
				else
				{
					TmpLastProperty = m_pRelationship.GetUIPropertyWithUIName(((UIBase)m_pUIMap[m_pCameraList[i]]).name);
					m_pCameraList[i].GetComponent<UICamera>().enabled = true;
				}
			}
		}
		return null;
	}
	
	object OnHandleAllUICantTouch (object pSender)
	{
		m_pRetainCount.Add();
		LogManager.Log(pSender + " " + m_pRetainCount.m_nCounter);
		for(int i = 0; i < m_pCameraList.Count; i++)
		{
			if(m_pUIMap[m_pCameraList[i]] != null)
			{
				m_pCameraList[i].GetComponent<UICamera>().enabled = false;
			}
		}
		return null;
	}
	
	
	void CreateTargetUI_Process (object pSender)
	{
		string  ShowUIName;
		ShowUIInfo SendInfo = null;
		if((SendInfo = pSender as ShowUIInfo) != null)
		{
			ShowUIName = ((ShowUIInfo)pSender).UIName;
			SendInfo = ((ShowUIInfo)pSender);
		}
		else if((ShowUIName = pSender as string) != null)
		{
			ShowUIName = (string)pSender;
		}
			
		UIProperty TmpProperty = m_pRelationship.GetUIPropertyWithUIName(ShowUIName);
		if(TmpProperty.TargetUI!=null)
			return;

		switch(TmpProperty.eLoadingType)
		{
		case ELoadingUIType.eLT_Alpha: this.SendEvent(GameEvent.UIEvent.EVENT_UI_SHOW_LOADING_ALPHA, null); break;
		case ELoadingUIType.eLT_Block: this.SendEvent(GameEvent.UIEvent.EVENT_UI_SHOW_LOADING_BLOCK, null); break;
		case ELoadingUIType.eLT_Transparent: this.SendEvent(GameEvent.UIEvent.EVENT_UI_SHOW_LOADING_TRANSPARENT, null); break;
		default: break;
		}

		TmpProperty.ShowCurrentUI();
		
		UIBase Tmp = InstantiatedUI(TmpProperty);
		
		if(!m_pRetainCount.IsRetain())
			OnHandleCanTouchUI(ShowUIName);
		else
			OnHandleCantTouchUI(ShowUIName);
		
		LogManager.Log("ShowUI:" + (string)ShowUIName + " With Layer:" + Tmp.gameObject.layer + " With Loading " + TmpProperty.eLoadingType.ToString());
		Tmp.ShowUI(SendInfo==null?null:SendInfo.pSender);
		if(Tmp!=null)
		{
			TmpProperty.SetCurrentUI(Tmp);
		}
		
		switch(TmpProperty.eLoadingType)
		{
		case ELoadingUIType.eLT_Alpha: this.SendEvent(GameEvent.UIEvent.EVENT_UI_HIDE_LOADING_ALPHA, null); break;
		case ELoadingUIType.eLT_Block: this.SendEvent(GameEvent.UIEvent.EVENT_UI_HIDE_LOADING_BLOCK, 1f); break;
		case ELoadingUIType.eLT_Transparent: this.SendEvent(GameEvent.UIEvent.EVENT_UI_HIDE_LOADING_TRANSPARENT ,null); break;
		default: break;
		}

		if(!m_pRetainCount.IsRetain())
			OnHandleCanTouchUI(ShowUIName);
		else
			OnHandleCantTouchUI(ShowUIName);
		
		ShowUIList.Remove(pSender);
	}
	
	public bool GetIsAllCouldTouch ()
	{
		return m_pRetainCount.m_nCounter <= 0;
	}
	
	//实例化UI//
	public UIBase InstantiatedUI (UIProperty pProperty)
	{
		GameObject TmpUI = null;
		foreach(GameObject TmpObject in UIPrefabArray)
		{
			if(TmpObject.name == pProperty.name)
				TmpUI = TmpObject;
		}
		
		if(TmpUI == null)
			TmpUI = Resources.Load("UI/"+pProperty.name,typeof(GameObject)) as GameObject;
		
		if(TmpUI != null)
		{
			UIBase Obj = (Instantiate(TmpUI) as GameObject).GetComponent<UIBase>();
			Obj.name = pProperty.name;
			OtherTool.SetLayer(Obj.gameObject,m_pCameraList[pProperty.GetCameraLayer()].gameObject.layer);
			Obj.transform.parent = m_pUIAnchorMap[m_pCameraList[pProperty.GetCameraLayer()]] as Transform;
			Obj.transform.localPosition = Vector3.zero;
			Obj.transform.localScale = Vector3.one;
			m_pUIMap[m_pCameraList[pProperty.GetCameraLayer()]] = Obj;	
			return Obj;
		}
		LogManager.LogError("Cant Find Target UI Prefab " + pProperty.name);
		return null;
	}
	
	//通过UIName得到摄像机//
	public Camera GetCameraLayerWithUIName (string Name)
	{
		for(int i = 0; i < m_pCameraList.Count; i++)
		{
			if(m_pUIMap[m_pCameraList[i]] != null)
			{
				if((m_pUIMap[m_pCameraList[i]] as UIBase).name == Name)
				{
					return m_pCameraList[i];
				}
			}
		}
		return null;
	}
	
	//检查摄像机是否含有其他UI//
	public bool CheckIsCameraHavaOtherUI (string Name)
	{
		for(int i = 0; i < m_pCameraList.Count; i++)
		{
			if(m_pUIMap[m_pCameraList[i]] != null)
			{
				if((m_pUIMap[m_pCameraList[i]] as UIBase).name == Name)
				{
					return m_pUIAnchorMap[m_pCameraList[i]].childCount > 1;
				}
			}
		}	
		return false;
	}
	
	//返回当前UI//
	public UIBase GetCurrentUI ()
	{
		for(int i = m_pCameraList.Count - 1; i >= 0; i--)
		{
			if(m_pUIMap[m_pCameraList[i]] != null)
			{
				return m_pUIMap[m_pCameraList[i]];
			}
		}
		return null;
	}
	
	//通过按钮ID得到按钮//
	public GameObject GetButtonWithID (uint ID)
	{
		for(int i = m_pCameraList.Count - 1; i >= 0; i--)
		{
			if(m_pUIMap[m_pCameraList[i]] != null)
			{
				GameObject Tmp = m_pUIMap[m_pCameraList[i]].GetGameObjectWithButtonID(ID);
				if(Tmp != null)
					return Tmp;
			}
		}	
		return null;
	}
}
