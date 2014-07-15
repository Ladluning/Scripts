using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace Server
{
    public class Server_Data_Map : MonoBehaviour
    {

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

        static public bool Load(string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.Open);
            if (fs == null)
            {
                Debug.LogError("Cant Open File:" + Path);
                return false;
            }
            byte[] date = new byte[fs.Length];
            fs.Read(date, 0, (int)fs.Length);

            Debug.Log(fs.Length);
            DeserializeBinary(date);

            fs.Close();

            Debug.Log("Load Success");
            return true;
        }
        static public void DeserializeBinary(byte[] buf)
        {
            ByteArray newArray = new ByteArray(buf);

            DataInfo tmpInfo = new DataInfo();
            newArray.Get_(out tmpInfo.MapID);
            newArray.Get_(out tmpInfo.DateLength);
            newArray.Get_(out tmpInfo.MapOffectX);
            newArray.Get_(out tmpInfo.MapOffectY);
            newArray.Get_(out tmpInfo.MapSizeX);
            newArray.Get_(out tmpInfo.MapSizeY);

            Server_Game_Manager.Singleton().GetSceneWithID("Scene_" + tmpInfo.MapID.ToString()).InitMapData(tmpInfo.MapSizeX, tmpInfo.MapSizeY);
            //Debug.Log (tmpInfo.MapID+" "+tmpInfo.MapSizeX);
            for (int i = 0; i < tmpInfo.DateLength; i++)
            {
                MapNode tmpNode = new MapNode();
                newArray.Get_(out tmpNode.Value);
                short X;
                short Y;
                newArray.Get_(out X);
                newArray.Get_(out Y);
                tmpNode.MapPos = new Int2((int)X, (int)Y);
                float A;
                float B;
                float C;
                newArray.Get_(out A);
                newArray.Get_(out B);
                newArray.Get_(out C);
                tmpNode.WorldPos = new Vector3(A, B, C);
                Server_Game_Manager.Singleton().GetSceneWithID("Scene_" + tmpInfo.MapID.ToString()).InsertMapData((int)X,(int)Y, tmpNode);
            }
        }
    }
}
