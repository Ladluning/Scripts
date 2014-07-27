using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Enemy_Spawn : Controller
{
	public List<Game_Enemy_Spawn_Point> mEnemyPointList = new List<Game_Enemy_Spawn_Point>();
	void Awake()
	{
		InitSpawnPoint ();
	}

	void OnEnable()
	{
		this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA,OnHandleUpdateVisibleData);
	}
	
	void OnDisable()
	{
		this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_VISIBLE_DATA, OnHandleUpdateVisibleData);
	}
	
	public Game_Enemy_Spawn_Point GetEnemySpawnPointWithID(string ID)
	{
		for (int i = 0; i < mEnemyPointList.Count; i++)
		{
			if (mEnemyPointList[i].name == ID)
			{
				return mEnemyPointList[i];
			}
		}
		
		return null;
	}
	
	public void InitSpawnPoint()
	{
		Game_Enemy_Spawn_Point[] tmpEnemyList = gameObject.GetComponentsInChildren<Game_Enemy_Spawn_Point>();
		for (int i = 0; i < tmpEnemyList.Length; i++)
		{
			mEnemyPointList.Add(tmpEnemyList[i]);
		}
	}

	Dictionary<string, GameObject> mPlayerList = new Dictionary<string, GameObject>();
	Dictionary<string, GameObject> mEnemyList = new Dictionary<string, GameObject>();
	object OnHandleUpdateVisibleData(object pSender)
	{
		JsonData tmpJson = new JsonData(pSender);
		
		//if ((string)tmpJson["results"]["id"] != ID)
		//	return null;
		//Debug.Log(MiniJSON.Json.Serialize(pSender));
		for (int i = 0; i < tmpJson["results"]["visi_player"].Count; ++i)
		{
			HandlePlayerVisible(tmpJson["results"]["visi_player"][i]);
		}
		for (int i = 0; i < tmpJson["results"]["visi_enemy"].Count; ++i)
		{
			HandleEnemyVisible(tmpJson["results"]["visi_enemy"][i]);
		}
		
		return null;
	}
	
	void HandlePlayerVisible(JsonData Target)
	{
		GameObject tmpObject;
		if (mPlayerList.ContainsKey((string)Target["id"]))
		{
			tmpObject = mPlayerList[(string)Target["id"]];
		}
		else
		{
			tmpObject = Game_Resources_Pool.Singleton().GetUnusedActorWithID((string)Target["mesh"]);
			mPlayerList.Add((string)Target["id"], tmpObject);
		}
		tmpObject.transform.position = new Vector3((float)Target["pos_x"], (float)Target["pos_y"], (float)Target["pos_z"]);
		//tmpObject.transform.localEulerAngles = new Vector3((float)Target["rotate_x"], (float)Target["rotate_y"], (float)Target["rotate_z"]);
		
		//Target["id"];
		//Target["hp"];
		//Target["mp"];
		//Target["exp"];
		//Target["mesh"];
	}
	
	void HandleEnemyVisible(JsonData Target)
	{
		GameObject tmpObject;
		if (mEnemyList.ContainsKey((string)Target["id"]))
		{
			tmpObject = mEnemyList[(string)Target["id"]];
		}
		else
		{
			tmpObject = Game_Resources_Pool.Singleton().GetUnusedActorWithID((string)Target["mesh"]);
			mEnemyList.Add((string)Target["id"], tmpObject);
		}
		tmpObject.GetComponent<Game_FSM_Enemy_General_Controller>().SetState((int)Target["fsm"],(string)Target["ani"]);
		tmpObject.transform.position = new Vector3((float)Target["pos_x"], (float)Target["pos_y"], (float)Target["pos_z"]);
		tmpObject.transform.localEulerAngles = new Vector3((float)Target["rotate_x"], (float)Target["rotate_y"], (float)Target["rotate_z"]);
		
		//Target["scene"]
		//Target["fsm"]
		//Target["ani"]
		//Target["hp"]
		//Target["maxHP"]
		//Target["mp"]
		//Target["maxMP"]
		//Target["attack"]
		//Target["defend"]
		//Target["exp"]
	}
}
