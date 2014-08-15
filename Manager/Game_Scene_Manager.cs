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
		return null;
	}

	IEnumerator CreateSceneWithName(JsonData LoginData)
	{
		Debug.Log (gameObject.name + " "+m_pRelationship);
		Game_Scene_Property tmpProperty = m_pRelationship.GetScenePropertyWithSceneName ((string)LoginData["results"]["scene"]);
		this.SendEvent (GameEvent.UIEvent.EVENT_UI_SHOW_LOADING_BLOCK,new Struct_Loading_Info(true,tmpProperty.DelayLoadingTime,null));
		this.SendEvent (GameEvent.InputEvent.EVENT_INPUT_STOP_INPUT,null);
		yield return new WaitForSeconds (tmpProperty.DelayLoadingTime);

		//Init MainCharacter
		if(mMainCharacter==null)
			mMainCharacter = (Instantiate (Game_Resources_Manager.Singleton().GetCharacterPrefab()) as GameObject).GetComponent<Game_FSM_MainPlayer_Controller>();

		mMainCharacter.name = (string)LoginData["results"]["id"];
		Vector3 tmpPos = new Vector3((float)(LoginData["results"]["pos_x"]), (float)(LoginData["results"]["pos_y"]), (float)(LoginData["results"]["pos_z"]));
		Vector3 tmpRotate = new Vector3((float)(LoginData["results"]["rotate_x"]), (float)(LoginData["results"]["rotate_y"]), (float)(LoginData["results"]["rotate_z"]));
		mMainCharacter.transform.parent = transform;
		mMainCharacter.InitMainPlayer ((string)LoginData["results"]["mesh"],tmpPos,tmpRotate);
		mMainCharacter.GetComponent<Client_User>().InitWithID(mMainCharacter.name);
		//Init UI
		this.SendEvent (GameEvent.UIEvent.EVENT_UI_SHOW_UI,"UIMainGame");

		//Init Scene
		InitSceneWithName(tmpProperty,(string)LoginData["results"]["scene"]);

		Camera.main.GetComponent<Game_Input_Manager> ().Reset ();

		this.SendEvent (GameEvent.InputEvent.EVENT_INPUT_RESUME_INPUT,null);
		this.SendEvent (GameEvent.UIEvent.EVENT_UI_HIDE_LOADING_BLOCK,new Struct_Loading_Info(false,tmpProperty.DelayLoadingTime,null));
		yield return new WaitForSeconds (tmpProperty.DelayLoadingTime);

		//return mCurrentActiveScene;
	}

	void InitSceneWithName(Game_Scene_Property Property,string SceneName)
	{
		if (mCurrentActiveScene != null && SceneName == mCurrentActiveScene.name)
			return;

		//Init CurrentScene
		if (mCurrentActiveScene != null) 
		{
			mCurrentActiveScene.SetActive (false);
		}
		
		if (Property.IsCloseOther)
			m_pRelationship.CloseBrother (SceneName);
		
		m_pRelationship.CloseChild (SceneName);
		
		if (Property.FatherObject != null) 
		{
			mCurrentActiveScene = Property.FatherObject;
			mCurrentActiveScene.SetActive(true);
		}
		
		mCurrentActiveScene = Instantiate(Game_Resources_Manager.Singleton ().GetSceneWithID (SceneName)) as GameObject;
		mCurrentActiveScene.name = SceneName;
		mCurrentActiveScene.transform.parent = transform;
		mCurrentActiveScene.transform.localPosition = Vector3.zero;
		mCurrentActiveScene.transform.localRotation = Quaternion.identity;
		mCurrentActiveScene.transform.localScale = Vector3.one;
		Property.ShowCurrent (mCurrentActiveScene);
	}
}
