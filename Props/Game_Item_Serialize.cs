using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Item_Serialize
{
    public static Game_Item_Base ConvertJsonToItem(JsonData Target)
    {
        if ((string)Target["type"] == "2")
            return ConvertJsonToEquip(Target);
        else
            return ConvertJsonToBase(Target);
    }

    public static Game_Item_Base ConvertJsonToBase(JsonData Target)
    {
        Game_Item_Base tmpItem= new Game_Item_Base();
        tmpItem.mItemMainType = (int)Target["type"];
        tmpItem.mItemSubType = (int)Target["subtype"];
        tmpItem.mItemID = (long)Target["id"];
        tmpItem.mItemPosID = (int)Target["pos"];
        tmpItem.mCurrentCount = (int)Target["num"];
        tmpItem.mMaxCount = (int)Target["max"];
        tmpItem.mUse = (int)Target["use"];

        return tmpItem;
    }

    //Dictionary<string,object> ConvertPotion()

    public static Game_Item_Base ConvertJsonToEquip(JsonData Target)
    {
        Game_Item_Equip tmpItem = new Game_Item_Equip();
        tmpItem.mItemMainType = (int)Target["type"];
        tmpItem.mItemSubType = (int)Target["subtype"];
        tmpItem.mItemID = (long)Target["id"];
        tmpItem.mItemPosID = (int)Target["pos"];
        tmpItem.mCurrentCount = (int)Target["num"];
        tmpItem.mMaxCount = (int)Target["max"];
        tmpItem.mUse = (int)Target["use"];
        tmpItem.mEquipLevel = (int)Target["level"];
        tmpItem.mEquipMaxLevel = (int)Target["maxlevel"];
        tmpItem.mEquipSoltType = (E_Sub_Equip_Type)(int)Target["equiptype"];
        tmpItem.mEquipQualityType = (E_Sub_Equip_Quality_Type)(int)Target["quality"];

        for (int i = 0; i < Target["stats"].Count; ++i)
        {
            Game_Item_Equip_Stat tmpStat = new Game_Item_Equip_Stat();
            tmpStat.mIdentifier = (E_Sub_Equip_Stat_Identifier_Type)(int)Target["stats"][i]["id"];
            tmpStat.mModifier = (E_Sub_Equip_Stat_Modifier_Type)(int)Target["stats"][i]["modifier"];
            tmpStat.mAmount = (int)Target["stats"][i]["amount"];
            tmpItem.mStats.Add(tmpStat);
        }
        return tmpItem;
    }

}
