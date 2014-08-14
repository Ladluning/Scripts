using UnityEngine;
using System.Collections;

namespace Server
{
	public class Server_Player_Buff_Bang_Value : Server_Game_Buff_Base {

		public float Fix_Attack_Value = 1.0f;
		public override void StartBuff(object pTarget)
		{
			base.StartBuff(pTarget);
			mBuffType = E_Buff_Type.Buff_Bang_Value;
		}
		public override void ApplyBuff (object pTarget)
		{
			base.ApplyBuff (pTarget);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mAttack += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mAttack*Fix_Attack_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mMaxAttack += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mMaxAttack*Fix_Attack_Value);
		}
		
		public override void EndBuff (object pTarget)
		{
			base.EndBuff (pTarget);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mAttack -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mAttack*Fix_Attack_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mMaxAttack -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mMaxAttack*Fix_Attack_Value);
		}
	}
}
