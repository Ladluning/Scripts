using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game_Buff_Base {
	
	// Use this for initialization
	public int    mBuffID;
	public E_Buff_Type mBuffType;
	public string mBuffNick;
	public string mBuffIntroduction;
	public float  mBuffWorkingTime;
	public float  mTimer;
	public bool   mIsDebuff;
	public string mEffectName;
	
	public string GetBuffNick()
	{
		return mBuffNick;
	}

	public bool GetBuffIsOver()
	{
		mTimer -= Time.deltaTime;
		return mTimer < 0;
	}

	public virtual void StartBuff(Game_Actor_Property_Base pTarget,float WorkingTime = -1)
	{
		if(WorkingTime>0)
			mTimer = WorkingTime;
		else
			mTimer = mBuffWorkingTime;
	}	
	
	public virtual void RunBuff(Game_Actor_Property_Base pTarget)
	{

	}
	
	public virtual void EndBuff(Game_Actor_Property_Base pTarget)
	{
		
	}
}