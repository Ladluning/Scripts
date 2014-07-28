using UnityEngine;
using System.Collections;
using System.Collections.Generic;
class SceneRelationList
{
	public Game_Scene_Property NodeProperty;
	public SceneRelationList NodeParent;
	public List<SceneRelationList> ChildList = new List<SceneRelationList>();
}
public class Game_Scene_Relation : MonoBehaviour {

	SceneRelationList pSceneRelationList = new SceneRelationList();

	void Awake () 
	{
		AddScenePropertyInList(transform, pSceneRelationList);
	}
	
	void AddScenePropertyInList(Transform Node, SceneRelationList pList)
	{
		if(Node.GetComponent<Game_Scene_Property>() != null)
		{
			pList.NodeProperty = Node.GetComponent<Game_Scene_Property>();
		}
		for(int i = 0; i < Node.childCount; i++)
		{
			SceneRelationList Tmp = new SceneRelationList();
			Tmp.NodeParent = pList;
			AddScenePropertyInList(Node.GetChild(i), Tmp);
			pList.ChildList.Add(Tmp);
		}
	}

	public Game_Scene_Property GetScenePropertyWithSceneName(string Name)
	{
		return GetScenePropertyWithName(Name, pSceneRelationList);
	}

	Game_Scene_Property GetScenePropertyWithName(string Name, SceneRelationList pList)
	{
		if(pList.NodeProperty)
		{
			if(pList.NodeProperty.name == Name)
				return pList.NodeProperty;
		}
		foreach(SceneRelationList Tmp in pList.ChildList)
		{
			Game_Scene_Property TmpProperty = GetScenePropertyWithName(Name, Tmp);
			if(TmpProperty != null)
			{
				return TmpProperty;
			}
		}
		return null;
	}

	public void CloseAll()
	{
		CloseSceneObject (pSceneRelationList);
	}

	void CloseSceneObject(SceneRelationList pList)
	{
		if (pList.NodeProperty.FatherObject != null) 
		{
			Destroy(pList.NodeProperty.FatherObject);	
			pList.NodeProperty.FatherObject = null;
		}
		foreach (SceneRelationList Tmp in pList.ChildList) 
		{
			CloseSceneObject(Tmp);	
		}
	}

	public void CloseChild(string Name)
	{
		SceneRelationList tmpRelation = GetSceneRelationWithName (Name,pSceneRelationList);
		foreach (SceneRelationList Tmp in tmpRelation.ChildList) 
		{
			CloseSceneObject(Tmp);	
		}
	}
	public void CloseBrother(string Name)
	{
		SceneRelationList tmpRelation = GetSceneRelationWithName (Name,pSceneRelationList);
		for (int i=0; i<tmpRelation.NodeParent.ChildList.Count; ++i)
		{
			if(tmpRelation == tmpRelation.NodeParent.ChildList[i])
			{
				continue;
			}
			if (tmpRelation.NodeParent.ChildList[i].NodeProperty.FatherObject != null) 
			{
				Destroy(tmpRelation.NodeParent.ChildList[i].NodeProperty.FatherObject);	
				tmpRelation.NodeParent.ChildList[i].NodeProperty.FatherObject = null;
			}
			foreach (SceneRelationList Tmp in tmpRelation.NodeParent.ChildList[i].ChildList) 
			{
				CloseSceneObject(Tmp);	
			}
		}
	}

	SceneRelationList GetSceneRelationWithName(string Name, SceneRelationList pList)
	{
		if(pList.NodeProperty)
		{
			if(pList.NodeProperty.name == Name)
				return pList;
		}
		foreach(SceneRelationList Tmp in pList.ChildList)
		{
			SceneRelationList TmpProperty = GetSceneRelationWithName(Name, Tmp);
			if(TmpProperty != null)
			{
				return TmpProperty;
			}
		}
		return null;
	}
}
