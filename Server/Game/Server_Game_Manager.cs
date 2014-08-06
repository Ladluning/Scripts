using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_Manager : Controller
    {
		private static Server_Game_Manager m_pInterface;
		public static Server_Game_Manager Singleton()
		{
			return m_pInterface;
		}
		Server_Game_Manager()
		{
			m_pInterface = this;
		}

        void OnEnable()
        { 
        
        }

        void OnDisable()
        {
        
        }

        void Awake()
        {
            Server_Game_Scene_Manager[] tmpList = gameObject.GetComponentsInChildren<Server_Game_Scene_Manager>();
            for (int i = 0; i < tmpList.Length; i++)
            {
                mSceneList.Add(tmpList[i]);
            }

        }

		void Start()
		{
			for (int i = 0; i < mSceneList.Count; i++)
			{
				mSceneList[i].Init();
			}
		}

		List<Server_Game_Scene_Manager> mSceneList = new List<Server_Game_Scene_Manager>();
		public Server_Game_Scene_Manager GetSceneWithID(string SceneID)
		{
			for (int i=0; i<mSceneList.Count; i++) 
			{
				if(SceneID == mSceneList[i].gameObject.name)	
					return mSceneList[i];
			}
			return null;
		}

        object OnHandleSwitchLevel(object pSender)
        {
            JsonData tmpData = (JsonData)pSender;

            Server_Game_User tmpUser = GetServerUserWithID((string)tmpData["results"]["id"]);
            if (tmpUser == null)
                return null;

            Server_Game_Transmit_Point tmpCurrentTransmit = GetServerTransmitWithID((string)tmpData["results"]["transmit"]);
            if (tmpCurrentTransmit == null)
                return null;

            if (!tmpCurrentTransmit.IsAvailable)
                return null;

            Server_Game_Transmit_Point tmpTargetTransmit = GetServerTransmitWithID((string)tmpData["results"]["transmit"]);
            if (tmpTargetTransmit == null)
                return null;

            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_PLAYER, tmpUser);

            tmpUser.mDataInfo.SceneID = tmpTargetTransmit.mFather.mSceneID;
            tmpUser.mDataInfo.WorldPos = tmpTargetTransmit.transform.localPosition;
            tmpUser.mDataInfo.WorldRotate = tmpTargetTransmit.transform.localEulerAngles;
            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_PLAYER,tmpUser);

            tmpUser.RequireLoginData();
            return null;
        }

        public Server_Game_User GetServerUserWithID(string ID)
        {
            for (int i = 0; i < mSceneList.Count; ++i)
                for (int j = 0; j < mSceneList[i].mPlayerList.Count; ++j)
                {
                    if (mSceneList[i].mPlayerList[j].mID == ID)
                        return mSceneList[i].mPlayerList[j];
                }
            return null;
        }

		public Server_Game_Transmit_Point GetServerTransmitWithID(string ID)
        {
            for (int i = 0; i < mSceneList.Count; ++i)
            {
                if (mSceneList[i].GetTransmitPointWithID(ID) != null)
                    return mSceneList[i].GetTransmitPointWithID(ID);
            }

            return null;
        }
    }
}
