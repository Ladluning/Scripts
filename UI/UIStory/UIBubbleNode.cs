using UnityEngine;
using System.Collections;

public class UIBubbleNode:MonoBehaviour
{
	public GameObject mFather;
	public TweenAlpha mTweenAnimation;
	public UILabel mBubbleLabel; 
	public UISprite mBubbleBack;
	public TypewriterEffect mLabelEffect;
	public float mBubbleTimer = 0;
	public float mBubbleTime = 4;
	private bool mIsDestroy = false;
	public void SetSaid(string Said,float SaidWidth,float SaidHeight)
	{
		mBubbleLabel.text = Said;
		mLabelEffect.enabled = true;
		mBubbleBack.transform.localScale = new Vector3 (SaidWidth,SaidHeight, 1);
		mBubbleTimer = 0;
	}

	public void StartShow()
	{
		mTweenAnimation.Play (true);
	}
	
	public void StartHide()
	{
		mTweenAnimation.Reset ();
		mTweenAnimation.Play (false);
		mBubbleLabel.text = "";
		StartCoroutine (WaitDestroy(0.4f));
	}
	
	IEnumerator WaitDestroy(float WaitTime)
	{
		yield return new WaitForSeconds (WaitTime);
		GameObject.Destroy (gameObject);
	}

	void Update()
	{
		if (mIsDestroy)
			return;

		mBubbleTimer += Time.deltaTime;
		if (mBubbleTimer > mBubbleTime) {
			StartHide();
			mIsDestroy = true;
		}
	}
}