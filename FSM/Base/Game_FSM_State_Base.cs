using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game_FSM_State_Base {

	public List<string> mStateComponent = new List<string>();

	protected Game_FSM_Controller mController;
	protected Dictionary<Type,object> mStateComonentList = new Dictionary<Type, object>();
	protected List<int> mStateTranslate = new List<int> ();

	public Game_FSM_State_Base(Game_FSM_Controller FSMController)
	{
		mController = FSMController;
	}

	public T GetComponent<T>()
	{
		if(mStateComonentList.ContainsKey(typeof(T)))
			return (T)mStateComonentList[typeof(T)];
		return default(T);
	}
	public void AddTranslate(int Target)
	{
		if (!mStateTranslate.Contains (Target)) 
		{
			mStateTranslate.Add(Target);
		}
	}
	public bool GetIsCouldTranslate(int Target)
	{
		return mStateTranslate.Contains (Target);
	}
	public virtual void Init()
	{
		foreach(string tmpComponentName in mStateComponent)
		{
			Game_ObjectBase tmpComponent = mController.GetComponent(tmpComponentName) as Game_ObjectBase;
			if(tmpComponent==null)
			{
				tmpComponent = mController.gameObject.AddComponent(tmpComponentName) as Game_ObjectBase;
			}

			mStateComonentList.Add(tmpComponent.GetType(),tmpComponent);
		}

		foreach(Type types in mStateComonentList.Keys)
		{
			((Game_ObjectBase)mStateComonentList[types]).Init();
		}

		Debug.Log ("InitState");
	}
	public virtual void OnEnter()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
	{
		foreach(Type types in mStateComonentList.Keys)
		{
			((MonoBehaviour)mStateComonentList[types]).enabled = true;
		}
	}
	
	public virtual void OnLoop(){}
	
	public virtual void OnExit()
	{
		foreach(Type types in mStateComonentList.Keys)
		{
			((MonoBehaviour)mStateComonentList[types]).enabled = false;
		}
	}

	protected void RegistListen(uint EventID,RegistFunction pFunction)
	{
		Facade.Singleton().RegistListen(EventID,pFunction);
	}

	protected void UnRegistListen(uint EventID,RegistFunction pFunction)
	{
		Facade.Singleton().UnRegistListen(EventID,pFunction);
	}

	protected void RegistEvent(uint EventID,RegistFunction pFunction)
	{	
		Facade.Singleton().RegistEvent(EventID,pFunction);
	}

	protected void UnRegistEvent(uint EventID,RegistFunction pFunction)
	{
		Facade.Singleton().UnRegistEvent(EventID,pFunction);
	}

	protected object SendEvent(uint EventID,object pSender)
	{
		return Facade.Singleton().SendEvent(EventID,pSender);
	}
}

