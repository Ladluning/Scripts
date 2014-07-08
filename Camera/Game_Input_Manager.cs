using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Input_Manager : Controller {

	public float mFactorMove = 4f;

	public Vector2 mPinchRect = new Vector2(3.5f,15);
	public float   mPinchSpeed = 0.006f;
	public float   mPinchFactor = 4f;
	private float   mTargetPinch;
	private float   mCurrentPinch;
	
	public float mSwipeSpeed = 2f;
	public float mFactorSwipe = 3f;
	private float mTargetAngle;
	private float mCurrentAngle;
	private float mDispatcherAngleStop = 90;
	
	private bool mIsMakeInput = false;

	protected Transform mCenterTransform;
	protected Transform mTargetTransform;
	protected Camera mMainCamera;
	void OnEnable()
	{
		FingerGestures.OnTap += OnFingerTap;
		FingerGestures.OnDragBegin += OnDragBegin;
		FingerGestures.OnDragMove += OnDragMove;
		FingerGestures.OnDragEnd += OnDragEnd;
		FingerGestures.OnPinchBegin += OnPinchBegin;
		FingerGestures.OnPinchMove += OnPinch;
		FingerGestures.OnPinchEnd += OnPinchEnd;
		FingerGestures.OnRotationBegin += OnSwipeBegin;
		FingerGestures.OnRotationMove += OnSwipe;
		FingerGestures.OnRotationEnd += OnSwipeEnd;
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

	}
	
	void Awake()
	{
		mMainCamera = gameObject.GetComponent<Camera>();
		

		Reset ();
	}

	public void Reset()
	{
		mTargetTransform = GameObject.Find ("_Camera_Player_Target").transform;
		mCenterTransform = GameObject.Find ("_Camera_Player_Center").transform;
		
		Vector3 HitPos;
		GameTools.getLayerPosFromScreenViewPort (out HitPos, "Background", new Vector2 (0.5f, 0.5f));
		mCenterTransform.position = HitPos;

		Vector3 tmpDirector = (mMainCamera.transform.position - mCenterTransform.position);
		
		mCenterTransform.position = mTargetTransform.position;
		
		mMainCamera.transform.position = mCenterTransform.position + tmpDirector;

		Init ();
	}

	
	void Init()
	{
		mCurrentAngle = mMainCamera.transform.eulerAngles.y;
		mTargetAngle = mCurrentAngle;
		mTargetPinch = (mCenterTransform.position - mMainCamera.transform.position).magnitude;
		mCurrentPinch = mTargetPinch;
	}
	
	void OnFingerTap(Vector2 fingerPos, int tapCount) 
	{
		Vector3 HitPos;
		GameObject HitObject;
		if((HitObject = GameTools.getGameObjectFromScreenPos ("NPC", fingerPos))!=null)
		{
			//this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_CLICK_POS,HitPos);
			//this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_CLICK_NPC,HitObject);
		}
		else if (GameTools.getLayerPosFromScreenPos (out HitPos, "Background", fingerPos)) 
		{
			this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_CLICK_POS,HitPos);
		}
	}

	void OnDragBegin(Vector2 fingerPos, Vector2 startPos)
	{
		if ((UICamera.touchCount>0 && !mIsMakeInput))
			return;
		
		MakeInput (true);
	}

	void OnDragMove(Vector2 fingerPos, Vector2 delta)
	{
		if ((UICamera.touchCount>0  || !mIsMakeInput))
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
		if ((UICamera.touchCount>0  && !mIsMakeInput))
			return;
		
		MakeInput (true);
	}
	void OnPinch(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
	{
		if ((UICamera.touchCount>0  && !mIsMakeInput))
			return;
		
		mTargetPinch += delta*mPinchSpeed;
	}
	void OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
	{
		MakeInput (false);
	}
	void OnSwipeBegin(Vector2 fingerPos1, Vector2 fingerPos2)
	{
		if ((UICamera.touchCount>0  && !mIsMakeInput))
			return;
		
		MakeInput (true);
	}
	void OnSwipe(Vector2 fingerPos1, Vector2 fingerPos2, float rotationAngleDelta)
	{
		if ((UICamera.touchCount>0  && !mIsMakeInput))
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
		UpdateMove ();
		UpdatePinch ();
		UpdateSwipe ();



	}

	void UpdateMove()
	{
		if ((mTargetTransform.position-mCenterTransform.position).sqrMagnitude < 0.001f)
			return;
		
		Vector3 tmpDirector = (mMainCamera.transform.position - mCenterTransform.position);
		
		mCenterTransform.position = Vector3.Lerp (mCenterTransform.position,mTargetTransform.position,Time.deltaTime*mFactorMove);
		
		mMainCamera.transform.position = mCenterTransform.position + tmpDirector;

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
		
		mMainCamera.transform.position = mTargetTransform.position+(mMainCamera.transform.position-mTargetTransform.position ).normalized*mCurrentPinch;

	}
	
	void UpdateSwipe(){
		
		if (Mathf.Abs(mTargetAngle - mCurrentAngle) < 0.5f)
			return;
		
		float tmpOriginAngle = mCurrentAngle;
		mCurrentAngle = Mathf.Lerp (mCurrentAngle,mTargetAngle,Time.deltaTime*mFactorSwipe);
		//tmpCurrentSwipe *= Time.deltaTime;//Mathf.Lerp (tmpCurrentSwipe,0,Time.deltaTime*mFactorSwipe);
		mMainCamera.transform.RotateAround (mTargetTransform.position,Vector3.up,mCurrentAngle-tmpOriginAngle);
		//mMainCamera.transform.eulerAngles = new Vector3(mMainCamera.transform.eulerAngles.x,mCurrentAngle%360,mMainCamera.transform.eulerAngles.z);

	}
	void OnDrawGizmos()
	{
	}
	void MakeInput(bool IsInMake)
	{
		mIsMakeInput = IsInMake;
	}

}
