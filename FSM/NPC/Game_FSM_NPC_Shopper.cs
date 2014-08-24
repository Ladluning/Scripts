using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_FSM_NPC_Shopper : Game_FSM_NPC_Base {

	public UI_NPC_Package mUIPackage;
    protected Game_FSM_NPC_Data_Package mPackage;
    protected Game_FSM_NPC_Data_Transform mTransform;
    public override void ActiveNPC()
    {
        base.ActiveNPC();

    }

    public override void InitWithID(string ID)
	{
        base.InitWithID(ID);

        mPackage = gameObject.AddComponent<Game_FSM_NPC_Data_Package>();
		mPackage.mStorageSlotMaxCount = 20;
		mPackage.mStorageSlotCount = 20;
		mUIPackage.InitPackageWithStroage (mPackage);
		//mPackage.InitPackageWithStroage ();
	}

	void ClickClose()
	{
		ExitNPC ();
	}
}
