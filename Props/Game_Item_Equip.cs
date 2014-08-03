using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
[System.Serializable]
public class Game_Item_Equip : Game_Item_Base 
{
	public List<Game_Item_Equip_Stat> mStats = new List<Game_Item_Equip_Stat>();

	public int  mEquipLevel;
	public int  mEquipMaxLevel = 50;
    public E_Sub_Equip_Type mEquipSoltType;
    public E_Sub_Equip_Quality_Type mEquipQualityType;

	public void SetEquipQuality(int TargetQuality)
	{
        mEquipQualityType = (E_Sub_Equip_Quality_Type)TargetQuality;

		switch (mEquipQualityType)
		{
            case E_Sub_Equip_Quality_Type.Cursed: mItemColor = Color.red; break;
            case E_Sub_Equip_Quality_Type.Broken: mItemColor = new Color(0.4f, 0.2f, 0.2f); break;
            case E_Sub_Equip_Quality_Type.Damaged: mItemColor = new Color(0.4f, 0.4f, 0.4f); break;
            case E_Sub_Equip_Quality_Type.Worn: mItemColor = new Color(0.7f, 0.7f, 0.7f); break;
            case E_Sub_Equip_Quality_Type.Sturdy: mItemColor = new Color(1.0f, 1.0f, 1.0f); break;
            case E_Sub_Equip_Quality_Type.Polished: mItemColor = NGUIMath.HexToColor(0xe0ffbeff); break;
            case E_Sub_Equip_Quality_Type.Improved: mItemColor = NGUIMath.HexToColor(0x93d749ff); break;
            case E_Sub_Equip_Quality_Type.Crafted: mItemColor = NGUIMath.HexToColor(0x4eff00ff); break;
            case E_Sub_Equip_Quality_Type.Superior: mItemColor = NGUIMath.HexToColor(0x00baffff); break;
            case E_Sub_Equip_Quality_Type.Enchanted: mItemColor = NGUIMath.HexToColor(0x7376fdff); break;
            case E_Sub_Equip_Quality_Type.Epic: mItemColor = NGUIMath.HexToColor(0x9600ffff); break;
            case E_Sub_Equip_Quality_Type.Legendary: mItemColor = NGUIMath.HexToColor(0xff9000ff); break;
		    default:mItemColor = Color.white;break;
		}
	}

	public void InitEquip()
	{
		mStats.Sort(Game_Item_Equip_Stat.Compare);
		foreach(Game_Item_Equip_Stat tmp in mStats)
		{
			tmp.mAmount = (int)(tmp.mAmount * statMultiplier);
		}

		string tmpColor = "[" + NGUIText.EncodeColor (mItemColor) + "]";
		mItemDescription = tmpColor + GetName() +" -- "+ tmpColor + "Lv " + mEquipLevel+"[-]\n";
		
		for (int i = 0, imax = mStats.Count; i < imax; ++i)
		{
			Game_Item_Equip_Stat stat = mStats[i];
			if (stat.mAmount == 0) continue;
			mItemDescription += "\n"+tmpColor + stat.mAmount;
            if (stat.mModifier == E_Sub_Equip_Stat_Modifier_Type.Percent) mItemDescription += "%";
			mItemDescription += " " + stat.GetName();
			mItemDescription += "[-]";
		}
		
		mItemDescription += "\n[FFFFFF]" + GetTypeDescription();
	}

	protected override void Awake()
	{
        SetUseTo(1 << (int)E_Use_Item_Type.Equip | 1 << (int)E_Use_Item_Type.Sell);
	}
	
	public override string GetItemDescription ()
	{
		return mItemDescription;
	}
	
	public override void SetUse ()
	{
		
	}
	
	public override void UnUse()
	{

	}

	public float statMultiplier
	{
		get
		{
			float mult = 0f;
			
			switch (mEquipQualityType)
			{
                case E_Sub_Equip_Quality_Type.Cursed: mult = -1f; break;
                case E_Sub_Equip_Quality_Type.Broken: mult = 0f; break;
                case E_Sub_Equip_Quality_Type.Damaged: mult = 0.25f; break;
                case E_Sub_Equip_Quality_Type.Worn: mult = 0.9f; break;
                case E_Sub_Equip_Quality_Type.Sturdy: mult = 1f; break;
                case E_Sub_Equip_Quality_Type.Polished: mult = 1.1f; break;
                case E_Sub_Equip_Quality_Type.Improved: mult = 1.25f; break;
                case E_Sub_Equip_Quality_Type.Crafted: mult = 1.5f; break;
                case E_Sub_Equip_Quality_Type.Superior: mult = 1.75f; break;
                case E_Sub_Equip_Quality_Type.Enchanted: mult = 2f; break;
                case E_Sub_Equip_Quality_Type.Epic: mult = 2.5f; break;
                case E_Sub_Equip_Quality_Type.Legendary: mult = 3f; break;
			}
			
			// Take item's level into account
			float linear = (float)mEquipLevel / (float)mEquipMaxLevel;
			
			// Add a curve for more interesting results
			mult *= Mathf.Lerp(linear, linear * linear, 0.5f);
			return mult;
		}
	}
}
