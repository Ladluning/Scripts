using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{

    public class Server_Game_Scene_Manager : Server_Game_Scene_PathFinder
    {
		private Server_Manager mManager;
		private Transform mPlayerListNode;
        [HideInInspector]
        public List<Server_Game_User> mPlayerList = new List<Server_Game_User>();
        [HideInInspector]
        public List<Server_Game_Enemy> mEnemyList = new List<Server_Game_Enemy>();

        private List<Server_Game_Spawn_Manager> mSpawnList = new List<Server_Game_Spawn_Manager>();
        private List<Server_Game_Transmit_Manager> mTransmitList = new List<Server_Game_Transmit_Manager>();
        void OnEnable()
        {
            this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_PLAYER,OnHandleNewPlayer);
            this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_PLAYER,OnHandleDestroyPlayer);
            this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_ENEMY,OnHandleNewEnemy);
            this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_ENEMY,OnHandleDestroyEnemy);
			this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_SCENE_DATA,OnHandleInitSceneData);
        }

        void OnDisable()
        {
            this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_PLAYER, OnHandleNewPlayer);
            this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_PLAYER, OnHandleDestroyPlayer);
            this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_ENEMY, OnHandleNewEnemy);
            this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_ENEMY, OnHandleDestroyEnemy);
			this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_SCENE_DATA,OnHandleInitSceneData);
        }

		public override void Init()
		{
            base.Init();

			mManager = Server_Manager.Singleton();
			mPlayerListNode = InitUserSaveNode ();

            Server_Game_Spawn_Manager[] tmpSpawnList = gameObject.GetComponentsInChildren<Server_Game_Spawn_Manager>();
            for (int i = 0; i < tmpSpawnList.Length; ++i)
            {
                mSpawnList.Add(tmpSpawnList[i]);
            }
            for (int i = 0; i < mSpawnList.Count; ++i)
            {
                mSpawnList[i].InitSpawnPoint();
            }

            Server_Game_Transmit_Manager[] tmpTransmitList = gameObject.GetComponentsInChildren<Server_Game_Transmit_Manager>();
            for (int i = 0; i < tmpTransmitList.Length; ++i)
            {
                mTransmitList.Add(tmpTransmitList[i]);
            }
            for (int i = 0; i < mTransmitList.Count; ++i)
            {
                mTransmitList[i].InitTransmitManager();
            }
		}

		Transform InitUserSaveNode()
		{
			Transform tmp = transform.FindChild ("UserList");
			if (tmp == null)
			{
				tmp = (new GameObject() ).transform;
				tmp.name = "UserList";
				tmp.transform.parent = transform;
				tmp.transform.localPosition = Vector3.zero;
			}
			return tmp;
		}

		void LateUpdate()
		{
            for (int i = 0; i < mPlayerList.Count; ++i)
			{
                for (int j = i + 1; j < mPlayerList.Count; ++j)
				{
					if (!mPlayerList[j].GetIsChanged() || !mPlayerList[i].GetIsChanged())
						continue;
					
					if ((mPlayerList[j].transform.position - mPlayerList[i].transform.position).sqrMagnitude < PlayerVisibleRange)
					{
						mPlayerList[j].GetVisible().AddVisiblePlayer(mPlayerList[i]);
						mPlayerList[i].GetVisible().AddVisiblePlayer(mPlayerList[j]);
					}
				}

                for (int j = 0; j < mEnemyList.Count; ++j)
                {
                    if (mEnemyList[j].GetIsChanged() || mPlayerList[i].GetIsChanged())
					{
                    	if ((mEnemyList[j].transform.position - mPlayerList[i].transform.position).sqrMagnitude < PlayerVisibleRange)
                   		{
							mPlayerList[i].GetVisible().AddVisibleEnemy(mEnemyList[j]);
                    	}
					}
                }
			}
		}


        object OnHandleNewPlayer(object pSender)
        {
            Server_Game_User tmpUser = (Server_Game_User)pSender;

			if (tmpUser.mDataInfo.SceneID != mSceneID)
				return null;

            if (!mPlayerList.Contains(tmpUser))
            {
                mPlayerList.Add(tmpUser);
				tmpUser.transform.parent = mPlayerListNode;
				tmpUser.transform.localPosition = tmpUser.mDataInfo.WorldPos;
				//Debug.LogError("Add Player: " + tmpUser.ID);
            }
            else
            {
                Debug.LogError("Already Add Player: " + tmpUser.mID);
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

        object OnHandleNewEnemy(object pSender)
        {
            Server_Game_Enemy tmpUser = (Server_Game_Enemy)pSender;
            if (tmpUser.mSceneID == mSceneID&&!mEnemyList.Contains(tmpUser))
            {
                mEnemyList.Add(tmpUser);
            }
            return null;
        }

        object OnHandleDestroyEnemy(object pSender)
        {
            Server_Game_Enemy tmpUser = (Server_Game_Enemy)pSender;
            if (mEnemyList.Contains(tmpUser))
            {
                mEnemyList.Remove(tmpUser);
            }
            return null;
        }



        public Server_Game_Transmit_Point GetTransmitPointWithID(string ID)
        {
            for (int i = 0; i < mTransmitList.Count; ++i)
            {
                if (mTransmitList[i].GetTransmitWithID(ID)!=null)
                    return mTransmitList[i].GetTransmitWithID(ID);
            }
            return null;
        }

		object OnHandleInitSceneData(object pSender)
		{
			JsonData tmpJson = new JsonData(pSender);
			if((string)tmpJson["results"]["target"]!=mSceneID)
				return null;

			return null;
		}

    }
}
