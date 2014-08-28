using UnityEngine;
using System.Collections;

public class Game_ObjectAction : Game_ObjectBase 
{
	private Vector3 mTargetPos;
	private RegistFunction mCallBack;
	private bool IsStartMove;
	private CharacterController mCharacterController;
	void Awake()
	{
		mCharacterController = gameObject.GetComponent<CharacterController> ();
	}

	public override void Init()
	{
		base.Init ();
		IsStartMove = false;
		mCallBack = null;
	}

	public void MoveToTarget(Vector3 TargetPos,RegistFunction CallBack)
	{
		mTargetPos = TargetPos;
		mCallBack = CallBack;

		mCurrentTransform.LookAt (new Vector3(TargetPos.x,mCurrentTransform.position.y,TargetPos.z));
		IsStartMove = true;
	}

	void OnEnable()
	{
		IsStartMove = false;
	}

	void OnDisable()
	{
		IsStartMove = false;
	}

	void Update()
	{
		if (!IsStartMove)
			return;

		Vector3 moveDirection = (mTargetPos - mCurrentTransform.position);

		if (moveDirection.sqrMagnitude < 0.01f) 
		{
			IsStartMove = false;
			if(mCallBack!=null)mCallBack(true);
			return;	
		}

		moveDirection = moveDirection.normalized;
		moveDirection.y = 0;
		moveDirection *= 3f* Time.deltaTime;
		moveDirection += Physics.gravity;// * Time.deltaTime;

		Vector3 tmpLast = mCharacterController.transform.localPosition;
		mCharacterController.Move (moveDirection);

		if ((mCharacterController.transform.localPosition-tmpLast).sqrMagnitude<0.00001f) 
		{
			IsStartMove = false;
			if(mCallBack!=null)mCallBack(false);
			return;
		}
	}

	bool RaycastWall(Vector3 CurrentPos,Vector3 Director,float MoveDistance)
	{
		Ray tmpRay = new Ray (CurrentPos+Vector3.up, Director);

		if (Physics.Raycast (tmpRay, MoveDistance*2+mCharacterController.radius, 1 << LayerMask.NameToLayer ("SceneCollider"))) 
		{
			return true;
		}
		return false;
	}
}
