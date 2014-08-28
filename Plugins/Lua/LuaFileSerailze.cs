#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;

public class LuaFileSerailze : MonoBehaviour {

	[MenuItem("Assets/Serailze File Format")]    
	static void SerailzeFile () 
	{             
		TextAsset TargetAsset = Selection.activeObject as TextAsset;
		if (TargetAsset == null) 
		{
			Debug.LogError("Requair Text Format!!!");
			return;
		}
		//Debug.Log (TargetAsset);
		Stream tmp = new FileStream (UnityEditor.AssetDatabase.GetAssetPath(Selection.activeObject), FileMode.Create, FileAccess.ReadWrite);
		byte[] tmpArray = Encoding.UTF8.GetBytes (TargetAsset.ToString());//Encoding.GetEncoding ("gb2312").GetBytes (TargetAsset.ToString());
		tmp.Write(tmpArray, 0,tmpArray.Length); 
		tmp.Close ();
		Debug.Log ("Serailze Success");
	}  
}
#endif
