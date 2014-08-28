using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
public class UI_Story_Interface: Controller {

    private Camera mStoryCamer = null;
	private List<GameObject> mObjectList = new List<GameObject>();
    private UI_Story_Bubble_Manager mUIBubble = null;
	private Transform mInitPrefabNode = null;



    MethodInfo method_InitPrefab = null;
    MethodInfo method_SetObjectAnimation = null;
    MethodInfo method_SetObjectMove = null;
    MethodInfo method_SetShowActorBubble = null;
    MethodInfo method_SetNextStepTime = null;
	MethodInfo method_SetCameraPos = null;
	MethodInfo method_SetStoryEnd = null;
	void Awake()
	{
		mUIBubble = gameObject.GetComponentInChildren<UI_Story_Bubble_Manager>();
		mInitPrefabNode = transform.FindChild ("UIStory_Prefab");

		method_InitPrefab = this.GetType().GetMethod("InitPrefab", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string) ,typeof(Vector3),typeof(Vector3)}, null);
		method_SetObjectAnimation = this.GetType().GetMethod("SetObjectAnimation", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string),typeof(bool)}, null);
		method_SetObjectMove = this.GetType().GetMethod("SetObjectMove", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(Vector3) ,typeof(Vector3),typeof(float)}, null);
		method_SetShowActorBubble = this.GetType().GetMethod("SetShowActorBubble", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string),typeof(float),typeof(float)}, null);
		method_SetNextStepTime = this.GetType().GetMethod("SetNextStepTime", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(string),typeof(float),typeof(int)}, null);
		method_SetCameraPos = this.GetType().GetMethod("SetCameraPos", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] { typeof(Vector3),typeof(Vector3)}, null);
		method_SetStoryEnd = this.GetType().GetMethod("SetStoryEnd", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance, null, new Type[] {}, null);
	}

    public void RegistFile(string FileName)
    {
        LuaManager.Singleton().RegistFile(FileName);

        LuaManager.Singleton().RegistFunction(FileName, "InitPrefab", this, method_InitPrefab);
        LuaManager.Singleton().RegistFunction(FileName, "SetObjectAnimation", this, method_SetObjectAnimation);
        LuaManager.Singleton().RegistFunction(FileName, "SetObjectMove", this, method_SetObjectMove);
        LuaManager.Singleton().RegistFunction(FileName, "SetShowActorBubble", this, method_SetShowActorBubble);
        LuaManager.Singleton().RegistFunction(FileName, "SetNextStepTime", this, method_SetNextStepTime);
		LuaManager.Singleton().RegistFunction(FileName, "SetCameraPos", this, method_SetCameraPos);
		LuaManager.Singleton().RegistFunction(FileName, "SetStoryEnd", this, method_SetStoryEnd);
    }

    public void StartStory(string FileName)
    {
		Debug.Log (LuaManager.Singleton().GetFunction(FileName, "Start"));
        LuaManager.Singleton().GetFunction(FileName, "Start").Call();
    }

	void Start()
	{
        
	}

	public void SetCameraPos(Vector3 Pos,Vector3 Rotate)
	{
		Game_Camera_Fade_Manager.Singleton ().GetCameraWithType (E_Camera_Type.Story).transform.position = Pos;
		Game_Camera_Fade_Manager.Singleton ().GetCameraWithType (E_Camera_Type.Story).transform.eulerAngles = Rotate;
	}

	public void SetCameraMove(Vector3 StartPos,Vector3 EndPos,float MoveSpeed)
	{

	}

	public void InitPrefab(string name,string MeshID,Vector3 Pos,Vector3 Rotate)
	{
		GameObject tmpTarget = Instantiate( Resources.Load("Story/"+MeshID)) as GameObject;
		tmpTarget.name = name;
		tmpTarget.transform.position = Pos;
		tmpTarget.transform.localEulerAngles = Rotate;
		mObjectList.Add (tmpTarget);
	}

	public void DestroyPrefab(string name,float FadeTime)
	{

	}

	public void SetObjectAnimation(string name,string AniName,bool IsLoop)
	{
		GameObject tmpTarget = GetTargetWithName (name);
		if(tmpTarget == null||(tmpTarget!=null&&tmpTarget.GetComponentInChildren<Animation>()==null))
			return; 
		Debug.Log (tmpTarget.GetComponentInChildren<Animation> ().GetClip (AniName));
		//if (IsLoop)
		//	tmpTarget.GetComponentInChildren<Animation> ().GetClip (AniName).wrapMode = WrapMode.Loop;
		//else
		//	tmpTarget.GetComponentInChildren<Animation> ().GetClip (AniName).wrapMode = WrapMode.Once;
		tmpTarget.GetComponentInChildren<Animation>().CrossFade (AniName,0.1f);
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

    public void SetNextStepTime(string FileName, float StepTime, int TargetStep)
	{
        StartCoroutine(NextStep(FileName,StepTime, TargetStep));
	}

	IEnumerator NextStep(string FileName,float WaitTime,int TargetStep)
	{
		yield return new WaitForSeconds (WaitTime);
		LuaManager.Singleton().GetFunction(FileName, "MoveNext").Call(TargetStep);
	}

	public void SetShowStoryUI(string IconID)
	{

	}

	public void SetStorySaid(string Icon,string SaidString)
	{

	}

	public void SetStoryEnd()
	{
		StopAllCoroutines ();
		ClearAllObject ();
		this.SendEvent (GameEvent.FightingEvent.EVENT_FIGHT_END_STORY,null);
	}

	public void ClearAllObject()
	{
		for (int i=0; i<mObjectList.Count; ++i) 
		{
			Destroy(mObjectList[i]);
		}
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
