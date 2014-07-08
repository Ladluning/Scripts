using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum E_Buff_Type
{
	Buff_Armored_Value,
	Buff_Assault_Value,
	Buff_AttackGrow_Value,
	Buff_AttackMarking_Value,
	Buff_Bang_Value,
	Buff_Blindness_Value,
	Buff_BodyGrow_Value,
}
public class Game_Buff_Manager : MonoBehaviour {

	public List<Game_Buff_Base> BuffFunctionList = new List<Game_Buff_Base>();
	public Material[] BuffIconMaterials;
	private static Game_Buff_Manager m_pInterface;
	Game_Buff_Manager()
	{
		m_pInterface = this;
	}

	public static Game_Buff_Manager Singleton()
	{	
		return m_pInterface;
	}

	public Game_Buff_Base GetBuffFunctionWithID(int vID)
	{
		return InitWithBuffType ((E_Buff_Type)vID);
	}

	public Game_Buff_Base GetBuffFunctionWithName(E_Buff_Type BuffType)
	{
		return InitWithBuffType (BuffType);
	}

	Game_Buff_Base InitWithBuffType(E_Buff_Type BuffType)
	{
		switch(BuffType)
		{
		case E_Buff_Type.Buff_Armored_Value:		return new Buff_Armored_Value();
		case E_Buff_Type.Buff_Assault_Value:		return new Buff_Armored_Value();
		case E_Buff_Type.Buff_AttackGrow_Value:		return new Buff_Armored_Value();
		case E_Buff_Type.Buff_AttackMarking_Value:	return new Buff_Armored_Value();
		case E_Buff_Type.Buff_Bang_Value:			return new Buff_Armored_Value();
		case E_Buff_Type.Buff_Blindness_Value:		return new Buff_Armored_Value();
		case E_Buff_Type.Buff_BodyGrow_Value:		return new Buff_Armored_Value();
		default:return null;
		}
	}

	public Material GetBuffMaterialWithName(string Name)
	{
		foreach(Material Tmp in BuffIconMaterials)
		{
			if(Tmp.name == Name)
				return Tmp;
		}
		return null;		
	}
}
