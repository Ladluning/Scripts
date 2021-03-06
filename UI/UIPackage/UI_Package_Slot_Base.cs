﻿using UnityEngine;
using System.Collections;

public class UI_Package_Slot_Base : UI_Slot_Base
{
    //protected Game_Item_Base ;
    protected Game_Storage_Manager_Base mFather;
	protected UILabel mNumberLabel;
	protected override void Awake()
	{
		base.Awake ();
		mNumberLabel = transform.FindChild("Label").GetComponent<UILabel>();
	}
    public virtual void SetItem(Game_Item_Base Target)
    {
		if (Target != null&&!mItemSprite.enabled)
        {
			mItemSprite.enabled = true;
			mNumberLabel.enabled = true;
		}
		else if(Target == null&&mItemSprite.enabled)
        {
			mItemSprite.enabled = false;
			mNumberLabel.enabled = false;
        }
    }

    public virtual Game_Item_Base GetItem()
    {
		return (Game_Item_Base)mCurrentItem;
    }

    public override void Init(object Father, int SlotID)
    {
		base.Init(Father,SlotID);

        mSlotPosID = SlotID;
		mFather = (Game_Storage_Manager_Base)Father;
    }

    protected virtual void Update()
    {
		if (mFather == null)
			return;
		Game_Item_Base tmp = mFather.GetItemWithSlotPos(mSlotPosID);
		if (mCurrentItem != tmp)
        {
			mCurrentItem = tmp;

			if(tmp!=null)
			{
				mItemSprite.material = Resources.Load("Item/Icon/" + tmp.mItemMainType+"_"+tmp.mItemSubType, typeof(Material)) as Material;
        	}
		}

		if (tmp != null) 
		{
//			Debug.Log(mSlotPosID);
			mNumberLabel.text = tmp.mCurrentCount>1?tmp.mCurrentCount.ToString():"";		
		}

		if(this != UI_Character_Package_Cursor.Singleton().GetCurrentCursor())
		{
			SetItem(tmp);
		}

    }


}
