using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
namespace Server
{
    public class Server_Item_Factory : Controller
    {
        public static Struct_Item_Base CopyItem(Struct_Item_Base Target,int Count)
        {
            Struct_Item_Base tmpItem = new Struct_Item_Base(Target);

            return tmpItem;
        }

        public static Struct_Item_Base RandomItem(int PosID = 0)
        {
            Struct_Item_Base tmpItem = new Struct_Item_Base();
            tmpItem.mItemID = DateTime.Now.Ticks + UnityEngine.Random.Range(0, 1000);
            tmpItem.mItemMainType = (int)E_Main_Item_Type.Potion;
            tmpItem.mItemSubType = UnityEngine.Random.Range(0, 21);
            tmpItem.mMaxCount = 20;
            tmpItem.mItemPosID = PosID;
            tmpItem.mUse = (int)E_Use_Item_Type.Normal;
            tmpItem.mCurrentCount = UnityEngine.Random.Range(1, 5);
            return tmpItem;
        }

        public static Struct_Item_Equip RandomEquip(int PosID = 0)
        {
            Struct_Item_Equip tmpItem = new Struct_Item_Equip();
            tmpItem.mItemID = DateTime.Now.Ticks + UnityEngine.Random.Range(0, 1000);
            tmpItem.mItemMainType = (int)E_Main_Item_Type.Equip;
            tmpItem.mItemSubType = UnityEngine.Random.Range(0, 8);
            tmpItem.mMaxCount = 1;
            tmpItem.mItemPosID = PosID;
            tmpItem.mUse = (int)E_Use_Item_Type.Equip;
            tmpItem.mCurrentCount = 1;
            tmpItem.mEquipSoltType = UnityEngine.Random.Range(0, (int)E_Sub_Equip_Type.Count);
            tmpItem.mEquipQualityType = UnityEngine.Random.Range(0, (int)E_Sub_Equip_Quality_Type.Count);

            for(int i=0;i<UnityEngine.Random.Range(2,8);i++)
            {
                Struct_Item_Equip_Stat tmpStat = new Struct_Item_Equip_Stat();
                tmpStat.mAmount = UnityEngine.Random.Range(5,21);
                tmpStat.mIdentifier = UnityEngine.Random.Range(0, (int)E_Sub_Equip_Stat_Identifier_Type.Count);
                tmpStat.mModifier = (int)E_Sub_Equip_Stat_Modifier_Type.Added;
                tmpItem.mStats.Add(tmpStat);
            }

            return tmpItem;
        }
    }
}
