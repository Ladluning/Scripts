using UnityEngine;
using System.Collections;

public class Game_Scene_Fog : MonoBehaviour {

	public Struct_Game_Fog mOriginFog;

	void OnEnable()
	{
		Init ();
	}

	void OnDisable()
	{

	}

	void Init()
	{
		if (mOriginFog.IsFog) 
		{
			RenderSettings.fog = true;
			RenderSettings.fogColor = mOriginFog.FogColor;
			RenderSettings.fogDensity = mOriginFog.FogDensity;
			RenderSettings.fogStartDistance = mOriginFog.FogStartPos;
			RenderSettings.fogEndDistance = mOriginFog.FogEndPos;

			if(gameObject.GetComponent<Camera>())
				gameObject.GetComponent<Camera>().farClipPlane = mOriginFog.CameraCullFar;
		}
	}
}
