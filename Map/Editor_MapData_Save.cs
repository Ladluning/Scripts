using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;
using System.Runtime.InteropServices;
[System.Serializable]
public class SaveNode
{
	public int Value;
	public short Map_X;
	public short Map_Y;
	public float WorldPosX;
	public float WorldPosY;
	public float WorldPosZ;
}
public class Editor_MapData_Save : MonoBehaviour {

	[System.Serializable]
	public class DataInfo
	{
		public byte MapID;
		public uint DateLength;
		public short MapOffectX;
		public short MapOffectY;
		public short MapSizeX;
		public short MapSizeY;
		public SaveNode[] request;
	}

	static public bool Save(string Path,byte MapID,uint DateLength,short MapOffectX,short MapOffectY,short MapSizeX,short MapSizeY,SaveNode[] request)
	{
		DataInfo tmpFileInfo = new DataInfo ();
		tmpFileInfo.MapID = MapID;
		tmpFileInfo.DateLength = DateLength;
		tmpFileInfo.MapOffectX = MapOffectX;
		tmpFileInfo.MapOffectY = MapOffectY;
		tmpFileInfo.MapSizeX = MapSizeX;
		tmpFileInfo.MapSizeY = MapSizeY;
		tmpFileInfo.request = request;

		byte[] tmpDate = SerializeBinary (tmpFileInfo);

		FileStream fs = new FileStream(Path,FileMode.Create,FileAccess.ReadWrite);
		if (fs == null)
		{
			Debug.LogError("Cant Open File:"+Path);
			return false;
		}
		fs.Write (tmpDate,0,tmpDate.Length);
		//StreamWriter sw = new StreamWriter(fs);
		//sw.Write(tmpDate);
		//sw.Close();
		fs.Close();
		Debug.Log ("Save Success");
		return true;
	}
	static public bool Load(string Path)
	{		
		FileStream fs = new FileStream(Path,FileMode.Open);
		if (fs == null)
		{
			Debug.LogError("Cant Open File:"+Path);
			return false;
		}
		byte[] date = new byte[fs.Length];
		fs.Read (date,0,(int)fs.Length);

		Debug.Log (fs.Length);
		DeserializeBinary (date);

		fs.Close();

		Debug.Log ("Load Success");
		return true;
	}
	static public byte[] SerializeBinary(DataInfo  request)    
	{  
		ByteArray newArray = new ByteArray ();
		newArray.Put (request.MapID);
		newArray.Put (request.DateLength);
		newArray.Put (request.MapOffectX);
		newArray.Put (request.MapOffectY);
		newArray.Put (request.MapSizeX);
		newArray.Put (request.MapSizeY);
		for (int i=0; i<request.request.Length; i++) 
		{
			newArray.Put (request.request [i].Value);
			newArray.Put (request.request [i].Map_X);
			newArray.Put (request.request [i].Map_Y);
			newArray.Put (request.request [i].WorldPosX);
			newArray.Put (request.request [i].WorldPosY);
			newArray.Put (request.request [i].WorldPosZ);
		}

		Debug.Log (newArray.GetData ().Length);
		return newArray.GetData ();
	} 

	static public void DeserializeBinary(byte[] buf) 
	{ 
		ByteArray newArray = new ByteArray (buf);

		DataInfo tmpInfo = new DataInfo ();
		newArray.Get_ (out tmpInfo.MapID);
		newArray.Get_ (out tmpInfo.DateLength);
		newArray.Get_ (out tmpInfo.MapOffectX);
		newArray.Get_ (out tmpInfo.MapOffectY);
		newArray.Get_ (out tmpInfo.MapSizeX);
		newArray.Get_ (out tmpInfo.MapSizeY);
		Game_MapData_Manager.mMapOriOffect = new Int2 ((int)tmpInfo.MapOffectX, (int)tmpInfo.MapOffectY);
		Game_MapData_Manager.Singleton().InitMapData ((int)tmpInfo.MapSizeX,(int)tmpInfo.MapSizeY);
		//Debug.Log (tmpInfo.MapID+" "+tmpInfo.MapSizeX);
		for(int i=0;i<tmpInfo.DateLength;i++)
		{
			MapNode tmpNode = new MapNode();
			newArray.Get_(out tmpNode.Value);
			short X;
			short Y;
			newArray.Get_(out X);
			newArray.Get_(out Y);
			tmpNode.MapPos = new Int2((int)X,(int)Y);
			float A;
			float B;
			float C;
			newArray.Get_(out A);
			newArray.Get_(out B);
			newArray.Get_(out C);
			tmpNode.WorldPos = new Vector3(A,B,C);
			Game_MapData_Manager.Singleton().InsertMapData((int)X,(int)Y,tmpNode);
		}
	} 

//	void OnGUI()
//	{
//		if (GUI.Button (new Rect (10, 10, 80, 40), "Save")) 
//		{
//			List<SaveNode> tmpList = new List<SaveNode>();
//			for(int i=0;i<100;i++)
//			{
//				SaveNode tmpNode = new SaveNode();
//				tmpNode.H_X = (short)i;
//				tmpNode.H_Y = (short)i;
//				tmpNode.E = i;
//				tmpNode.WorldPosX = i;
//				tmpNode.WorldPosY = 1;
//				tmpNode.WorldPosZ = i;
//				tmpList.Add(tmpNode);
//			}
//			Editor_MapData_Save.Save(Application.dataPath+"/Resources/MapDate/"+GameObject.FindObjectOfType(typeof(Game_Scene_Environment_Init)).name+".date",
//			                         1,(uint)tmpList.Count,0,0,10,10,tmpList.ToArray());
//		}
//
//		if (GUI.Button (new Rect (10, 60, 80, 40), "Load")) 
//		{
//			Game_MapData_Manager.Singleton().InitMapData();
//			Load(Application.dataPath+"/Resources/MapDate/"+GameObject.FindObjectOfType(typeof(Game_Scene_Environment_Init)).name+".date");
//		}
//	}
}
