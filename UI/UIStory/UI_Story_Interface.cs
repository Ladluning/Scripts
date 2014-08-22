using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
public class UI_Story_Interface: MonoBehaviour {

    private Camera mStoryCamer = null;
	private List<GameObject> mObjectList = new List<GameObject>();
    private UI_Story_Bubble_Manager mUIBubble = null;
	private Transform mInitPrefabNode = null;



    MethodInfo method_InitPrefab = null;
    MethodInfo method_SetObjectAnimation = null;
    MethodInfo method_SetObjectMove = null;
    MethodInfo method_SetShowActorBubble = null;
    MethodInfo method_SetNextStepTime = null;

	void Awake()
	{
		mUIBubble = gameObject.GetComponentInChildren<UI_Story_Bubble_Manager>();
		mInitPrefabNode = transform.FindChild ("UIStory_Prefab");

		MethodInfo method_InitPrefab = this.GetType().GetMethod("InitPrefab", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string) ,typeof(Vector3),typeof(Vector3)}, null);
		MethodInfo method_SetObjectAnimation = this.GetType().GetMethod("SetObjectAnimation", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string)}, null);
		MethodInfo method_SetObjectMove = this.GetType().GetMethod("SetObjectMove", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(Vector3) ,typeof(Vector3),typeof(float)}, null);
		MethodInfo method_SetShowActorBubble = this.GetType().GetMethod("SetShowActorBubble", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string),typeof(float),typeof(float)}, null);
		MethodInfo method_SetNextStepTime = this.GetType().GetMethod("SetNextStepTime", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(float),typeof(int)}, null);

	}

    public void RegistFile(string FileName)
    {
        LuaManager.Singleton().RegistFile(FileName);

        LuaManager.Singleton().RegistFunction(FileName, "InitPrefab", this, method_InitPrefab);
        LuaManager.Singleton().RegistFunction(FileName, "SetObjectAnimation", this, method_SetObjectAnimation);
        LuaManager.Singleton().RegistFunction(FileName, "SetObjectMove", this, method_SetObjectMove);
        LuaManager.Singleton().RegistFunction(FileName, "SetShowActorBubble", this, method_SetShowActorBubble);
        LuaManager.Singleton().RegistFunction(FileName, "SetNextStepTime", this, method_SetNextStepTime);
    }

    public void StartStory(string FileName)
    {
        LuaManager.Singleton().GetFunction(FileName, "Start").Call();
    }

	void Start()
	{
        
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
				mUIBubble.ShowBubbleWithTarget(mObjectList[i],SaidString,SaidWidth,SaidHeight);

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
