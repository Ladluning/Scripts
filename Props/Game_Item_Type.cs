using UnityEngine;
using System.Collections;

public class Game_Item_Type 
{
	public int mItemMainType;
	public int mItemSubType;

	public virtual string GetDescription()
	{
		return Localization.Get("ITEM_MAINTYPE_"+mItemMainType)+"\n"+Localization.Get("ITEM_SUBTYPE_"+mItemSubType);
	}
}
