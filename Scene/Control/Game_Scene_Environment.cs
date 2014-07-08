using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Scene_Environment : MonoBehaviour {

	public  Texture2D[] mSceneLightMapTexList;
	public  Texture2D[] mSceneLightMapScaleTexList;
	
	private List<LightmapData> mSceneLightMapList = new List<LightmapData>();
	void OnEnable()
	{
		Init ();
	}

	void OnDisable()
	{

	}

	public void Init()
	{
		for (int i=0; i<mSceneLightMapTexList.Length; i++)
		{
			LightmapData tmpData = new LightmapData();
			tmpData.lightmapFar = mSceneLightMapTexList[i];

			if(i<mSceneLightMapScaleTexList.Length)
				tmpData.lightmapNear = mSceneLightMapScaleTexList[i];

			mSceneLightMapList.Add(tmpData);                                          
			
		}
		LightmapSettings.lightmaps = mSceneLightMapList.ToArray();
	}

	public void Clear()
	{
		LightmapSettings.lightmaps = null;

	}
}
