using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Inventory System statistic
/// </summary>

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
public class Game_Item_Equip_Stat
{
    public E_Sub_Equip_Stat_Identifier_Type mIdentifier;
    public E_Sub_Equip_Stat_Modifier_Type mModifier;
	public int mAmount;

	/// <summary>
	/// Get the localized name of the stat.
	/// </summary>
	
	public string GetName ()
	{
        switch (mIdentifier)
		{
            case E_Sub_Equip_Stat_Identifier_Type.Strength: return Localization.Get("Item_Stat_Name_Strength");
            case E_Sub_Equip_Stat_Identifier_Type.Constitution: return Localization.Get("Item_Stat_Name_Constitution");
            case E_Sub_Equip_Stat_Identifier_Type.Agility: return Localization.Get("Item_Stat_Name_Agility");
            case E_Sub_Equip_Stat_Identifier_Type.Intelligence: return Localization.Get("Item_Stat_Name_Intelligence");
            case E_Sub_Equip_Stat_Identifier_Type.Damage: return Localization.Get("Item_Stat_Name_Damage");
            case E_Sub_Equip_Stat_Identifier_Type.Crit: return Localization.Get("Item_Stat_Name_Crit");
            case E_Sub_Equip_Stat_Identifier_Type.Armor: return Localization.Get("Item_Stat_Name_Armor");
            case E_Sub_Equip_Stat_Identifier_Type.Health: return Localization.Get("Item_Stat_Name_Health");
        case E_Sub_Equip_Stat_Identifier_Type.SP: return Localization.Get("Item_Stat_Name_SP");
		}
		return null;
	}

    static public string GetDescription(E_Sub_Equip_Stat_Identifier_Type i)
	{
		switch (i)
		{
            case E_Sub_Equip_Stat_Identifier_Type.Strength: return Localization.Get("Item_Stat_Description_Strength");
            case E_Sub_Equip_Stat_Identifier_Type.Constitution: return Localization.Get("Item_Stat_Description_Constitution");
            case E_Sub_Equip_Stat_Identifier_Type.Agility: return Localization.Get("Item_Stat_Description_Agility");
            case E_Sub_Equip_Stat_Identifier_Type.Intelligence: return Localization.Get("Item_Stat_Description_Intelligence");
            case E_Sub_Equip_Stat_Identifier_Type.Damage: return Localization.Get("Item_Stat_Description_Damage");
            case E_Sub_Equip_Stat_Identifier_Type.Crit: return Localization.Get("Item_Stat_Description_Crit");
            case E_Sub_Equip_Stat_Identifier_Type.Armor: return Localization.Get("Item_Stat_Description_Armor");
            case E_Sub_Equip_Stat_Identifier_Type.Health: return Localization.Get("Item_Stat_Description_Health");
            case E_Sub_Equip_Stat_Identifier_Type.SP: return Localization.Get("Item_Stat_Description_SP");
		}
		return null;
	}

	static public int Compare(Game_Item_Equip_Stat a, Game_Item_Equip_Stat b)
	{
        int ia = (int)a.mIdentifier;
        int ib = (int)b.mIdentifier;

        if (a.mIdentifier == E_Sub_Equip_Stat_Identifier_Type.Damage) ia -= 10000;
        else if (a.mIdentifier == E_Sub_Equip_Stat_Identifier_Type.Armor) ia -= 5000;
        if (b.mIdentifier == E_Sub_Equip_Stat_Identifier_Type.Damage) ib -= 10000;
        else if (b.mIdentifier == E_Sub_Equip_Stat_Identifier_Type.Armor) ib -= 5000;
		
		if (a.mAmount < 0) ia += 1000;
		if (b.mAmount < 0) ib += 1000;

        if (a.mModifier == E_Sub_Equip_Stat_Modifier_Type.Percent) ia += 100;
        if (b.mModifier == E_Sub_Equip_Stat_Modifier_Type.Percent) ib += 100;
		
		if (ia < ib) return -1;
		if (ia > ib) return 1;
		return 0;
	}
}