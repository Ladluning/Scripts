using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Struct_User_Info_Base {
	
	public string udid;
	public int HP;
	public int MaxHP;
	public int MP;
	public int MaxMP;
	public ulong EXP;
	public ulong MaxExp;
	
}

[System.Serializable]
public class Struct_Player_Info : Struct_User_Info_Base {
	
	public List<string> ItemList = new List<string> ();
	public List<string> SkillList = new List<string> ();
	
}
