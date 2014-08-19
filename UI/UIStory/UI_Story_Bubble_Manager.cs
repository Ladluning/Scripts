using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Story_Bubble_Manager : MonoBehaviour {

	public GameObject mBubblePrefab;
	public List<UI_Story_Bubble_Node> mBubbleList = new List<UI_Story_Bubble_Node> ();
	public void ShowBubbleWithTarget(GameObject target,string Said,float SaidWidth,float SaidHeight)
	{
		UI_Story_Bubble_Node tmpTarget = GetBubbleWithFather (target);

		if (tmpTarget != null) {
			tmpTarget.SetSaid (Said,SaidWidth,SaidHeight);
			tmpTarget.mBubbleTimer = 0f;
			return;
		}

		tmpTarget = (Instantiate (mBubblePrefab) as GameObject).GetComponent<UI_Story_Bubble_Node>();
		tmpTarget.mFather = target;
		tmpTarget.transform.parent = transform;
		tmpTarget.transform.position = target.transform.localPosition + Vector3.up*2;
		tmpTarget.StartShow ();
		tmpTarget.SetSaid (Said,SaidWidth,SaidHeight);

		mBubbleList.Add (tmpTarget);

	}

	UI_Story_Bubble_Node GetBubbleWithFather(GameObject target)
	{
		for (int i=0; i<mBubbleList.Count; ++i) 
		{
			if(mBubbleList[i].mFather == target)
			{
				return mBubbleList[i];
			}
		}
		return null;
	}
}
