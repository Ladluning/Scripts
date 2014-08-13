using UnityEngine;
using System.Collections;

public enum E_State_MainPlayer
{
    Idle = 0,
    Move,
}

public class Game_FSM_MainPlayer_Controller: Game_FSM_Controller {

	// Use this for initialization
	GameObject mMainCharacterMesh;
	void OnEnable()
	{
		this.RegistEvent (GameEvent.FightingEvent.EVENT_FIGHT_CLICK_POS, OnHandleClickPos);
	}
	
	void OnDisable()
	{
		this.UnRegistEvent (GameEvent.FightingEvent.EVENT_FIGHT_CLICK_POS,OnHandleClickPos);
	}
	protected override void Awake () 
	{
		base.Awake();
		//InitMainPlayer ("Blade_Girl_M_Base_All",new Vector3(185,7,88.5f),new Vector3(0,-95,0));
	}

	public void InitMainPlayer(string MeshID,Vector3 Pos,Vector3 Rotate)
	{
		if (mMainCharacterMesh != null && mMainCharacterMesh.name != MeshID) 
		{
			Destroy(mMainCharacterMesh);
			mMainCharacterMesh = null;
		}
		if (mMainCharacterMesh == null) 
		{
			mMainCharacterMesh = Instantiate(Resources.Load ("ActorMesh/"+MeshID,typeof(GameObject))) as GameObject;
		}

		mMainCharacterMesh.name = MeshID;
		mMainCharacterMesh.transform.parent = transform;
		mMainCharacterMesh.transform.localPosition = Vector3.zero;
		mMainCharacterMesh.transform.localRotation = Quaternion.identity;
		mMainCharacterMesh.transform.localScale = Vector3.one;

		Transform TmpHeadCameraPos = OtherTool.GetChildWithName ("Bip001 Neck", transform);//TmpMesh.transform.FindChild ("Bip001 Neck");

		if (TmpHeadCameraPos != null) 
		{
			GameObject TmpCamera = Instantiate(Resources.Load ("Tools/MainGame/HeadCamera",typeof(GameObject))) as GameObject;
			TmpCamera.transform.parent = TmpHeadCameraPos;
			TmpCamera.transform.localPosition = Vector3.zero;
			TmpCamera.transform.localRotation = Quaternion.identity;
			TmpCamera.transform.localScale = Vector3.one;
		}

		mStateMap.Add((int)E_State_MainPlayer.Idle,new Game_FSM_MainPlayer_State_Idle(this));
		mStateMap [(int)E_State_MainPlayer.Idle].AddTranslate ((int)E_State_MainPlayer.Move);
		
		mStateMap.Add((int)E_State_MainPlayer.Move,new Game_FSM_MainPlayer_State_Move(this));
		mStateMap [(int)E_State_MainPlayer.Move].AddTranslate ((int)E_State_MainPlayer.Idle);
		
		
		this.InitState();
		this.StartState((int)E_State_MainPlayer.Idle);

		transform.localEulerAngles = Rotate;
		transform.position = Pos;
		mAIStop = false;
	}
	void Start () 
	{
		
	}
	
	object OnHandleClickPos(object pSender)
	{
		this.SwitchState ((int)E_State_MainPlayer.Move);
		((Game_FSM_MainPlayer_State_Move)mStateMap [(int)E_State_MainPlayer.Move]).SetTargetPos ((Vector3)pSender);

		return null;
	}
	
	
	protected override void Update ()
	{
		base.Update ();
	}
}
