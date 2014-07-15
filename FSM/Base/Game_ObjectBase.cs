using UnityEngine;
using System.Collections;

public class Game_ObjectBase : Controller {

	protected GameObject mCurrentObject;
	protected Transform  mCurrentTransform;

	public virtual void Init()
	{
		mCurrentObject = gameObject;
		mCurrentTransform = transform;
	}
}
