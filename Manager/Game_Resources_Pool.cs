using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Resources_Pool : MonoBehaviour {

	private static Game_Resources_Pool m_pInterface;
	public static Game_Resources_Pool Singleton()
	{
		return m_pInterface;
	}

	Game_Resources_Pool()
	{
		m_pInterface = this;
	}

	void Awake()
	{
		m_pCurrentTransform = transform;
		m_pResourcesManager = Game_Resources_Manager.Singleton ();
	}

	protected Transform m_pCurrentTransform;
	protected Game_Resources_Manager m_pResourcesManager;
	protected List<GameObject> m_pActorList = new List<GameObject>();
	public GameObject GetUnusedActorWithID(string ID)
	{
		foreach (GameObject tmp in m_pActorList) 
		{
			if(tmp.name == ID&&!tmp.activeSelf)
				return tmp;
		}

		m_pActorList.Add (Instantiate(m_pResourcesManager.GetActorWithID(ID)) as GameObject);
		return m_pActorList [m_pActorList.Count - 1];
	}

	public void SetUnusedActor(GameObject Target)
	{
		Target.transform.parent = m_pCurrentTransform;
		Target.SetActive (false);
	}
}
