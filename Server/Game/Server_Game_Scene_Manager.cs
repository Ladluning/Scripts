using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
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

    public class Server_Game_Scene_Manager : Controller
    {
		[HideInInspector] public List<List<MapNode>> mMapData = new List<List<MapNode>>();
		[HideInInspector] public Vector2 mMapNodeSize = new Vector2(0.5f,0.5f);
		[HideInInspector] public Int2 mMapSize = new Int2(1000,1000);
		[HideInInspector]public int PlayerVisibleRange = 400;

		private Server_Manager mManager;
		private List<Server_Game_User> mPlayerList;
		

		void Awake()
		{
			mManager = Server_Manager.Singleton();
			mPlayerList = mManager.mPlayerList;
		}

		void Update()
		{
			for (int i = 0; i < mPlayerList.Count;i++ )
			{
				for (int j = i+1; j < mPlayerList.Count; j++)
				{
					if (!mPlayerList[j].GetIsChanged() || !mPlayerList[i].GetIsChanged())
						continue;
					
					if ((mPlayerList[j].transform.position - mPlayerList[i].transform.position).sqrMagnitude < PlayerVisibleRange)
					{
						mPlayerList[j].AddVisiblePlayer(mPlayerList[i]);
						mPlayerList[i].AddVisiblePlayer(mPlayerList[j]);
					}
				}
			}
		}

		public void InitMapData(int x,int y)
		{
			ClearMapData ();
			for(int i=0;i<x;i++)
			{
				List<MapNode> tmpList=new List<MapNode>();
				for(int j=0;j<y;j++)
				{
					tmpList.Add(null);
				}
				mMapData.Add(tmpList);
			}
			
			mMapSize = new Int2(x,y);
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
		
		public bool GetPointIsInMap(int x,int y)
		{
			if(x<0||y<0||x>=mMapSize.x||y>=mMapSize.y)
				return false;
			return true;
		}
		
		public bool GetPointIsInMap(Int2 Pos)
		{
			if(Pos.x<0||Pos.y<0||Pos.x>=mMapSize.x||Pos.y>=mMapSize.y)
				return false;
			return true;
		}
		
		public bool GetPointIsInMap(Vector2 Pos)
		{
			Int2 tmpPos = ConvertWorldPosToMapPos(Pos);
			if(tmpPos.x<0||tmpPos.y<0||tmpPos.x>=mMapSize.x||tmpPos.y>=mMapSize.y)
				return false;
			return true;
		}
		
		public Int2 ConvertWorldPosToMapPos(Vector3 TargetPos)
		{
			return new Int2(TargetPos.x/mMapNodeSize.x,TargetPos.z/mMapNodeSize.y);
		}
		
		public Int2 ConvertWorldPosToMapPos(Vector2 TargetPos)
		{
			return new Int2(TargetPos.x/mMapNodeSize.x,TargetPos.y/mMapNodeSize.y);
		}
		
		public Vector3 ConvertMapPosToWorldPos(int x,int y)
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
}
