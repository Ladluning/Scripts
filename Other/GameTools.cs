using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using System.IO;


public class GameTools
{
	public static GameObject getGameObjectFromScreenPos( string layerName, Vector3 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay (screenPos);
		LayerMask mask = 1 << LayerMask.NameToLayer (layerName);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, mask)) {
			return hit.collider.gameObject;
		} else {
			return null;	
		}
	}
	public static bool getLayerPosFromScreenPos (out Vector3 layerPos, string layerName, Vector3 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay (screenPos);
		LayerMask mask = 1 << LayerMask.NameToLayer (layerName);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, mask)) {
			layerPos = hit.point;
			return true;
		} else {
			Debug.LogError ("Could't hit " + layerName + " Layer");
			layerPos = Vector3.zero;
			return false;	
		}
	}

	public static bool getLayerPosFromScreenViewPort (out Vector3 layerPos, string layerName, Vector3 viewPort)
	{
		Ray ray = Camera.main.ViewportPointToRay(viewPort);
		LayerMask mask = 1 << LayerMask.NameToLayer (layerName);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, mask)) {
			layerPos = hit.point;
			return true;
		} else {
			Debug.LogError ("Could't hit " + layerName + " Layer");
			layerPos = Vector3.zero;
			return false;	
		}
	}

	// 在某个范围内随机得到一些点
	public static Vector2 GetRamdomPointInRange(Vector3 srcPoint, Rect vRect)
	{
		Vector2 vCur = new Vector2(srcPoint.x,srcPoint.z);
		Vector2 Dis;
		do
		{
			float minX = srcPoint.x - vRect.width / 2;
			float minY = srcPoint.z - vRect.height / 2;
			float maxX = srcPoint.x + vRect.width / 2;
			float maxY = srcPoint.z + vRect.height / 2;
			float disX = UnityEngine.Random.Range(minX, maxX);
			float disY = UnityEngine.Random.Range(minY, maxY);
			Dis = new Vector2(disX, disY);
			
		}
		while (Vector2.Distance(Dis,vCur) < (vRect.width / 2));

		return Dis;

	}
	
	//辅助方法， 从预设中创建GameObject对象,预设名字要包含路径
	public static GameObject CreateGameObjectFromPrefab (string prefabName)
	{
		UnityEngine.Object prefeb = Resources.Load (prefabName);
		if (prefeb == null) {
			return null;
		}
		
		GameObject obj = (GameObject)UnityEngine.Object.Instantiate (prefeb);
		if (null == obj) {
			return null;
		}
		
		return obj;
	}

	
	//角度转弧度
	public static float RadiansToDegrees (float angle)
	{
		return angle * 57.29577951f;
	}
	
	//计算2点之间的夹角
	public static float CalculateAngle (Vector3 v1, Vector3 v2)
	{
		// 计算旋转角度
		// 朝向
		Vector3 oriVec = v1 - v2;
    
		// 正切值
		
		float radian = Mathf.Atan2 (oriVec.z, oriVec.x);
		// 弧度转角度
		float angle = RadiansToDegrees (radian);
    
		// 取正角度（－10度相当于350度）
		if (angle < 0) {
			angle += 360;
		}
    
		return angle;
	}

	public static void PointRotate(Vector3 center, ref Vector3 p1, double angle)  
	{  
   		 double x1 = (p1.x - center.x) * Math.Cos(angle) + (p1.z - center.z) * Math.Sin(angle) + center.x;  
   		 double y1 = -(p1.x - center.x) * Math.Sin(angle) + (p1.z - center.z) * Math.Cos(angle) + center.z;  
   		 p1.x = (float)x1;  
   		 p1.z = (float)y1;  
	} 

}


public class GameObjectHelper
{
	
	public static Transform FindChildInHierarchy (Transform parent, string name)
	{
		if (null == parent) {
			return null;
		}
		
		for (int i = 0; i < parent.childCount; i++) {
			Transform childTrans = parent.GetChild (i);
			if (childTrans.name == name) {
				return childTrans;
			} else {
				Transform t = FindChildInHierarchy (childTrans, name);
				if (t != null) {
					return t;
				}
			}
		}
		
		return null;
	}

	
}


