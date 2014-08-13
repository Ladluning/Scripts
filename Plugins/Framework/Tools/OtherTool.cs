using UnityEngine;
using System.Collections;

public class OtherTool : MonoBehaviour 
{
	static public void SetLayer(GameObject pObject,int nLayer)
	{
		for(int i=0;i<pObject.transform.childCount;i++)
		{
			Transform Tmp = pObject.transform.GetChild(i);
			Tmp.gameObject.layer = nLayer;
			SetLayer(Tmp.gameObject,nLayer);
		}
	}
	
	static public Camera GetTargetUICamera(Transform pObject)
	{
		foreach(Camera Tmp in Camera.allCameras)
		{
			if(Tmp.cullingMask==(Tmp.cullingMask|1<<pObject.gameObject.layer))
			{
				return Tmp;
			}
		}
		return null;
	}

	static public Transform GetChildWithName(string Name ,Transform Target)
	{
		//Debug.Log (Target.name);
		if (Target.name == Name)
			return Target;

		for (int i=0; i<Target.childCount; i++) 
		{
			Transform Tmp = GetChildWithName(Name,Target.GetChild(i));
			if(Tmp!=null)
				return Tmp;
		}

		return null;
	}
//	Camera FindCameraInCamera(Transform pObject,int nLayer)
//	{
//		
//	}
}
