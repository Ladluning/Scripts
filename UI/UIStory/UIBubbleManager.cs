using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIBubbleManager : MonoBehaviour {

	private static UIBubbleManager m_pInterface;
	public static UIBubbleManager Singleton()
	{
		return m_pInterface;
	}
	void Awake()
	{
		m_pInterface = this;
	}

	public GameObject mBubblePrefab;
	public List<UIBubbleNode> mBubbleList = new List<UIBubbleNode> ();
	public void ShowBubbleWithTarget(GameObject target,string Said,float SaidWidth,float SaidHeight)
	{
		UIBubbleNode tmpTarget = GetBubbleWithFather (target);

		if (tmpTarget != null) {
			tmpTarget.SetSaid (Said,SaidWidth,SaidHeight);
			tmpTarget.mBubbleTimer = 0f;
			return;
		}

		tmpTarget = (Instantiate (mBubblePrefab) as GameObject).GetComponent<UIBubbleNode>();
		tmpTarget.mFather = target;
		tmpTarget.transform.parent = transform;
		tmpTarget.transform.position = target.transform.localPosition + Vector3.up*2;
		tmpTarget.StartShow ();
		tmpTarget.SetSaid (Said,SaidWidth,SaidHeight);

		mBubbleList.Add (tmpTarget);

	}

	UIBubbleNode GetBubbleWithFather(GameObject target)
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
