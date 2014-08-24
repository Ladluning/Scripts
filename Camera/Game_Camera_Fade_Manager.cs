using UnityEngine;
using System.Collections;
public enum E_Camera_Type{
	Main = 0,
	NPC,
	Story,
}
public class Game_Camera_Fade_Manager : Controller {

	public Camera[] mCameraList;

	private static Game_Camera_Fade_Manager m_pInterface;
	public static Game_Camera_Fade_Manager Singleton()
	{
		return m_pInterface;
	}

	void Awake()
	{
		m_pInterface = this;
	}

	public Camera GetCameraWithType(E_Camera_Type Target)
	{
		return mCameraList [(int)Target];
	}

	public float mFadeTime = 2f;
	public AnimationCurve mFadeCurve;
	public void CrossFadeCamera(E_Camera_Type Current,E_Camera_Type Target)
	{
		StartCoroutine(ScreenWipe.use.CrossFade (mCameraList[(int)Current], mCameraList[(int)Target], mFadeTime, mFadeCurve));
	}
}


