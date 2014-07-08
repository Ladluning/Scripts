using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffAttackSendInfo
{
	public E_Buff_Type mBuffType;
	public float mBuffTime = -1;
}
public class BuffEventInfo
{
	public E_Buff_Type mBuffType;
	public GameObject BuffTarget;
}
public class Game_Actor_Property_Buff : Game_Actor_Property_Base {

	public List<Game_Buff_Base> m_pBuffList = new List<Game_Buff_Base>();
	public List<Game_Buff_Base> m_pTmpRemoveBuffList = new List<Game_Buff_Base>();
	public bool Check_If_Actor_Contains_Buff(E_Buff_Type BuffType)
	{
		foreach(Game_Buff_Base Tmp in m_pBuffList)
		{
			if(Tmp.mBuffType == BuffType)
			{
				return true;
			}
		}
		return false;
	}

	BuffEventInfo SendBuffEvent = new BuffEventInfo();
	public void AddBuff(BuffAttackSendInfo pBuffInfo)
	{
		foreach(Game_Buff_Base Tmp in m_pBuffList)
		{
			if(Tmp.mBuffType == pBuffInfo.mBuffType)
			{	
				Tmp.mBuffWorkingTime = Mathf.Max(Tmp.mTimer,pBuffInfo.mBuffTime);
				return;
			}
		}
		
		Game_Buff_Base TargetBuff = Game_Buff_Manager.Singleton ().GetBuffFunctionWithName (pBuffInfo.mBuffType);
		TargetBuff.StartBuff (this,pBuffInfo.mBuffTime);
		m_pBuffList.Add(TargetBuff);
	}
	
	public void RemoveBuff(E_Buff_Type BuffType)
	{
		foreach (Game_Buff_Base Tmp in m_pBuffList) 
		{
			if(BuffType == Tmp.mBuffType)
			{	
				Tmp.EndBuff(this);
				m_pBuffList.Remove(Tmp);
				return;
			}
		}
		//BuffManager.Singleton().GetBuffFunctionWithName(pBuffInfo.BuffName).GetComponent<BuffBase>().EndBuff(m_pActorSelf);
		//BuffEventInfo TmpEvent = new BuffEventInfo();
		//SendBuffEvent.BuffName = 	pBuffInfo.BuffName;
		//SendBuffEvent.BuffTarget = m_pActorSelf;
		//this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_ACTOR_REMOVE_BUFF,SendBuffEvent);
		

	}
	
	public void RemoveAllBuff()
	{
//		foreach(BuffAttackSendInfo Tmp in m_pBuffList)
//		{
			//BuffEventInfo TmpEvent = new BuffEventInfo();
//			SendBuffEvent.BuffName = 	Tmp.BuffName;
//			SendBuffEvent.BuffTarget = m_pActorSelf;
//			this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_ACTOR_REMOVE_BUFF,SendBuffEvent);			
//		}
		m_pBuffList.Clear();	
	}

	protected void UpdateBuff()
	{
		foreach (Game_Buff_Base Tmp in m_pBuffList) 
		{
			Tmp.RunBuff(this);

			if(Tmp.GetBuffIsOver())
				m_pTmpRemoveBuffList.Add(Tmp);
		}

		if (m_pTmpRemoveBuffList.Count <= 0)
			return;

		foreach (Game_Buff_Base Tmp in m_pTmpRemoveBuffList) 
		{
			RemoveBuff(Tmp.mBuffType);
		}
	}
}
