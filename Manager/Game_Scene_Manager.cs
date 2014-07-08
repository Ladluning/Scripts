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
		this.RegistEvent (GameEvent.FightingEvent.EVENT_FIGHT_START_LEVEL,OnHandleInitLevel);
	}
	void OnDisable()
	{
		this.UnRegistEvent (GameEvent.FightingEvent.EVENT_FIGHT_START_LEVEL,OnHandleInitLevel);
	}

	object OnHandleInitLevel(object pSender)
	{
//		Struct_Scene_Init TmpReceive = (Struct_Scene_Init)pSender;
//		GameObject TmpTarget = CreateSceneWithName (TmpReceive.TargetScene);
//
//		if (TmpReceive.TargetPassPointID != null || TmpReceive.TargetPassPointID != "") 
//		{
//			TmpTarget.GetComponent<Game_Scene_Controller>().
//		}
		return null;
	}

	GameObject CreateSceneWithName(string Name)
	{
//		if (Name == mCurrentActiveScene.name)
//			return;

		Game_Scene_Property tmpProperty = m_pRelationship.GetScenePropertyWithSceneName (Name);

		if (tmpProperty.IsCloseOther)
						m_pRelationship.CloseAll ();
		else if (mCurrentActiveScene != null) 
		{
			mCurrentActiveScene.SetActive (false);
		}

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
		return mCurrentActiveScene;
	}
}
