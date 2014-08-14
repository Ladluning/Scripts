using UnityEngine;
using System.Collections;
namespace Server
{
	public class Server_Buff_BodyGrow_Value : Server_Game_Buff_Base {

		public float Fix_HP_Value = 0.3f;
		public float Fix_Defend_Value = 0.35f;
		public override void StartBuff(object pTarget)
		{
			base.StartBuff(pTarget);
			mBuffType = E_Buff_Type.Buff_BodyGrow_Value;
		}
		public override void ApplyBuff (object pTarget)
		{
			base.ApplyBuff (pTarget);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mDefend += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mDefend*Fix_Defend_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mMaxDefend += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mMaxDefend*Fix_Defend_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mHP += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mHP*Fix_HP_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mMaxHP += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mMaxHP*Fix_HP_Value);
		}
		
		public override void EndBuff (object pTarget)
		{
			base.EndBuff (pTarget);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mDefend -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mDefend*Fix_Defend_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mMaxDefend -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mMaxDefend*Fix_Defend_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mHP -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mHP*Fix_HP_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mMaxHP -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mMaxHP*Fix_HP_Value);
		}
	}
}
