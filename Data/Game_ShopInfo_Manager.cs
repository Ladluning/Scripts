using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TD_ShopInfo_Manager : MonoBehaviour {

	private static TD_ShopInfo_Manager m_pInterface;
	public static TD_ShopInfo_Manager Singleton()
	{
		return m_pInterface;
	}
	void Awake()
	{
		m_pInterface = this;
	}

	public List<TD_ShopInfo_Tab_Struct> mShopTabList = new List<TD_ShopInfo_Tab_Struct>();

	public TD_ShopInfo_Item_Struct GetItemInfoWithID(int ID)
	{
		for (int i=0; i<mShopTabList.Count; i++) 
		{
			for(int j=0;j<mShopTabList[i].mItemList.Count;j++)
			{
				if(mShopTabList[i].mItemList[j].mItemID == ID)
					return mShopTabList[i].mItemList[j];
			}
		}
		return null;
	}
}
