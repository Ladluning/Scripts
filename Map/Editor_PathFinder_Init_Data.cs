
#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;



public class Editor_PathFinder_Init_Data : EditorWindow
{
	[MenuItem("Tools/InitMapData")]
	static void Init()
	{
		Editor_PathFinder_Init_Data window = (Editor_PathFinder_Init_Data)EditorWindow.GetWindow(typeof(Editor_PathFinder_Init_Data));
		window.Show();
	}

	GameObject TargetStartPoint;
	string TargetName;
	int BackgroundLayer;
	int ColliderLayer;
	int Count = 0;
	byte[,] MapMask;
	int MaxCount = 15000;
	List<SaveNode> mMapData = new List<SaveNode>();
	void OnGUI()//
	{
		GUILayout.BeginVertical();

		TargetStartPoint = EditorGUILayout.ObjectField("Target",TargetStartPoint, typeof(GameObject)) as GameObject;
		TargetName = EditorGUILayout.TextField ("FileName",TargetName);

		MaxCount = EditorGUILayout.IntField ("Count",MaxCount);

		if (GUILayout.Button ("Lighting")) 
		{
			GameObject.FindObjectOfType<Game_Scene_Environment>().Init();
		}
		if(GUILayout.Button("StartInit"))
		{
			int beginTime = DateTime.Now.Millisecond+DateTime.Now.Second*1000;
			BackgroundLayer = LayerMask.NameToLayer("Background");
			ColliderLayer = LayerMask.NameToLayer("SceneCollider");
			Count = 0;

			mMapData.Clear();
			MapMask = new byte[Game_MapData_Manager.mMapSize.x,Game_MapData_Manager.mMapSize.y];
			Int2 tmpStart = Game_MapData_Manager.ConvertWorldPosToMapPos(TargetStartPoint.transform.position);
			RaycastHit(tmpStart.x,tmpStart.y);

			Editor_MapData_Save.Save(Application.dataPath+"/Resources/MapDate/"+TargetName+".date",
			                         0,(uint)mMapData.Count,0,0,1000,1000,mMapData.ToArray());

			Debug.Log ("Init Over"+Count+" "+(DateTime.Now.Millisecond+DateTime.Now.Second*1000-beginTime));
		}
		if (GUILayout.Button ("Load")) 
		{
			Game_MapData_Manager.Singleton().InitMapData();
			Editor_MapData_Save.Load(Application.dataPath+"/Resources/MapDate/"+TargetName+".date");
		}
		GUILayout.EndVertical();
	}
//	void OnLostFocus() 
//	{
//		if(TargetStartPoint!=null)
//			DestroyImmediate(TargetStartPoint);
//	}
	Vector3 TargetHitPoint;

	void RaycastHit(int x,int y)
	{
		//yield return new WaitForSeconds(0.1f);

		long tmpLong = (long)(x) * (long)(int.MaxValue) + (long)(y);

		if (Count>MaxCount||!Game_MapData_Manager.GetPointIsInMap(x,y)||MapMask[x,y]==1)
			return;
		++Count;
		MapMask[x,y] = 1;

		if(!GetIsHitTargetWithTag(Game_MapData_Manager.ConvertMapPosToWorldPos(x,y),Vector3.down,out TargetHitPoint,1<<BackgroundLayer|1<<ColliderLayer))
		{
			return;
		}
		SaveNode tmpNode = new SaveNode ();
		tmpNode.H_X = (short)x;
		tmpNode.H_Y = (short)y;
		tmpNode.WorldPosX = TargetHitPoint.x;
		tmpNode.WorldPosY = TargetHitPoint.y;
		tmpNode.WorldPosZ = TargetHitPoint.z;
		//mMapData
		mMapData.Add (tmpNode);
		//Debug.Log (x+" "+y);
		RaycastHit(x+1,y);
		RaycastHit(x,y+1);
		RaycastHit(x,y-1);
		RaycastHit(x-1,y);
	}

	
	public bool GetIsHitTargetWithTag(Vector3 StartPos,Vector3 Director,out Vector3 HitPos,int nLayerMask)
	{
		RaycastHit hit;
		if (Physics.Raycast (StartPos+Vector3.up*100, Director, out hit, Mathf.Infinity,nLayerMask)) 
		{
			HitPos = hit.point;
			if(hit.collider.gameObject.layer == BackgroundLayer)
				return true;	
			else
				return false;
		}
		HitPos = Vector3.zero;
		return false;
	}


}
#endif