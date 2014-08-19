using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{
	Camera cacheMainCamera;
	Transform mMainCameraTrans;
	Transform mTrans;
	// Use this for initialization
	void Start () 
	{
		foreach (Camera tmp in Camera.allCameras) 
		{
			if((tmp.cullingMask&gameObject.layer)!=0)
			{
				Debug.Log ("Init Bubble Camera Success:"+tmp.name);
				cacheMainCamera = Camera.main;
				break;
			}
		}

		if(cacheMainCamera== null)
			Debug.LogError("!Init Bubble Camera Error:"+gameObject.name);

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
