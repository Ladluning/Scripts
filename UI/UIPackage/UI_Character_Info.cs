using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Character_Info : UIBase {

    private Dictionary<int, UI_Package_Slot_Base> mSlotList = new Dictionary<int, UI_Package_Slot_Base>();

	public Transform mInstanceNode;
    void Start()
    {
		Invoke ("InitPackage",5f);
    }

    public UI_Package_Slot_Base GetSlotWithPos(int Pos)
    {
        if (mSlotList.ContainsKey(Pos))
            return mSlotList[Pos];

        return null;
    }

    public void InitPackage()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
			UI_Package_Slot_Base tmpSlot = mInstanceNode.GetChild(i).GetComponent<UI_Package_Slot_Base>();
            mSlotList.Add(tmpSlot.mSlotPosID, tmpSlot);

			tmpSlot.Init(Client_User.Singleton().GetPackage(),i);
			tmpSlot.name = "Slot_"+i;
        }
    }
}
