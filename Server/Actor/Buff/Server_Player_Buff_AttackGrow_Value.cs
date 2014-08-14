using UnityEngine;
using System.Collections;

namespace Server
{
	public class Server_Player_Buff_AttackGrow_Value : Server_Game_Buff_Base {
		
		public float Fix_Attack_Value = 0.08f;
		public float Fix_Hit_Value = 0.04f;
		public override void StartBuff(object pTarget)
		{
			base.StartBuff(pTarget);
			mBuffType = E_Buff_Type.Buff_AttackGrow_Value;
		}
		public override void ApplyBuff (object pTarget)
		{
			base.ApplyBuff (pTarget);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mHit += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mHit*Fix_Hit_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mAttack += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mAttack*Fix_Attack_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mMaxAttack += (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mMaxAttack*Fix_Attack_Value);
		}
		
		public override void EndBuff (object pTarget)
		{
			base.EndBuff (pTarget);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mHit -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mHit*Fix_Hit_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mAttack -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mAttack*Fix_Attack_Value);
			((Server_Game_User_Property)pTarget).mCurrentProperty.mMaxAttack -= (int)((float)((Server_Game_User_Property)pTarget).mOriginProperty.mMaxAttack*Fix_Attack_Value);
		}
	}
}
