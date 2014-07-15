using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_Spawn_Manager : MonoBehaviour
    {
        List<Server_Game_Spawn_Enemy_Point> mEnemyPointList = new List<Server_Game_Spawn_Enemy_Point>();
        List<Server_Game_Spawn_NPC_Point> mNPCPointList = new List<Server_Game_Spawn_NPC_Point>();

        public Server_Game_Spawn_Enemy_Point GetEnemySpawnPointWithID(string ID)
        {
            for (int i = 0; i < mEnemyPointList.Count; i++)
            {
                if (mEnemyPointList[i].name == ID)
                {
                    return mEnemyPointList[i];
                }
            }

            return null;
        }

        public Server_Game_Spawn_NPC_Point GetNPCSpawnPointWithID(string ID)
        {
            for (int i = 0; i < mNPCPointList.Count; i++)
            {
                if (mNPCPointList[i].name == ID)
                {
                    return mNPCPointList[i];
                }
            }

            return null;
        }

        public void InitSpawnPoint()
        {
            for (int i = 0; i < mEnemyPointList.Count; i++)
            {
                mEnemyPointList[i].Init();
            }
            for (int i = 0; i < mNPCPointList.Count; i++)
            {
                mNPCPointList[i].Init();
            }
        }
    }
}
