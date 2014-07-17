using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_Scene_PathFinder : Server_Game_Scene_Map
    {
        [HideInInspector]
        public LinkedList<MapNode> FindList = new LinkedList<MapNode>();
        [HideInInspector]
        public List<MapNode> CloseList = new List<MapNode>();
        [HideInInspector]
        public List<Vector3> MovePath = new List<Vector3>();
        [HideInInspector]
        public MapNode EndGrid;
        [HideInInspector]
        public MapNode StartGrid;
        [HideInInspector]
        public Int2 EndPos;

        public bool StartFindPath(Int2 start, Int2 end)
        {
            //EndPos = end;

            StartGrid = GetMapData(start.x, start.y);
            EndGrid = GetMapData(end.x, end.y);

            if (StartGrid == null || EndGrid == null)
                return false;

            if (StartGrid == EndGrid)
            {
                return true;
            }

            MovePath.Clear();
            FindNode(StartGrid);
            //FindList.Sort();

            if (FindList.Count <= 0)
            {
                Debug.Log("Cant Find Path");
                ClearPathList();
                return false;
            }
            int i = 0;

            while (true)
            {
                //Debug.Log("StartFind");
                //i++;
                // PathNode Tmp = FindList[0];
                MapNode Tmp = FindList.First.Value;

                if (Tmp == EndGrid)
                {
                    //Debug.Log("Find Path "+i);
                    BuildPath();
                    ClearPathList();
                    return true;
                }

                // FindList.RemoveAt(0);
                FindList.RemoveFirst();
                FindNode(Tmp);
                //FindList.Sort();

                if (FindList.Count <= 0)
                {
                    Debug.Log("Cant Find Path" + i);
                    ClearPathList();
                    return false;
                }

                //if (i > 1000)
                //    return false;
            }
        }

        void BuildPath()
        {
            //MovePath.Add(EndGrid.transform);
            MapNode Tmp = EndGrid;
            while (Tmp.parent != null)
            {
                MovePath.Add(this.ConvertMapPosToWorldPos(Tmp.MapPos.x, Tmp.MapPos.y));
                Tmp = Tmp.parent;
                if (Tmp == StartGrid)
                    break;
                //    MovePath.Add(Tmp.transform);
            }
            MovePath.Add(this.ConvertMapPosToWorldPos(StartGrid.MapPos.x, StartGrid.MapPos.y));
            MovePath.Reverse();
        }

        void ClearPathList()
        {
            foreach (MapNode p in FindList)
            {
                p.parent = null;
                p.g = 0;
                p.IsClose = false;
                p.IsOpen = false;
                p.f = 0;
            }

            for (int i = 0; i < CloseList.Count; i++)
            {
                CloseList[i].parent = null;
                CloseList[i].g = 0;
                CloseList[i].IsClose = false;
                CloseList[i].IsOpen = false;
                CloseList[i].f = 0;
            }
            FindList.Clear();
            CloseList.Clear();
        }

        void FindNode(MapNode TmpValue)
        {
            CloseList.Add(TmpValue);
            TmpValue.IsClose = true;
            //Debug.Log("Node = " + TmpValue.point.ToString());

            GetRealValue(TmpValue.MapPos.x + 1, TmpValue.MapPos.y, TmpValue, 10);
            GetRealValue(TmpValue.MapPos.x - 1, TmpValue.MapPos.y, TmpValue, 10);
            GetRealValue(TmpValue.MapPos.x, TmpValue.MapPos.y + 1, TmpValue, 10);
            GetRealValue(TmpValue.MapPos.x, TmpValue.MapPos.y - 1, TmpValue, 10);
            GetRealValue(TmpValue.MapPos.x + 1, TmpValue.MapPos.y + 1, TmpValue, 14);
            GetRealValue(TmpValue.MapPos.x - 1, TmpValue.MapPos.y - 1, TmpValue, 14);
            GetRealValue(TmpValue.MapPos.x - 1, TmpValue.MapPos.y + 1, TmpValue, 14);
            GetRealValue(TmpValue.MapPos.x + 1, TmpValue.MapPos.y - 1, TmpValue, 14);
        }

        void GetRealValue(int x, int y, MapNode Father, int price)
        {
            MapNode Target = GetMapData(x, y);
            if (Target == null)
                return;

            if (Target.IsClose)
                return;

            if (FindGridAlreadInList(Target, Father.g + Target.Value + price, Father))
                return;

            Target.parent = Father;
            Target.g = Father.g + Target.Value + price;
            Target.f = Target.g + GetPredictValue(x, y);
            Target.IsOpen = true;
            FindList.AddLast(Target);
        }

        bool FindGridAlreadInList(MapNode pTarget, int NewValue, MapNode Father)
        {
            if (pTarget.IsOpen)
            {
                if (pTarget.g > NewValue)
                {
                    pTarget.parent = Father;
                    pTarget.g = NewValue;
                    pTarget.f = NewValue + GetPredictValue(pTarget.MapPos.x, pTarget.MapPos.y);
                }
                return true;
            }

            return false;
        }
        int GetPredictValue(int x, int y)
        {
            return (Mathf.Abs((int)(EndGrid.MapPos.x - x)) + Mathf.Abs((int)(EndGrid.MapPos.y - y))) * 10;
        }
    }
}
