using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum E_Equipment_Pos
{
	None = 0,
	eHead,
	eLeft_Hand,
	eRight_Hand,
};
public enum E_AppendPropertyType
{
	eHP,
	eAttack,
	eDefend,
	eMP,
	eEXP_Glow,
	eDodge,
	eCrit,
	eSuckBlood,
}
public class AppendProperty
{
	public E_AppendPropertyType eType;
	public float Value;
	//public 
}
[System.Serializable]
public class Game_Equipment_Base_Info
{
	public int mEquipmentID;
	public string mEquipmentName;
	public string mEquipmentIntroduce;
	public E_Equipment_Pos Pos;
}

public class Game_Equipment_InfoManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
