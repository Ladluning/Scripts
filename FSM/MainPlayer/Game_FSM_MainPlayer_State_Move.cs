using UnityEngine;
using System.Collections;

public class Game_FSM_MainPlayer_State_Move : Game_FSM_State_Base {

	public Game_FSM_MainPlayer_State_Move(Game_FSM_Controller FSMController):base(FSMController){}

	private Vector3 mTargetPos;

	public override void Init()
	{
		mStateComponent.Add ("Game_ObjectAnimation");
		mStateComponent.Add ("Game_ObjectAction");
		base.Init();
	}
	
	
	public override void OnEnter()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
	{
		base.OnEnter ();
		GetComponent<Game_ObjectAnimation> ().PlayAnimation ("BG_Run");
	}
	
	public virtual void OnLoop()
	{

	}
	
	public virtual void OnExit()
	{
		base.OnExit ();

	}

	public void SetTargetPos(Vector3 TargetPos)
	{
		mTargetPos = TargetPos;
		GetComponent<Game_ObjectAction> ().MoveToTarget (mTargetPos,MoveEndCallBack);
	}

	object MoveEndCallBack(object pSender)
	{
		mController.SwitchState ((int)E_State_MainPlayer.Idle);
		return null;
	}
}
