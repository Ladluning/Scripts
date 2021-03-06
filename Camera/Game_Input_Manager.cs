using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Input_Manager : Controller {

	public float mFactorMove = 4f;

	public Vector2 mPinchRect = new Vector2(3.5f,15);
	public float   mPinchSpeed = 0.006f;
	public float   mPinchFactor = 4f;

	[HideInInspector]
	public Vector3 mDefaultRotate = new Vector3(44.2f,0,0);
	[HideInInspector]
	public float mDefaultPinch = 8.52f;

	private float   mTargetPinch;
	private float   mCurrentPinch;
	
	public float mSwipeSpeed = 2f;
	public float mFactorSwipe = 3f;
	private float mTargetAngle;
	private float mCurrentAngle;
	private float mDispatcherAngleStop = 90;
	private bool mIsInit = false;
	private bool mIsMakeInput = false;

	protected Transform mCurrentTransform;
	protected Transform mCenterTransform;
	protected Transform mTargetTransform;

	bool mIsStop = false;
	//protected Camera mMainCamera;
	private static Game_Input_Manager m_pInterface;
	public  static Game_Input_Manager Singleton()
	{
		return m_pInterface;
	}

	void OnEnable()
	{
		FingerGestures.OnTap += OnFingerTap;//
		FingerGestures.OnDragBegin += OnDragBegin;
		FingerGestures.OnDragMove += OnDragMove;
		FingerGestures.OnDragEnd += OnDragEnd;
		FingerGestures.OnPinchBegin += OnPinchBegin;
		FingerGestures.OnPinchMove += OnPinch;
		FingerGestures.OnPinchEnd += OnPinchEnd;
		FingerGestures.OnRotationBegin += OnSwipeBegin;
		FingerGestures.OnRotationMove += OnSwipe;
		FingerGestures.OnRotationEnd += OnSwipeEnd;

		this.RegistEvent (GameEvent.InputEvent.EVENT_INPUT_STOP_INPUT,OnHandleStopInput);
		this.RegistEvent (GameEvent.InputEvent.EVENT_INPUT_RESUME_INPUT,OnHandleResumeInput);
	}
	void OnDisable()
	{
		FingerGestures.OnTap -= OnFingerTap;
		FingerGestures.OnDragBegin -= OnDragBegin;
		FingerGestures.OnDragMove -= OnDragMove;
		FingerGestures.OnDragEnd -= OnDragEnd;
		FingerGestures.OnPinchBegin -= OnPinchBegin;
		FingerGestures.OnPinchMove -= OnPinch;
		FingerGestures.OnPinchEnd -= OnPinchEnd;
		FingerGestures.OnRotationBegin -= OnSwipeBegin;
		FingerGestures.OnRotationMove -= OnSwipe;
		FingerGestures.OnRotationEnd -= OnSwipeEnd;

		this.UnRegistEvent (GameEvent.InputEvent.EVENT_INPUT_STOP_INPUT,OnHandleStopInput);
		this.UnRegistEvent (GameEvent.InputEvent.EVENT_INPUT_RESUME_INPUT,OnHandleResumeInput);
	}
	
	void Awake()
	{
		//mMainCamera = gameObject.GetComponent<Camera>();
		m_pInterface = this;
		mCurrentTransform = transform;

		//Reset ();
	}

	public void Reset()
	{
		mIsInit = true;
		mCurrentTransform.eulerAngles = mDefaultRotate;

		mTargetTransform = GameObject.Find ("_Camera_Player_Target").transform;
		mCenterTransform = GameObject.Find ("_Camera_Player_Center").transform;

		Vector3 tmpDirector = -mCurrentTransform.transform.forward*mDefaultPinch;
		
		mCenterTransform.position = mTargetTransform.position;
		mCurrentTransform.position = mCenterTransform.position + tmpDirector;

		Init ();
	}

	
	void Init()
	{
		mCurrentAngle = mCurrentTransform.eulerAngles.y;
		mTargetAngle = mCurrentAngle;
		mTargetPinch = (mCenterTransform.position - mCurrentTransform.position).magnitude;
		mCurrentPinch = mTargetPinch;
	}
	
	void OnFingerTap(Vector2 fingerPos, int tapCount) 
	{
		Debug.Log (UICamera.touchCount);
		if ((UICamera.touchCount>0  && !mIsMakeInput)||mIsStop)
			return;

		Vector3 HitPos;
		GameObject HitObject;
		if((HitObject = GameTools.getGameObjectFromScreenPos ("NPC", fingerPos))!=null)
		{
            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_CLICK_POS, HitObject.transform.position);
			this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_CLICK_NPC, HitObject);
		}
		else if (GameTools.getLayerPosFromScreenPos (out HitPos, "Background", fingerPos)) 
		{
			this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_CLICK_POS,HitPos);
		}
	}

	void OnDragBegin(Vector2 fingerPos, Vector2 startPos)
	{
		Debug.Log (UICamera.touchCount);
		if ((UICamera.touchCount>0  && !mIsMakeInput)||mIsStop)
			return;
		
		MakeInput (true);
	}

	void OnDragMove(Vector2 fingerPos, Vector2 delta)
	{
		if ((UICamera.touchCount>0  && !mIsMakeInput)||mIsStop)
			return;
		OnFingerTap (fingerPos,1);
//
//		Vector3 HitPos;
//		if (GameTools.getLayerPosFromScreenPos (out HitPos, "Background", fingerPos)) 
//		{
//			//this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_CLICK_POS,HitPos);
//		}
	}

	void OnDragEnd(Vector2 fingerPos)
	{
		MakeInput (false);
	}


	void OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
	{
		if ((UICamera.touchCount>0  && !mIsMakeInput)||mIsStop)
			return;
		
		MakeInput (true);
	}
	void OnPinch(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
	{
		if ((UICamera.touchCount>0  && !mIsMakeInput)||mIsStop)
			return;
		
		mTargetPinch += delta*mPinchSpeed;
	}
	void OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
	{
		MakeInput (false);
	}
	void OnSwipeBegin(Vector2 fingerPos1, Vector2 fingerPos2)
	{
		if ((UICamera.touchCount>0  && !mIsMakeInput)||mIsStop)
			return;
		
		MakeInput (true);
	}
	void OnSwipe(Vector2 fingerPos1, Vector2 fingerPos2, float rotationAngleDelta)
	{
		if ((UICamera.touchCount>0  && !mIsMakeInput)||mIsStop)
			return;
		
		mTargetAngle += rotationAngleDelta * mSwipeSpeed;
	}
	void OnSwipeEnd(Vector2 fingerPos1, Vector2 fingerPos2, float totalRotationAngle)
	{
		mTargetAngle = Mathf.RoundToInt(mTargetAngle/mDispatcherAngleStop)*mDispatcherAngleStop;
		MakeInput (false);
	}
	
	void Update()
	{
		if (!mIsInit)
			return;


		UpdateMove ();
		UpdatePinch ();
		UpdateSwipe ();
	}

	void UpdateMove()
	{
		if ((mTargetTransform.position-mCenterTransform.position).sqrMagnitude < 0.001f)
			return;
		
		Vector3 tmpDirector = (mCurrentTransform.position - mCenterTransform.position);
		
		mCenterTransform.position = Vector3.Lerp (mCenterTransform.position,mTargetTransform.position,Time.deltaTime*mFactorMove);
		
		mCurrentTransform.position = mCenterTransform.position + tmpDirector;

	}
	
	void UpdatePinch(){
		if(mTargetPinch>mPinchRect.y)
			mTargetPinch = Mathf.Lerp (mTargetPinch,mPinchRect.y,Time.deltaTime*mPinchFactor);
		else if(mTargetPinch<mPinchRect.x)
			mTargetPinch = Mathf.Lerp (mTargetPinch,mPinchRect.x,Time.deltaTime*mPinchFactor);
		//mTargetPinch = tmpTargetPinch - mMainCamera.orthographicSize;
		
		if (Mathf.Abs(mTargetPinch-mCurrentPinch) < 0.01f)
			return;

		mCurrentPinch = Mathf.Lerp(mCurrentPinch,mTargetPinch,Time.deltaTime*mPinchFactor);
		
		mCurrentTransform.position = mTargetTransform.position+(mCurrentTransform.position-mTargetTransform.position ).normalized*mCurrentPinch;

	}
	
	void UpdateSwipe(){
		
		if (Mathf.Abs(mTargetAngle - mCurrentAngle) < 0.5f)
			return;
		
		float tmpOriginAngle = mCurrentAngle;
		mCurrentAngle = Mathf.Lerp (mCurrentAngle,mTargetAngle,Time.deltaTime*mFactorSwipe);
		//tmpCurrentSwipe *= Time.deltaTime;//Mathf.Lerp (tmpCurrentSwipe,0,Time.deltaTime*mFactorSwipe);
		mCurrentTransform.RotateAround (mTargetTransform.position,Vector3.up,mCurrentAngle-tmpOriginAngle);
		//mCurrentTransform.eulerAngles = new Vector3(mCurrentTransform.eulerAngles.x,mCurrentAngle%360,mCurrentTransform.eulerAngles.z);

	}
	void OnDrawGizmos()
	{
	}
	void MakeInput(bool IsInMake)
	{
		mIsMakeInput = IsInMake;
	}

	object OnHandleStopInput(object pSender)
	{
		mIsStop = true;
		return null;
	}

	object OnHandleResumeInput(object pSender)
	{
		mIsStop = false;
		return null;
	}
}
