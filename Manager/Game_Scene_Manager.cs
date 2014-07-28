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
		StartCoroutine(CreateSceneWithName ((string)pSender));
//
//		if (TmpReceive.TargetPassPointID != null || TmpReceive.TargetPassPointID != "") 
//		{
//			TmpTarget.GetComponent<Game_Scene_Controller>().
//		}
		return null;
	}

	IEnumerator CreateSceneWithName(string Name)
	{
//		if (Name == mCurrentActiveScene.name)
//			return;
		Debug.Log (gameObject.name + " "+m_pRelationship);
		Game_Scene_Property tmpProperty = m_pRelationship.GetScenePropertyWithSceneName (Name);
		this.SendEvent (GameEvent.UIEvent.EVENT_UI_SHOW_LOADING_BLOCK,new Struct_Loading_Info(true,tmpProperty.DelayLoadingTime,null));
		yield return new WaitForSeconds (tmpProperty.DelayLoadingTime);

		if (mCurrentActiveScene != null) 
		{
			mCurrentActiveScene.SetActive (false);
		}

		if (tmpProperty.IsCloseOther)
			m_pRelationship.CloseBrother (Name);

		m_pRelationship.CloseChild (Name);

		if (tmpProperty.FatherObject != null) 
		{
			mCurrentActiveScene = tmpProperty.FatherObject;
			mCurrentActiveScene.SetActive(true);
			return mCurrentActiveScene;
		}

		mCurrentActiveScene = Instantiate(Game_Resources_Manager.Singleton ().GetSceneWithID (Name)) as GameObject;
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
