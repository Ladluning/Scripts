using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
	public class Server_Game_User_Visible : Server_Game_User_Component {

		protected List<Server_Game_User> mVisiblePlayerList = new List<Server_Game_User>();
		protected List<Server_Game_Enemy> mVisibleEnemyList = new List<Server_Game_Enemy>();

		public void AddVisiblePlayer(Server_Game_User Target)
		{
			mVisiblePlayerList.Add(Target);
		}
		
		public void AddVisibleEnemy(Server_Game_Enemy Target)
		{
			mVisibleEnemyList.Add(Target);
		}

		private void FixedUpdate()
		{
			RequireVisibleData();
			
			mVisibleEnemyList.Clear();
			mVisiblePlayerList.Clear();
		}

		public string RequireVisibleData()
		{
			if (mVisibleEnemyList.Count <= 0&&mVisiblePlayerList.Count <= 0)
				return "";
			
			Dictionary<string, object> tmpSend = SerializeVisibleData();
			
			if (tmpSend == null)
				return "";
			
			this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA, tmpSend);
			return MiniJSON.Json.Serialize(tmpSend);
		}

		private Dictionary<string, object> SerializeVisibleData()
		{
			if (mVisiblePlayerList.Count <= 0&&mVisibleEnemyList.Count<=0)
				return null;
			
			Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA);
			List<object> tmpVisiblePlayerData = new List<object>();
			for (int i = 0; i < mVisiblePlayerList.Count; i++)
			{
				Dictionary<string, object> tmpUser = new Dictionary<string, object>();

				tmpVisiblePlayerData.Add(tmpUser);
			}
			List<object> tmpVisibleEnemyData = new List<object>();
			for (int i = 0; i < mVisibleEnemyList.Count; i++)
			{
				tmpVisibleEnemyData.Add(mVisibleEnemyList[i].GetDataComponent<Server_Game_NPC_Data_Transform>().GetSerializeData());
			}
			((Dictionary<string, object>)tmpSend["results"]).Add("id", mUser.mID);
			((Dictionary<string, object>)tmpSend["results"]).Add("visi_player", tmpVisiblePlayerData);
			((Dictionary<string, object>)tmpSend["results"]).Add("visi_enemy", tmpVisibleEnemyData);
			return tmpSend;
		}
	}
}