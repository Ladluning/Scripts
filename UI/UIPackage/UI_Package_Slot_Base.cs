using UnityEngine;
using System.Collections;

public class UI_Package_Slot_Base : UI_Slot_Base
{
    protected Game_Item_Base mCurrentItem;
    protected Game_Storage_Manager_Base mFather;


    public virtual void SetItem(Game_Item_Base Target)
    {
        mCurrentItem = Target;

        if (Target != null)
        {
            mItemSprite.material = Resources.Load("Item/" + Target.mItemMainType, typeof(Material)) as Material;
        }
        else
        {
            mItemSprite.material = null;
        }
    }

    public virtual Game_Item_Base GetItem()
    {
        return mCurrentItem;
    }

    public override void Init(object Father, int SlotID)
    {
		base.Init(Father,SlotID);

        mSlotPosID = SlotID;
		mFather = (Game_Storage_Manager_Base)Father;
    }

    protected virtual void Update()
    {
		Game_Item_Base tmp = mFather.GetItemWithSlotPos(mSlotPosID);
        if (mCurrentItem != tmp && this != UI_Character_Package_Cursor.Singleton().GetCurrentCursor())
        {
            SetItem(tmp);
        }
    }


}
