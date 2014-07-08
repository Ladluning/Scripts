using UnityEngine;
using System.Collections;

public class Game_Input_Cursor : Controller {

	public UITweener mCursorTween;
	public float mCursorTweenTime;
	public int   mCursorTweenCount;
	public float mNextCursorSpaceTime = 0.12f;
	private Vector3 mNextCursorPos;

	private float mTimer;
	private float mLoseTime;
	private bool IsRun = false;
	private bool IsStart = false;
	void Awake()
	{
		mCursorTween.duration = mCursorTweenTime;

		mLoseTime = mCursorTweenTime * mCursorTweenCount;
		mCursorTween.gameObject.SetActive(false);
	}
	void OnEnable()
	{
		this.RegistListen (GameEvent.FightingEvent.EVENT_FIGHT_CLICK_POS, OnListenClickPos);
	}

	void OnDisable()
	{
		this.UnRegistListen (GameEvent.FightingEvent.EVENT_FIGHT_CLICK_POS, OnListenClickPos);
	}

	object OnListenClickPos(object pSender)
	{
		if (IsStart)
			return null;

		IsRun = true;
		IsStart = true;
		mTimer = 0;
		transform.position = (Vector3)pSender;
		if(!mCursorTween.gameObject.activeSelf)
			mCursorTween.gameObject.SetActive(true);
		mCursorTween.ResetToBeginning ();
		return null;
	}

	void Update()
	{
		//if (!IsRun)
		//	return;


	}

	void LateUpdate()
	{
		UpdateEnd ();
		UpdateStart ();
	}

	void UpdateStart()
	{
		if (!IsStart)
			return;

		if (mTimer+0.033f > mNextCursorSpaceTime) 
		{
			IsStart = false;
		}
	}
	void UpdateEnd()
	{
		if (!IsRun)
			return;
		
		mTimer += Time.deltaTime;
		if (mTimer+0.033f > mLoseTime) 
		{
			IsRun = false;
			mCursorTween.gameObject.SetActive(false);
		}
	}

}
