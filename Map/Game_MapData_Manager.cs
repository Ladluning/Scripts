using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapNode
{
	public bool IsClose;
	public bool IsOpen;
	public byte D;
	public int A;
	public int E;
	public Int2 H;
	public Vector3 WorldPos;
}
public class Game_MapData_Manager : MonoBehaviour {
	public List<List<MapNode>> mMapData = new List<List<MapNode>>();
	static public Int2 mMapSize = new Int2(1000,1000);
	static public Vector2 mMapNodeSize = new Vector2(0.5f,0.5f);
	static private Game_MapData_Manager m_pInterface;
	static public Game_MapData_Manager Singleton()
	{
		return m_pInterface;
	}

	Game_MapData_Manager()
	{
		m_pInterface = this;//
	}

	void Awake()
	{
		InitMapData();
		Editor_MapData_Save.Load(Application.dataPath+"/Resources/MapDate/"+gameObject.name+".date");
	}

	public void InitMapData()
	{
		ClearMapData ();
		for(int i=0;i<mMapSize.x;i++)
		{
			List<MapNode> tmpList=new List<MapNode>();
			for(int j=0;j<mMapSize.y;j++)
			{
				tmpList.Add(null);
			}
			mMapData.Add(tmpList);
		}

		mMapSize = new Int2(mMapSize.x,mMapSize.y);
	}

	public void ClearMapData()
	{
		mMapData.Clear();
	}

	public void InsertMapData(MapNode TargetNode)
	{
		//Debug.Log (TargetNode);
		if(TargetNode==null||TargetNode.H.x<0||TargetNode.H.y<0||TargetNode.H.x>=mMapSize.x||TargetNode.H.y>=mMapSize.y)
			return;
		//Debug.Log (TargetNode.WorldPos);
		mMapData[TargetNode.H.y][TargetNode.H.x] = TargetNode;
	}

	public MapNode GetMapData(int x,int y)
	{
		if(x<0||y<0||x>=mMapSize.x||y>=mMapSize.y)
			return null;

		return mMapData[y][x];
	}

	public static bool GetPointIsInMap(int x,int y)
	{
		if(x<0||y<0||x>=mMapSize.x||y>=mMapSize.y)
			return false;
		return true;
	}

	public static bool GetPointIsInMap(Int2 Pos)
	{
		if(Pos.x<0||Pos.y<0||Pos.x>=mMapSize.x||Pos.y>=mMapSize.y)
			return false;
		return true;
	}

	public static bool GetPointIsInMap(Vector2 Pos)
	{
		Int2 tmpPos = ConvertWorldPosToMapPos(Pos);
		if(tmpPos.x<0||tmpPos.y<0||tmpPos.x>=mMapSize.x||tmpPos.y>=mMapSize.y)
			return false;
		return true;
	}

	public static Int2 ConvertWorldPosToMapPos(Vector3 TargetPos)
	{
		return new Int2(TargetPos.x/mMapNodeSize.x,TargetPos.z/mMapNodeSize.y);
	}

	public static Int2 ConvertWorldPosToMapPos(Vector2 TargetPos)
	{
		return new Int2(TargetPos.x/mMapNodeSize.x,TargetPos.y/mMapNodeSize.y);
	}

	public static Vector3 ConvertMapPosToWorldPos(int x,int y)
	{
		return new Vector3 (x*mMapNodeSize.x,0,y*mMapNodeSize.y);
	}

	void OnDrawGizmos()
	{
		if (mMapData==null||mMapData.Count <= 0)
			return;

		for (int i=0; i<mMapSize.x; ++i)
			for (int j=0; j<mMapSize.y; j++) 
		{
			if(mMapData[i][j]!=null)
				Gizmos.DrawWireCube(mMapData[i][j].WorldPos,Vector3.one*0.3f);
		
		}
	}
}
