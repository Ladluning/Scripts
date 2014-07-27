using UnityEngine;
using System.Collections;

public class Game_FSM_Enemy_General_Controller : Controller {

	Animation mAnimation;
	void Awake()
	{
		mAnimation = gameObject.GetComponentInChildren<Animation>();
	}

	public void SetState(int State,string AnimationName)
	{
		mAnimation.CrossFade(AnimationName,0.3f);
	}
}
