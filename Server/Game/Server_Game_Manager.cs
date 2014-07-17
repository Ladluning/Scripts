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

        void Awake()
        {
            Server_Game_Scene_Manager[] tmpList = gameObject.GetComponentsInChildren<Server_Game_Scene_Manager>();
            for (int i = 0; i < tmpList.Length; i++)
            {
                mSceneList.Add(tmpList[i]);
            }

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
    }
}
