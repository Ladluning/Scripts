using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Scene_Transmit_Manager : MonoBehaviour {

	List<Game_Scene_Transmit_Info> mTransmitList = new List<Game_Scene_Transmit_Info>();
	void Awake()
	{
		for (int i=0; i<transform.childCount; i++)
		{
			if(transform.GetChild(i).GetComponent<Game_Scene_Transmit_Info>()!=null)
				mTransmitList.Add(transform.GetChild(i).GetComponent<Game_Scene_Transmit_Info>());	
		}
	}

	void OnEnable()
	{

	}

	void OnDisable()
	{

	}

	object OnHandleActiveTransmit(object pSender)
	{
		return null;
	}
}
