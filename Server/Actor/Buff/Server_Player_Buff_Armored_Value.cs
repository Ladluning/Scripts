using UnityEngine;
using System.Collections;

namespace Server
{
	public class Server_Player_Buff_Armored_Value : Server_Game_Buff_Base {
		
		public float Fix_Defend_Value = 0.5f;
		public override void StartBuff(object pTarget)
		{
			base.StartBuff(pTarget);
			mBuffType = E_Buff_Type.Buff_Armored_Value;
		}
		public override void ApplyBuff (object pTarget)
		{
			base.ApplyBuff (pTarget);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mDefend += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mDefend*Fix_Defend_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mMaxDefend += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mMaxDefend*Fix_Defend_Value);
		}
		
		public override void EndBuff (object pTarget)
		{
			base.EndBuff (pTarget);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mDefend -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mDefend*Fix_Defend_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mMaxDefend -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mMaxDefend*Fix_Defend_Value);
		}
	}
}
