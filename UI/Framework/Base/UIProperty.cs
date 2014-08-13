using UnityEngine;
using System.Collections;

public enum EShowUIAction
{
	eUA_None = 0,
	eUA_Alpha,
	eUA_Translate,
}

public class UIAction
{
	public GameObject TargetObject;
	public EShowUIAction eAction;
	public float ActionSpeed = 1f;
	
	public Vector3 OriginPos;
	public Vector3 TargetPos;
	
}
public class UIProperty : Controller {

	public bool IsCloseBrotherUIWithShow;
	public bool IsCloseFatherUIWithShow;
	public bool IsCloseChildUIWithHide = true;
	
	public bool IsFatherUITouchEnable;
	public int  CameraLayer;
	
	public UIBase TargetUI = null;

	private UIManager m_pUIManager;
	private Transform  m_pCurrentTransform;
	void Awake()
	{
		m_pCurrentTransform = transform;
	}
	void Start()
	{
		m_pUIManager = UIManager.Singleton();
	}
	public void CloseCurrentUI()
	{
		if(IsCloseChildUIWithHide)
		{
			for(int i = 0; i < m_pCurrentTransform.childCount; i++)
			{
				m_pCurrentTransform.GetChild(i).GetComponent<UIProperty>().CloseCurrentUI();
			}
		}
		if(TargetUI)
		{
			TargetUI = null;
			this.SendEvent(GameEvent.UIEvent.EVENT_UI_HIDE_UI, this.name);
		}
		
		if(!IsFatherUITouchEnable)
		{
			if(m_pCurrentTransform.parent && m_pCurrentTransform.parent.name != "UIRelation")
				this.SendEvent(GameEvent.UIEvent.EVENT_UI_COULD_TOUCH, m_pCurrentTransform.parent.name);			
		}
	}
	
	public void ShowCurrentUI()
	{
		if(IsCloseBrotherUIWithShow)
		{
			Transform TmpParent = m_pCurrentTransform.parent;
			for(int i = 0; i < TmpParent.childCount; i++)
			{
				TmpParent.GetChild(i).GetComponent<UIProperty>().CloseCurrentUI();
			}
		}
		if(IsCloseFatherUIWithShow)
		{
			m_pCurrentTransform.parent.GetComponent<UIProperty>().CloseCurrentUI();
		}

		if(!IsFatherUITouchEnable)
		{
			if(m_pCurrentTransform.parent)
				this.SendEvent(GameEvent.UIEvent.EVENT_UI_COULD_NOT_TOUCH, m_pCurrentTransform.parent.name);
		}
	}
	public void SetCurrentUI(UIBase CurrentUI)
	{
		TargetUI = CurrentUI;
	}
	public int GetCameraLayer()
	{
		return CameraLayer;
	}
	
	public bool GetIsFatherUITouchEnable()
	{
		return IsFatherUITouchEnable;
	}
}
