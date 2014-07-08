using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum E_Camera_Effect_Type
{
	None = 0,
	PlayerDied,
}
public class Game_Camera_Effect_Manager : Controller {

	static Game_Camera_Effect_Manager m_pInterface;
	public static Game_Camera_Effect_Manager Singleton()
	{
		return m_pInterface;
	}

	Game_Camera_Effect_Manager()
	{
		m_pInterface = this;
	}

	public void StartEffect(E_Camera_Effect_Type EffectType)
	{

	}

	public void StopEffect(E_Camera_Effect_Type EffectType)
	{

	}
}
