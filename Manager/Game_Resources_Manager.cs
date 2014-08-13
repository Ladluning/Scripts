using UnityEngine;
using System.Collections;

public class Game_Resources_Manager : MonoBehaviour 
{
	private static Game_Resources_Manager m_pInterface;
	public static Game_Resources_Manager Singleton()
	{
		return m_pInterface;
	}
	Game_Resources_Manager()
	{
		m_pInterface = this;
	}

	public GameObject mMainCharacter;
	public GameObject GetCharacterPrefab()
	{
		return mMainCharacter;
	}

	public GameObject[] mActorList;
	public GameObject GetActorWithID(string ActorID)
	{
		for (int i=0; i<mActorList.Length; i++) 
		{
			if(mActorList[i].name == ActorID)
				return mActorList[i];
		}
		return null;
	}

	public GameObject[] mSceneList;
	public GameObject GetSceneWithID(string SceneID)
	{
		for (int i=0; i<mSceneList.Length; i++) 
		{
			if(mSceneList[i].name == SceneID)
				return mSceneList[i];
		}
		return null;
	}
}
