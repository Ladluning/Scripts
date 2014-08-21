using UnityEngine;
using System.Collections;

public class Game_Camera_Fade_Manager : Controller {

	private static Game_Camera_Fade_Manager m_pInterface;
	public static Game_Camera_Fade_Manager Singleton()
	{
		return m_pInterface;
	}

	void Awake()
	{
		m_pInterface = this;
	}

	public float mFadeTime = 2f;
	public AnimationCurve mFadeCurve;
	public void CrossFadeCamera(Camera Current,Camera Target)
	{
		ScreenWipe.use.CrossFade (Current, Target, mFadeTime, mFadeCurve);
	}
}


