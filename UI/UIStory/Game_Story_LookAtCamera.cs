using UnityEngine;
using System.Collections;

public class Game_Story_LookAtCamera : MonoBehaviour
{
	Camera cacheMainCamera;
	Transform mMainCameraTrans;
	Transform mTrans;
	// Use this for initialization
	void Start () 
	{
		cacheMainCamera = Camera.main;
		mMainCameraTrans = cacheMainCamera.transform;
		mTrans = transform;

		if(!mTrans.rotation.Equals(mMainCameraTrans.rotation))
		{
			mTrans.rotation = mMainCameraTrans.rotation;
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		/*Vector3 dir = mTrans.position - mMainCameraTrans.position;
		Quaternion lookRot = Quaternion.LookRotation(dir);
		transform.rotation = lookRot;*/
		if(!mTrans.rotation.Equals(mMainCameraTrans.rotation))
		{
			mTrans.rotation = mMainCameraTrans.rotation;
		}

	}

	void OnBecameVisible()
	{
		if(!mTrans.rotation.Equals(mMainCameraTrans.rotation))
		{
			mTrans.rotation = mMainCameraTrans.rotation;
		}
		
	}
}
