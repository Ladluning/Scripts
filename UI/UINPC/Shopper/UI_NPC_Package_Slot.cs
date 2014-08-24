using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_NPC_Package_Slot : UI_Package_Slot_Base 
{
	protected override void OnClick()
	{

	}
	
	protected override void OnDoubleClick()
	{
		Debug.Log("Double Click");
		Dictionary<string, object> tmpSend = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_BUY_ITEM);
		((Dictionary<string, object>)tmpSend ["results"]).Add("id", Client_User.Singleton().GetID());
		((Dictionary<string, object>)tmpSend ["results"]).Add ("npc", mFather.gameObject.name);
		((Dictionary<string, object>)tmpSend ["results"]).Add ("target",mCurrentItem.mItemID);
		((Dictionary<string, object>)tmpSend ["results"]).Add ("count",1);
		this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_BUY_ITEM,tmpSend);
	}
	
	protected override void OnDrag()
	{

	}
	
	protected override void OnDrop(GameObject go)
	{

	}

	protected override void Update()
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
			mNumberLabel.text = tmp.mCurrentCount>1?tmp.mCurrentCount.ToString():"";		
		}
	}
}
