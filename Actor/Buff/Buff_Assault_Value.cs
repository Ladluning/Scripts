using UnityEngine;
using System.Collections;

public class Buff_Assault_Value : Game_Buff_Base {
	
	public float Fix_Attack_Value = 1.04f;
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
