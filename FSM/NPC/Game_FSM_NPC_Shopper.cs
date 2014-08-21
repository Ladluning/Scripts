using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_FSM_NPC_Shopper : Game_FSM_NPC_Base {

	public UI_NPC_Package mPackage;

    public override void ActiveNPC()
    {
        base.ActiveNPC();
    }

	void Init()
	{
		//mPackage.InitPackageWithStroage ();
	}

	void ClickClose()
	{
		ExitNPC ();
	}
}
