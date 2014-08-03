using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_Spawn_Manager : MonoBehaviour
    {
        public List<Server_Game_Spawn_Point_Enemy> mEnemyPointList = new List<Server_Game_Spawn_Point_Enemy>();
        List<Server_Game_Spawn_Point_NPC> mNPCPointList = new List<Server_Game_Spawn_Point_NPC>();

        public Server_Game_Spawn_Point_Enemy GetEnemySpawnPointWithID(string ID)
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

        public Server_Game_Spawn_Point_NPC GetNPCSpawnPointWithID(string ID)
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
            Server_Game_Spawn_Point_Enemy[] tmpEnemyList = gameObject.GetComponentsInChildren<Server_Game_Spawn_Point_Enemy>();
            for (int i = 0; i < tmpEnemyList.Length; i++)
            {
                mEnemyPointList.Add(tmpEnemyList[i]);
            }

            Server_Game_Spawn_Point_NPC[] tmpNPCList = gameObject.GetComponentsInChildren<Server_Game_Spawn_Point_NPC>();
            for (int i = 0; i < tmpNPCList.Length; i++)
            {
                mNPCPointList.Add(tmpNPCList[i]);
            }

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
