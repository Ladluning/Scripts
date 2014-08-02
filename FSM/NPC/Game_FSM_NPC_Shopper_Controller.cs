using UnityEngine;
using System.Collections;
public enum E_State_NPC_Shopper
{
	Idle_Stand = 0,
}
public class Game_FSM_NPC_Shopper_Controller : Game_FSM_Controller
{


	protected Animation mAnimation;
	
	void Awake()
	{
		mAnimation = gameObject.GetComponentInChildren<Animation>();
		
		mStateMap.Add((int)E_State_NPC_Shopper.Idle_Stand, new Game_FSM_Enemy_General_State_Idle_Stand(this));

		this.InitState();
		this.StartState((int)E_State_Enemy_General.Idle_Stand);

	}
	
	public void SetState(int State,string AnimationName)
	{
		mAnimation.CrossFade(AnimationName,0.3f);
		this.SwitchState(State);
	}
}
