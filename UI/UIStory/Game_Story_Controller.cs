﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
public class Game_Story_Controller : MonoBehaviour {

	private Camera mStoryCamera;
	private List<GameObject> mObjectList = new List<GameObject>();
	public GameObject mUIStory;
	private UIBubbleManager mUIBubble;

	void Awake()
	{
		LuaManager.Singleton ().RegistFile ("Story_01.txt");


		MethodInfo method_InitPrefab = this.GetType().GetMethod("InitPrefab", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string) ,typeof(Vector3),typeof(Vector3)}, null);
		MethodInfo method_SetObjectAnimation = this.GetType().GetMethod("SetObjectAnimation", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string)}, null);
		MethodInfo method_SetObjectMove = this.GetType().GetMethod("SetObjectMove", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(Vector3) ,typeof(Vector3),typeof(float)}, null);
		MethodInfo method_SetShowActorBubble = this.GetType().GetMethod("SetShowActorBubble", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string),typeof(float),typeof(float)}, null);
		MethodInfo method_SetNextStepTime = this.GetType().GetMethod("SetNextStepTime", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(float),typeof(int)}, null);

		LuaManager.Singleton().RegistFunction("Story_01.txt", "InitPrefab", this, method_InitPrefab);
		LuaManager.Singleton().RegistFunction("Story_01.txt", "SetObjectAnimation",this,method_SetObjectAnimation);
		LuaManager.Singleton().RegistFunction("Story_01.txt", "SetObjectMove", this, method_SetObjectMove);
		LuaManager.Singleton().RegistFunction("Story_01.txt", "SetShowActorBubble", this, method_SetShowActorBubble);
		LuaManager.Singleton().RegistFunction("Story_01.txt", "SetNextStepTime", this, method_SetNextStepTime);

	}

	void Start()
	{
        LuaManager.Singleton().GetFunction("Story_01.txt", "Start").Call();
	}

	public void SetCameraPos(Vector3 Pos,Vector3 Rotate)
	{

	}

	public void SetCameraMove(Vector3 StartPos,Vector3 EndPos,float MoveSpeed)
	{

	}

	public void InitPrefab(string name,string MeshID,Vector3 Pos,Vector3 Rotate)
	{
		GameObject tmpTarget = Instantiate( Resources.Load("Prefab/Hero/"+MeshID)) as GameObject;
		tmpTarget.name = name;
		tmpTarget.transform.position = Pos;
		tmpTarget.transform.localEulerAngles = Rotate;
		mObjectList.Add (tmpTarget);
	}

	public void DestroyPrefab(string name,float FadeTime)
	{

	}

	public void SetObjectAnimation(string name,string AniName)
	{
		GameObject tmpTarget = GetTargetWithName (name);
		if(tmpTarget == null||(tmpTarget!=null&&tmpTarget.animation==null))
			return; 

		tmpTarget.animation.CrossFade (AniName,0.1f);
	}

	public void SetObjectMove(string name,Vector3 StartPos,Vector3 EndPos,float MoveSpeed)
	{
		GameObject tmpTarget = GetTargetWithName (name);
		if(tmpTarget == null)
			return;

		tmpTarget.transform.position = StartPos;
		tmpTarget.transform.LookAt (EndPos);
		iTween.MoveTo(tmpTarget,iTween.Hash("position",EndPos,"time",MoveSpeed,"easetype",iTween.EaseType.linear));
	}


	public void SetPosEffect(Vector3 TargetPos,string EffectName)
	{

	}

	public void SetShowActorBubble(string name,string SaidString,float SaidWidth,float SaidHeight)
	{
		for(int i=0;i<mObjectList.Count;++i)
		{
			if(mObjectList[i].name == name)
			{
				UIBubbleManager.Singleton().ShowBubbleWithTarget(mObjectList[i],SaidString,SaidWidth,SaidHeight);

				return;
			}
		}
	}

	public void SetNextStepTime(float StepTime,int TargetStep)
	{
		StartCoroutine (NextStep(StepTime,TargetStep));
	}

	IEnumerator NextStep(float WaitTime,int TargetStep)
	{
		yield return new WaitForSeconds (WaitTime);
		LuaManager.Singleton().GetFunction("Story_01.txt", "MoveNext").Call(TargetStep);
	}

	public void SetShowStoryUI(string IconID)
	{

	}

	public void SetStorySaid(string Icon,string SaidString)
	{

	}

	public void SetStoryEnd()
	{

	}

	GameObject GetTargetWithName(string Name)
	{
		for(int i=0;i<mObjectList.Count;++i)
		{
			if(mObjectList[i].name == Name)
			{
				return mObjectList[i];
			}
		}
		return null;
	}
}
