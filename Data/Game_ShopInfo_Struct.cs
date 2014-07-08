using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class TD_ShopInfo_Tab_Struct
{
	public List<TD_ShopInfo_Item_Struct> mItemList = new List<TD_ShopInfo_Item_Struct>();
};
[System.Serializable]
public class TD_ShopInfo_Item_Struct 
{
	public int mItemID;
	public string mBuildingID;
	public string mItemName;
	public string mItemIntro;
	public string mItemSprite;
	public int  mItemPriceType;
	public int  mItemPrice;
	public ulong mNextItemBuyTime;
	public int  mItemCD;
	public int  mCurrentBuyCount;
	public int  mMaxBuyCount;
};
