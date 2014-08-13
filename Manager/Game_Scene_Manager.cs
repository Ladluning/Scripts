using UnityEngine;
using System.Collections;
[System.Serializable]
public class Struct_Scene_Init
{
	public string TargetScene = "";
	public string TargetPassPointID = "";
	public Vector3 Pos;
}
public class Game_Scene_Manager : Controller {

	public Game_Scene_Relation m_pRelationship;
	private Game_FSM_MainPlayer_Controller mMainCharacter;
	protected GameObject mCurrentActiveScene;
	void OnEnable()
	{
		this.RegistEvent (GameEvent.FightingEvent.EVENT_FIGHT_INIT_LEVEL,OnHandleInitLevel);
	}
	void OnDisable()
	{
		this.UnRegistEvent (GameEvent.FightingEvent.EVENT_FIGHT_INIT_LEVEL,OnHandleInitLevel);
	}

	object OnHandleInitLevel(object pSender)
	{
//		Struct_Scene_Init TmpReceive = (Struct_Scene_Init)pSender;
		StartCoroutine(CreateSceneWithName ((JsonData)pSender));
//
//		if (TmpReceive.TargetPassPointID != null || TmpReceive.TargetPassPointID != "") 
//		{
//			TmpTarget.GetComponent<Game_Scene_Controller>().
//		}
		return null;
	}

	IEnumerator CreateSceneWithName(JsonData LoginData)
	{
		Debug.Log (gameObject.name + " "+m_pRelationship);
		Game_Scene_Property tmpProperty = m_pRelationship.GetScenePropertyWithSceneName ((string)LoginData["results"]["scene"]);
		this.SendEvent (GameEvent.UIEvent.EVENT_UI_SHOW_LOADING_BLOCK,new Struct_Loading_Info(true,tmpProperty.DelayLoadingTime,null));
		yield return new WaitForSeconds (tmpProperty.DelayLoadingTime);

		//Init MainCharacter
		if(mMainCharacter==null)
			mMainCharacter = (Instantiate (Game_Resources_Manager.Singleton().GetCharacterPrefab()) as GameObject).GetComponent<Game_FSM_MainPlayer_Controller>();

		mMainCharacter.name = (string)LoginData["results"]["id"];
		Vector3 tmpPos = new Vector3((float)(LoginData["results"]["pos_x"]), (float)(LoginData["results"]["pos_y"]), (float)(LoginData["results"]["pos_z"]));
		Vector3 tmpRotate = new Vector3((float)(LoginData["results"]["rotate_x"]), (float)(LoginData["results"]["rotate_y"]), (float)(LoginData["results"]["rotate_z"]));
		mMainCharacter.transform.parent = transform;
		mMainCharacter.GetComponent<Client_User>().SetID(mMainCharacter.name);
		mMainCharacter.InitMainPlayer ((string)LoginData["results"]["mesh"],tmpPos,tmpRotate);

		//Init UI
		this.SendEvent (GameEvent.UIEvent.EVENT_UI_SHOW_UI,"UIMainGame");


		//Init CurrentScene
		if (mCurrentActiveScene != null) 
		{
			mCurrentActiveScene.SetActive (false);
		}

		if (tmpProperty.IsCloseOther)
			m_pRelationship.CloseBrother ((string)LoginData["results"]["scene"]);

		m_pRelationship.CloseChild ((string)LoginData["results"]["scene"]);

		if (tmpProperty.FatherObject != null) 
		{
			mCurrentActiveScene = tmpProperty.FatherObject;
			mCurrentActiveScene.SetActive(true);
			return mCurrentActiveScene;
		}

		mCurrentActiveScene = Instantiate(Game_Resources_Manager.Singleton ().GetSceneWithID ((string)LoginData["results"]["scene"])) as GameObject;
		mCurrentActiveScene.transform.parent = transform;
		mCurrentActiveScene.transform.localPosition = Vector3.zero;
		mCurrentActiveScene.transform.localRotation = Quaternion.identity;
		mCurrentActiveScene.transform.localScale = Vector3.one;
		tmpProperty.ShowCurrent (mCurrentActiveScene);
		Camera.main.GetComponent<Game_Input_Manager> ().Reset ();
		this.SendEvent (GameEvent.UIEvent.EVENT_UI_HIDE_LOADING_BLOCK,new Struct_Loading_Info(false,tmpProperty.DelayLoadingTime,null));
		yield return new WaitForSeconds (tmpProperty.DelayLoadingTime);

		//return mCurrentActiveScene;
	}
}
