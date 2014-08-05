using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
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

    public class Server_Game_Scene_Map : Controller
    {
        public string mSceneID;
        [HideInInspector]
        public List<List<MapNode>> mMapData = new List<List<MapNode>>();
        [HideInInspector]
        public Vector2 mMapNodeSize = new Vector2(0.5f, 0.5f);
        [HideInInspector]
        public Int2 mMapOriOffect = new Int2(0, 0);
        [HideInInspector]
        public Int2 mMapSize = new Int2(0, 0);
        [HideInInspector]
        public int PlayerVisibleRange = 400;

        public virtual void Init()
        {
            Server_Data_Manager.Singleton().LoadMapDataWithID(mSceneID);
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

            mMapSize = new Int2(x + mMapOriOffect.x, y + mMapOriOffect.y);
        }

        public void ClearMapData()
        {
            mMapData.Clear();
        }

        public void InsertMapData(int x,int y,MapNode TargetNode)
        {
            if (TargetNode == null || x < 0 || y < 0 || x >= mMapSize.x || y >= mMapSize.y)
                return;
            //Debug.Log (TargetNode.WorldPos);
            mMapData[x - mMapOriOffect.x][y - mMapOriOffect.y] = TargetNode;
        }

        public MapNode GetMapData(int x, int y)
        {
            if (x < mMapOriOffect.x || y < mMapOriOffect.y || x >= mMapSize.x || y >= mMapSize.y)
                return null;

            return mMapData[x - mMapOriOffect.x][y - mMapOriOffect.y];
        }

        public bool GetPointIsInMap(int x, int y)
        {
            if (x < mMapOriOffect.x || y < mMapOriOffect.y || x >= mMapSize.x || y >= mMapSize.y)
                return false;
            if (mMapData[x - mMapOriOffect.x][y - mMapOriOffect.y] == null)
                return false;
            return true;
        }

        public bool GetPointIsInMap(Int2 Pos)
        {
            if (Pos.x < mMapOriOffect.x || Pos.y < mMapOriOffect.y || Pos.x >= mMapSize.x || Pos.y >= mMapSize.y)
                return false;
            if (mMapData[Pos.x - mMapOriOffect.x][Pos.y - mMapOriOffect.y] == null)
                return false;
            return true;
        }

        public bool GetPointIsInMap(Vector2 Pos)
        {
            Int2 tmpPos = ConvertWorldPosToMapPos(Pos);
            if (tmpPos.x < mMapOriOffect.x || tmpPos.y < mMapOriOffect.y || tmpPos.x >= mMapSize.x || tmpPos.y >= mMapSize.y)
                return false;
            if (mMapData[tmpPos.x - mMapOriOffect.x][tmpPos.y - mMapOriOffect.y] == null)
                return false;
            return true;
        }

        public bool GetPointIsInMap(Vector3 Pos)
        {
            Int2 tmpPos = ConvertWorldPosToMapPos(Pos);
            if (tmpPos.x < mMapOriOffect.x || tmpPos.y < mMapOriOffect.y || tmpPos.x >= mMapSize.x || tmpPos.y >= mMapSize.y)
                return false;
            if (mMapData[tmpPos.x - mMapOriOffect.x][tmpPos.y - mMapOriOffect.y] == null)
                return false;
            return true;
        }

        public Int2 ConvertWorldPosToMapPos(Vector3 TargetPos)
        {
			return new Int2((TargetPos.x+(mMapNodeSize.x/2))/ mMapNodeSize.x, (TargetPos.z+(mMapNodeSize.y/2))/ mMapNodeSize.y);
        }

        public Int2 ConvertWorldPosToMapPos(Vector2 TargetPos)
        {
			return new Int2((TargetPos.x+(mMapNodeSize.x/2)) / mMapNodeSize.x, (TargetPos.y+(mMapNodeSize.y/2))/ mMapNodeSize.y);
        }

        public Vector3 ConvertMapPosToWorldPos(Int2 Pos)
        {
			return mMapData[Pos.x - mMapOriOffect.x][Pos.y - mMapOriOffect.y].WorldPos;
        }

        public Vector3 ConvertMapPosToWorldPos(int x, int y)
        {
            try
            {
                return mMapData[x - mMapOriOffect.x][y - mMapOriOffect.y].WorldPos;//new Vector3(x * mMapNodeSize.x, 0, y * mMapNodeSize.y);
            }
            catch (System.Exception ex)
            {
                Debug.Log(x + " " + y + " :Is Null:" + ex);
				return new Vector3(x * mMapNodeSize.x, 0, y * mMapNodeSize.y);
            }
        }

		void OnDrawGizmos()
		{
			if (mMapData == null || mMapData.Count <= 0)
				return;
			
			for (int i = 0; i < mMapSize.x-mMapOriOffect.x; ++i)
				for (int j = 0; j < mMapSize.y-mMapOriOffect.y; j++)
			{
				if (mMapData[i][j] != null)
					Gizmos.DrawWireCube(transform.TransformPoint(mMapData[i][j].WorldPos), Vector3.one * 0.3f);
				//if (mMapData[i][j] != null)
				//	Gizmos.DrawWireCube(transform.TransformPoint(new Vector3((mMapOriOffect.x+i)*0.5f,mMapData[i][j].WorldPos.y+1f,(mMapOriOffect.y+j)*0.5f)), Vector3.one * 0.15f);
			}
		}
    }
}