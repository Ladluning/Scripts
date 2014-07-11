using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_Manager : Controller
    {
        Server_Manager mManager;
        List<Server_Game_User> mPlayerList;

        public int PlayerVisibleRange = 400;
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
    }
}
