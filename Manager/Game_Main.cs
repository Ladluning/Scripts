using UnityEngine;
using System.Collections;

public class Game_Main : Controller {

	// Use this for initialization
	void Start () {

		this.SendEvent (GameEvent.UIEvent.EVENT_UI_SHOW_UI,"TD_UI_MainGame");
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
