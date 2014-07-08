using UnityEngine;
using System.Collections;

public class Game_Scene_Transmit_Info : Controller 
{
	public bool IsActive = false;
	public string TargetSceneName;
	public string TargetTransmitID;

	void OnTriggerEnter(Collider Col)
	{
		Struct_Scene_Init TmpInit = new Struct_Scene_Init ();
		TmpInit.TargetScene = TargetSceneName;
		TmpInit.TargetPassPointID = TargetTransmitID;
		this.SendEvent (GameEvent.FightingEvent.EVENT_FIGHT_INIT_LEVEL,TmpInit);
	}
	
}
