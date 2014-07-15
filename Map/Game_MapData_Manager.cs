using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapNode
{
	public bool IsClose;
	public bool IsOpen;
	public int Value;
	public int g;
	public int f;
	public Int2 MapPos;
	public MapNode parent;
	public Vector3 WorldPos;
}
public class Game_MapData_Manager : MonoBehaviour {
	public List<List<MapNode>> mMapData = new List<List<MapNode>>();
	static public Int2 mMapSize = new Int2(1000,1000);
	static public Vector2 mMapNodeSize = new Vector2(0.5f,0.5f);
	static public Int2 mMapOriOffect = new Int2(0, 0);
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
		//InitMapData();
		Editor_MapData_Save.Load(Application.dataPath+"/Resources/MapDate/"+gameObject.name+".date");
	}

	public void InitMapData(int x, int y)
	{
		ClearMapData();
		for (int i = 0; i < x; i++)
		{
			List<MapNode> tmpList = new List<MapNode>();
			for (int j = 0; j < y; j++)
			{
				tmpList.Add(null);
			}
			mMapData.Add(tmpList);
		}

		//Debug.Log ((x + mMapOriOffect.x)+" "+(y + mMapOriOffect.y)+" "+x+" "+y+" "+mMapOriOffect.x+" "+mMapOriOffect.y);
		mMapSize = new Int2(x + mMapOriOffect.x,y + mMapOriOffect.y);
	}
	
	public void ClearMapData()
	{
		mMapData.Clear();
	}
	
	public void InsertMapData(int x,int y,MapNode TargetNode)
	{
		if (TargetNode == null || x < 0 || y < 0 || x >= mMapSize.x || y >= mMapSize.y)
			return;
		//Debug.Log (y+" "+x);
		mMapData[x - mMapOriOffect.x][y - mMapOriOffect.y] = TargetNode;
	}

	public MapNode GetMapData(int x, int y)
	{
		if (x < mMapOriOffect.x || y < mMapOriOffect.y || x >= mMapSize.x || y >= mMapSize.y)
			return null;
		
		return mMapData[x - mMapOriOffect.x][y - mMapOriOffect.y];
	}
	
	static public bool GetPointIsInMap(int x, int y)
	{
		if (x < mMapOriOffect.x || y < mMapOriOffect.y || x >= mMapSize.x || y >= mMapSize.y)
			return false;
		return true;
	}
	
	static public bool GetPointIsInMap(Int2 Pos)
	{
		if (Pos.x < mMapOriOffect.x || Pos.y < mMapOriOffect.y || Pos.x >= mMapSize.x || Pos.y >= mMapSize.y)
			return false;
		return true;
	}
	
	static public bool GetPointIsInMap(Vector2 Pos)
	{
		Int2 tmpPos = ConvertWorldPosToMapPos(Pos);
		if (tmpPos.x < mMapOriOffect.x || tmpPos.y < mMapOriOffect.y || tmpPos.x >= mMapSize.x || tmpPos.y >= mMapSize.y)
			return false;
		return true;
	}
	
	static public Int2 ConvertWorldPosToMapPos(Vector3 TargetPos)
	{
		return new Int2(TargetPos.x / mMapNodeSize.x, TargetPos.z / mMapNodeSize.y);
	}
	
	static public Int2 ConvertWorldPosToMapPos(Vector2 TargetPos)
	{
		return new Int2(TargetPos.x / mMapNodeSize.x, TargetPos.y / mMapNodeSize.y);
	}
	
	static public Vector3 ConvertMapPosToWorldPos(int x, int y)
	{
		return new Vector3(x * mMapNodeSize.x, 0, y * mMapNodeSize.y);
	}
	
	void OnDrawGizmos()
	{
		if (mMapData == null || mMapData.Count <= 0)
			return;

		for (int i = 0; i < mMapSize.x-mMapOriOffect.x; ++i)
			for (int j = 0; j < mMapSize.y-mMapOriOffect.y; j++)
		{
			if (mMapData[i][j] != null)
				Gizmos.DrawWireCube(mMapData[i][j].WorldPos, Vector3.one * 0.3f);

		}
	}
}
