using UnityEngine;
using System.Collections;
using System.Collections.Generic;
class UIRelationList
{
	public UIProperty NodeProperty;
	public List<UIRelationList> ChildList = new List<UIRelationList>();
}
public class UIRelationship : MonoBehaviour {

	// Use this for initialization
	UIRelationList pUIRelationList = new UIRelationList();
	
	void Awake () 
	{
		AddUIPropertyInList(transform, pUIRelationList);
		//Debug.Log(pUIRelationList.ChildList[0].ChildList[0].NodeProperty.name);
	}
	
	void AddUIPropertyInList(Transform Node, UIRelationList pList)
	{
		if(Node.GetComponent<UIProperty>() != null)
		{
			pList.NodeProperty = Node.GetComponent<UIProperty>();
		}
		for(int i = 0; i < Node.childCount; i++)
		{
			UIRelationList Tmp = new UIRelationList();
			AddUIPropertyInList(Node.GetChild(i), Tmp);
			pList.ChildList.Add(Tmp);
		}
	}
	
	public UIProperty GetUIPropertyWithUIName(string Name)
	{
		//Debug.Log(Name+" ");
		return GetUIPropertyWithName(Name, pUIRelationList);
	}

	UIProperty GetUIPropertyWithName(string Name, UIRelationList pList)
	{
		//Debug.Log(Name+" "+pList.NodeProperty);
		if(pList.NodeProperty)
		{
			if(pList.NodeProperty.name == Name)
				return pList.NodeProperty;
		}
		foreach(UIRelationList Tmp in pList.ChildList)
		{
			UIProperty TmpProperty = GetUIPropertyWithName(Name, Tmp);
			if(TmpProperty != null)
			{
				return TmpProperty;
			}
		}
		return null;
	}

}
