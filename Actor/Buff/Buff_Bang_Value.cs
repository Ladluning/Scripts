using UnityEngine;
using System.Collections;

public class Buff_Bang_Value : Game_Buff_Base {

	public float Fix_Attack_Value = 2.0f;

	public override void StartBuff (Game_Actor_Property_Base pTarget,float WorkingTime = -1)
	{
		base.StartBuff (pTarget,WorkingTime);
		pTarget.mAttack = (int)((float)pTarget.mAttack*Fix_Attack_Value);
	}
	
	public override void EndBuff (Game_Actor_Property_Base pTarget)
	{
		base.EndBuff (pTarget);
		pTarget.mAttack = (int)((float)pTarget.mAttack/Fix_Attack_Value);
	}
}
