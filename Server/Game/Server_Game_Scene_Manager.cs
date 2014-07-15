using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{

    public class Server_Game_Scene_Manager : Server_Game_Scene_PathFinder
    {

		private Server_Manager mManager;
		private List<Server_Game_User> mPlayerList = new List<Server_Game_User>();
        private List<Server_Game_Spawn_Manager> mSpawnList = new List<Server_Game_Spawn_Manager>();
        private List<Server_Game_Transmit_Manager> mTransmitList = new List<Server_Game_Transmit_Manager>();
        void OnEnable()
        {
            this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_PLAYER,OnHandleNewPlayer);
            this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_PLAYER,OnHandleDestroyPlayer);
        }

        void OnDisable()
        {
            this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_PLAYER, OnHandleNewPlayer);
            this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_PLAYER, OnHandleDestroyPlayer);
        }

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


        object OnHandleNewPlayer(object pSender)
        {
            Server_Game_User tmpUser = (Server_Game_User)pSender;
            if (!mPlayerList.Contains(tmpUser))
            {
                mPlayerList.Add(tmpUser);
            }
            else
            {
                Debug.LogError("Already Add Player: " + tmpUser.ID);
            }
            return null; 
        }

        object OnHandleDestroyPlayer(object pSender)
        {
            Server_Game_User tmpUser = (Server_Game_User)pSender;
            if (mPlayerList.Contains(tmpUser))
            {
                mPlayerList.Remove(tmpUser);
            }
            return null;
        }

        object OnHandleSwitchLevel(object pSender)
        {
            return null;
        }
    }
}
