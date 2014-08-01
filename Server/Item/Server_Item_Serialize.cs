using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Server
{
    public class Server_Item_Serialize
    {
        public static Dictionary<string, object> ConvertItemToJson(Struct_Item_Base Target)
        {
            if (Target.GetType() == typeof(Struct_Item_Equip))
                return ConvertEquipToJson(Target as Struct_Item_Equip);
            else
                return ConvertBaseToJson(Target);
        }

        public static Dictionary<string, object> ConvertBaseToJson(Struct_Item_Base Target)
        {
            Dictionary<string, object> tmpItem = new Dictionary<string, object>();
            tmpItem.Add("type", (int)Target.mItemMainType);
            tmpItem.Add("subtype", Target.mItemSubType);
            tmpItem.Add("id", Target.mItemID);
            tmpItem.Add("pos", Target.mItemPosID);
            tmpItem.Add("num", Target.mCurrentCount);
            tmpItem.Add("max", Target.mMaxCount);
            tmpItem.Add("use", Target.mUse);
            return tmpItem;
        }

        //Dictionary<string,object> ConvertPotion()

        public static Dictionary<string, object> ConvertEquipToJson(Struct_Item_Equip Target)
        {
            Dictionary<string, object> tmpItem = ConvertBaseToJson(Target);
            tmpItem.Add("level", Target.mEquipLevel);
            tmpItem.Add("maxlevel", Target.mEquipMaxLevel);
            tmpItem.Add("equiptype", (int)Target.mEquipSoltType);
            tmpItem.Add("quality", (int)Target.mEquipQualityType);

            List<Dictionary<string, object>> tmpStats = new List<Dictionary<string, object>>();
            for (int i = 0; i < Target.mStats.Count; i++)
            {
                Dictionary<string, object> tmpStat = new Dictionary<string, object>();
                tmpStat.Add("id", (int)Target.mStats[i].mIdentifier);
                tmpStat.Add("modifier", (int)Target.mStats[i].mModifier);
                tmpStat.Add("amount", Target.mStats[i].mAmount);
                tmpStats.Add(tmpStat);
            }
            tmpItem.Add("stats", tmpStats.ToArray());
            return tmpItem;
        }
    }
}

