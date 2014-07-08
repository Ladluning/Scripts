using UnityEngine;
using System.Collections;

public class Game_UserInfo_Manager: Controller {

	public TD_UserInfo_Struct mInfo;
	private static Game_UserInfo_Manager m_pInterface;
	public static Game_UserInfo_Manager Singleton()
	{
		return m_pInterface;
	}
	void Awake()
	{
		m_pInterface = this;
	}
	void OnEnable()
	{

	}

	void OnDisable()
	{

	}
}
