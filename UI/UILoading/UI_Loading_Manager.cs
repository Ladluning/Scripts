using UnityEngine;
using System.Collections;
public class Struct_Loading_Info
{
	public bool mStopGame;
	public float mAlphaTime;
	public RegistFunction mCallBack;

	public Struct_Loading_Info(bool StopGame,float AlphaTime,RegistFunction CallBack)
	{
		mStopGame = StopGame;
		mAlphaTime = AlphaTime;
		mCallBack = CallBack;
	}
}
public class UI_Loading_Manager : Controller {

	protected Camera mMainCamera;
	protected Material mBlockMaterial;

	private RegistFunction mCallBack;
	private bool mIsAnimation = false;
	private bool mIsStopGame = false;
	private float mTimer = 0;
	private float mTime = 0;
	private float mSpeed = 0;
	void Awake()
	{
		mMainCamera = gameObject.GetComponentInChildren<Camera>();
		mBlockMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
	}

	void OnEnable()
	{
		this.RegistEvent (GameEvent.UIEvent.EVENT_UI_SHOW_LOADING_BLOCK,OnHandleShowLoading);
		this.RegistEvent (GameEvent.UIEvent.EVENT_UI_HIDE_LOADING_BLOCK,OnHandleHideLoading);
	}

	void OnDisable()
	{
		this.UnRegistEvent (GameEvent.UIEvent.EVENT_UI_SHOW_LOADING_BLOCK,OnHandleShowLoading);
		this.UnRegistEvent (GameEvent.UIEvent.EVENT_UI_HIDE_LOADING_BLOCK,OnHandleHideLoading);

	}

	object OnHandleShowLoading(object pSender)
	{
		Struct_Loading_Info tmpInfo = (Struct_Loading_Info)pSender;

		mIsStopGame = tmpInfo.mStopGame;

		if (mIsStopGame) 
		{
				
		}

		if (tmpInfo.mAlphaTime > 0) 
		{
			mIsAnimation = true;
			mTime = tmpInfo.mAlphaTime;
			mTimer = 0;
			mSpeed = 1f/tmpInfo.mAlphaTime;
			mCallBack += tmpInfo.mCallBack;
		}
		else
		{
			mBlockMaterial.SetColor("_TintColor",new Color(0,0,0,1));
		}
		return null;
	}

	object OnHandleHideLoading(object pSender)
	{
		Struct_Loading_Info tmpInfo = (Struct_Loading_Info)pSender;
		mIsStopGame = tmpInfo.mStopGame;
		
		if (tmpInfo.mAlphaTime > 0) 
		{
			mIsAnimation = true;
			mTime = tmpInfo.mAlphaTime;
			mTimer = 0;
			mSpeed = -1f/tmpInfo.mAlphaTime;
			mCallBack += tmpInfo.mCallBack;
		}
		else
		{
			mBlockMaterial.SetColor("_TintColor",new Color(0,0,0,0));
		}
		return null;
	}

	void Update()
	{
		if (!mIsAnimation) 
			return;

		mTimer += Time.deltaTime;
		mBlockMaterial.SetColor("_TintColor",new Color(0,0,0,mSpeed<0?(1+mTimer*mSpeed):(mTimer*mSpeed)));

		if (mTimer > mTime) {
			mIsAnimation = false;

			if(mCallBack!=null)
			{
				mCallBack(null);
			}

			if(!mIsStopGame)
			{

			}
		}
	}
}
