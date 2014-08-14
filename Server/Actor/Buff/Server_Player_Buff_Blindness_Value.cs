using UnityEngine;
using System.Collections;

namespace Server
{
	public class Server_Player_Buff_Blindness_Value : Server_Game_Buff_Base {

		public float Fix_Hit_Value = 0.25f;
		public override void StartBuff(object pTarget)
		{
			base.StartBuff(pTarget);
			mBuffType = E_Buff_Type.Buff_Blindness_Value;
		}
		public override void ApplyBuff (object pTarget)
		{
			base.ApplyBuff (pTarget);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mHit += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mHit*Fix_Hit_Value);
		}
		
		public override void EndBuff (object pTarget)
		{
			base.EndBuff (pTarget);

			((Server_Game_User_Property)pTarget).mCurrentProperty.mHit -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mHit*Fix_Hit_Value);
		}
	}
}
