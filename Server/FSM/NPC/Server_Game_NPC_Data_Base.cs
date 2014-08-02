using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
	public class Server_Game_NPC_Data_Base : Controller 
	{
		protected Dictionary<string, object> tmpSend = new Dictionary<string, object>();
		[HideInInspector]
		public Server_Game_NPC_Base mFather;
		public Dictionary<string, object> GetSerializeData()
		{
			return tmpSend;
		}

		public virtual void Init(Server_Game_NPC_Base Father)
		{
			mFather = Father;
		}
	}
}
