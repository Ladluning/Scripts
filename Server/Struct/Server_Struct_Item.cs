using UnityEngine;
using System.Collections;
using System.Collections.Generic;

   
namespace Server
{
    public enum E_Main_Item_Type
    {
        None = 0,
        Potion,
        Equip,
    }

    public enum E_Use_Item_Type
    {
        None = 0,
        Sell,
        Task,
        Equip,
        Unlock,
        Normal,
    }


    public enum E_Sub_Equip_Type
    {
        None = 0,		// First element MUST be 'None'
        Weapon,			// All the following elements are yours to customize -- edit, add or remove as you desire
        Shield,
        Body,
        Shoulders,
        Bracers,
        Boots,
        Trinket,

        Count,
    }


    public enum E_Sub_Equip_Quality_Type
    {
        Broken = 0,
        Cursed,
        Damaged,
        Worn,
        Sturdy,			// Normal quality
        Polished,
        Improved,
        Crafted,
        Superior,
        Enchanted,
        Epic,
        Legendary,

        Count,
    }

    public enum E_Sub_Equip_Stat_Identifier_Type
    {
        Strength = 0,
        Constitution,
        Agility,
        Intelligence,
        Damage,
        Crit,
        Armor,
        Health,
        SP,
        Other,

        Count,
    }

    public enum E_Sub_Equip_Stat_Modifier_Type
    {
        Added = 0,
        Percent,
    }

    [System.Serializable]
    public class Struct_Item_Base
    {
        public int mUse;
        public int mItemMainType;//User For Class of Object
        public int mItemSubType;//User For Icon // Use for Description
        public int mMaxCount = 5;
        public int mCurrentCount = 1;
        public long mItemID;//uniform ID
        public int mItemPosID;

        public Struct_Item_Base()
        { 
            
        }

        public Struct_Item_Base(Struct_Item_Base Target)
        {
            mUse = Target.mUse;
            mItemMainType = Target.mItemMainType;
            mItemSubType = Target.mItemSubType;
            mMaxCount = Target.mMaxCount;
            mCurrentCount = Target.mCurrentCount;
            mItemID = Target.mItemID;
            mItemPosID = Target.mItemPosID;
        }
    }

    [System.Serializable]
    public class Struct_Item_Equip : Struct_Item_Base
    {
        public Struct_Item_Equip(): base()
        { 
        
        }

        public Struct_Item_Equip(Struct_Item_Equip Target):base(Target)
        {
            mEquipLevel = Target.mEquipLevel;
            mEquipMaxLevel = Target.mEquipMaxLevel;
            mEquipSoltType = Target.mEquipSoltType;
            mEquipQualityType = Target.mEquipQualityType;

            for (int i = 0; i < Target.mStats.Count; ++i)
            {
                mStats.Add(new Struct_Item_Equip_Stat(Target.mStats[i]));
            }
        }

        public List<Struct_Item_Equip_Stat> mStats = new List<Struct_Item_Equip_Stat>();

        public int mEquipLevel;
        public int mEquipMaxLevel;
        public int mEquipSoltType;
        public int mEquipQualityType;
    }

    [System.Serializable]
    public class Struct_Item_Equip_Stat
    {
        public Struct_Item_Equip_Stat()
        { }
        public Struct_Item_Equip_Stat(Struct_Item_Equip_Stat Target)
        {
            mIdentifier = Target.mIdentifier;
            mModifier = Target.mModifier;
            mAmount = Target.mAmount;
        }
        public int mIdentifier;
        public int mModifier;
        public int mAmount;
    }
}
