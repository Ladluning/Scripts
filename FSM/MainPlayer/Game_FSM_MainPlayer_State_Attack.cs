using UnityEngine;
using System.Collections;

public class Game_FSM_MainPlayer_State_Attack : Game_FSM_State_Base {

	public Game_FSM_MainPlayer_State_Attack(Game_FSM_Controller FSMController):base(FSMController){}


	public override void Init()
	{
		mStateComponent.Add("Game_ObjectAnimation");
		base.Init();
	}
	
	
	public override void OnEnter()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
	{
		base.OnEnter ();
		//GetComponent<Game_ObjectAnimation> ().PlayAnimation ("BG_Idle");
	}
	
	public virtual void OnLoop()
	{

	}
	
	public virtual void OnExit()
	{
		base.OnExit ();
	}
}
