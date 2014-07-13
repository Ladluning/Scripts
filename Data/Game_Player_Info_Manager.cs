using UnityEngine;
using System.Collections;

public class Game_Player_Info_Manager : MonoBehaviour {

	public static Game_Player_Info_Manager m_pInterface;
	public static Game_Player_Info_Manager Singleton()
	{
		return m_pInterface;
	}

	void Awake()
	{
		m_pInterface = this;
	}


	Struct_Player_Info mPlayerInfo;



}
