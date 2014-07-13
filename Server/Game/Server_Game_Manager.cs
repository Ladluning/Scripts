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
