using UnityEngine;
using System.Collections;

public class Game_FSM_Enemy_General_State_Attack : Game_FSM_State_Base
{
    public Game_FSM_Enemy_General_State_Attack(Game_FSM_Controller FSMController) : base(FSMController) { }

	public override void Init()
	{
		base.Init();
	}


	public override void OnEnter()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
	{
		base.OnEnter ();
	}
	
	public virtual void OnLoop()
    {

    }
	
	public virtual void OnExit()
	{
		base.OnExit ();
	}
}
