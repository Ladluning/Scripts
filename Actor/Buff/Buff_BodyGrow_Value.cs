using UnityEngine;
using System.Collections;

public class Buff_BodyGrow_Value : Game_Buff_Base {

	public float Fix_HP_Value = 1.6f;
	public float Fix_Defend_Value = 0.7f;
	public override void StartBuff (Game_Actor_Property_Base pTarget,float WorkingTime = -1)
	{
		base.StartBuff (pTarget,WorkingTime);
		pTarget.mDefend = (int)((float)pTarget.mDefend*Fix_Defend_Value);
		pTarget.mHP = (int)((float)pTarget.mHP*Fix_HP_Value);
	}
	
	public override void EndBuff (Game_Actor_Property_Base pTarget)
	{
		base.EndBuff (pTarget);
		pTarget.mDefend = (int)((float)pTarget.mDefend/Fix_Defend_Value);
		pTarget.mHP = (int)((float)pTarget.mHP/Fix_HP_Value);
	}
}
