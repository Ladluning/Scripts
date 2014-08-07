using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_FSM_Controller : Controller {
	
	protected Dictionary<int,Game_FSM_State_Base> mStateMap = new Dictionary<int, Game_FSM_State_Base>();
	protected int  mCurrentState = -1;
	protected bool mAIStop = true;
	protected virtual void Awake()
	{

	}

	protected void InitState()
	{
		foreach(int tmpKey in mStateMap.Keys)
		{
			mStateMap[tmpKey].Init();
		}
	}

	public void StartState(int nStateID)
	{
		mCurrentState = nStateID;
		mStateMap[mCurrentState].OnEnter();
	}

	public void SwitchState(int nNextState)
	{
		if(mCurrentState!=null&&nNextState!=mCurrentState&&mStateMap[mCurrentState].GetIsCouldTranslate(nNextState))
		{
			mStateMap[mCurrentState].OnExit();

			mCurrentState = nNextState;
			mStateMap[mCurrentState].OnEnter();
		}
	}

	protected virtual void Update()
	{
		if(mAIStop)
			return;

		mStateMap[mCurrentState].OnLoop();
	}

}
