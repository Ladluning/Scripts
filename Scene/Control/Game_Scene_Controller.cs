using UnityEngine;
using System.Collections;

public class Game_Scene_Controller : Controller 
{
	void OnEnable()
	{
		//this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_INIT_TRANSMIT_POINT,OnHandleInitTransmitPoint);
		//this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_ININT_NPC,OnHandleInitNPC);
		//this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_INIT_MAINPLAYER,OnHandleInitMainPlayer);
		//this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_INIT_OTHERPLAYER,OnHandleInitOtherPlayer);
		//this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_ADD_OTHERPLAYER,OnHandleAddOtherPlayer);
		//this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_REMOVE_OTHERPLAYER,OnHandleRemoveOtherPlayer);
		//this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_FINISH_INIT_SCENE,OnHandleFinishInitScene);
	}

	void OnDisable()
	{

	}

	public virtual void Init(string SceneID,string CurrentTransmitID)
	{
		//this.SendEvent (GameEvent.WebEvent.EVENT_WEB_REQUAIR_INIT_SCENE,SceneID);
	}

	void InitPlayer()
	{

	}

	void InitNPC()//Server
	{

	}

	void InitEnemySpawn()//Server
	{

	}


}
