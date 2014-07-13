using UnityEngine;
using System.Collections;

public class Game_ObjectBase : Controller {

	//protected TD_ObjectProperty mObjectPreperty;
	//protected Game_Player_Info_Manager mObjectPreperty;
	protected GameObject mCurrentObject;
	protected Transform  mCurrentTransform;

	public virtual void Init()
	{
		//mObjectPreperty = gameObject.GetComponent<TD_ObjectProperty>();
		mCurrentObject = gameObject;
		mCurrentTransform = transform;
	}
}
