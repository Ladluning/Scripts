using UnityEngine;
using System.Collections;
public enum E_State_Enemy_General
{
    Idle_Move = 0,
    Idle_Stand,
    Move,
    Attack,
    Away,
    Died,
    Escape,
}
public class Game_FSM_Enemy_General_Controller : Game_FSM_Controller
{
	Animation mAnimation;
	void Awake()
	{
		mAnimation = gameObject.GetComponentInChildren<Animation>();

        mStateMap.Add((int)E_State_Enemy_General.Idle_Stand, new Game_FSM_Enemy_General_State_Idle_Stand(this));
        mStateMap[(int)E_State_Enemy_General.Idle_Stand].AddTranslate((int)E_State_Enemy_General.Idle_Move);

        mStateMap.Add((int)E_State_Enemy_General.Idle_Move, new Game_FSM_Enemy_General_State_Idle_Move(this));
        mStateMap[(int)E_State_Enemy_General.Idle_Move].AddTranslate((int)E_State_Enemy_General.Idle_Stand);

        this.InitState();
        this.StartState((int)E_State_Enemy_General.Idle_Stand);
	}

	public void SetState(int State,string AnimationName)
	{
		mAnimation.CrossFade(AnimationName,0.3f);
        this.SwitchState(State);
	}
}
