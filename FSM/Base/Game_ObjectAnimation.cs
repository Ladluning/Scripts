using UnityEngine;
using System.Collections;

public class Game_ObjectAnimation : Game_ObjectBase {
	Animation mAnimation;
	RegistFunction mCallback;
	string mAnimationName;
	void Awake()
	{
		mAnimation = gameObject.GetComponentInChildren<Animation>();
	}
	public void PlayAnimation(string Name,RegistFunction CallBack = null)
	{
		StopAllCoroutines ();

		if(mAnimation==null)
			return;

		mAnimationName = Name;
		mCallback = CallBack;
		mAnimation.CrossFade (mAnimationName,0.2f);

		if (mCallback!=null&&mAnimation.GetClip (mAnimationName).wrapMode != WrapMode.Loop) 
		{
			StartCoroutine(WaitAnimationEnd(mAnimation.GetClip (mAnimationName).length));
		}
	}

	IEnumerator WaitAnimationEnd(float EndTime)
	{
		yield return new WaitForSeconds (EndTime);

		if(mCallback!=null)
			mCallback (mAnimationName);

		StopAnimation ();
	}

	public void StopAnimation()
	{
		mCallback = null;
		mAnimationName = "";
		StopAllCoroutines ();
		mAnimation.Stop ();
	}

	public bool GetIsPlayAnimation()
	{
		return mAnimationName == "";
	}
}
