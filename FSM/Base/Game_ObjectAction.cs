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

		Vector3 moveDirection = (mTargetPos - mCurrentTransform.position).normalized;
		moveDirection.y = 0;

		if (moveDirection.sqrMagnitude < 0.01f)
		{
			IsStartMove = false;
			if(mCallBack!=null)mCallBack(true);
			return;
		}


		moveDirection *= 3f* Time.deltaTime;

		if (RaycastWall (mCurrentTransform.position, moveDirection, 1f * Time.deltaTime)) 
		{
			IsStartMove = false;
			if(mCallBack!=null)mCallBack(false);
			return;
		}

		moveDirection += Physics.gravity * Time.deltaTime;
		mCharacterController.Move(moveDirection);
	}

	bool RaycastWall(Vector3 CurrentPos,Vector3 Director,float MoveDistance)
	{
		Ray tmpRay = new Ray (CurrentPos+Vector3.up, Director);

		if (Physics.Raycast (tmpRay, MoveDistance, 1 << LayerMask.NameToLayer ("SceneCollider"))) 
		{
			return true;
		}
		return false;
	}
}
