using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
	public enum E_Buff_Type
	{
		Buff_Armored_Value,
		Buff_Assault_Value,
		Buff_AttackGrow_Value,
		Buff_AttackMarking_Value,
		Buff_Bang_Value,
		Buff_Blindness_Value,
		Buff_BodyGrow_Value,
	}

	public class Server_Game_User_Buff : Server_Game_User_Component {

		public List<Server_Game_Buff_Base> m_pBuffList = new List<Server_Game_Buff_Base>();
		public List<Server_Game_Buff_Base> m_pTmpRemoveBuffList = new List<Server_Game_Buff_Base>();
		public bool CheckIsActorContainsBuff(E_Buff_Type BuffType)
		{
			foreach(Server_Game_Buff_Base Tmp in m_pBuffList)
			{
				if(Tmp.mBuffType == BuffType)
				{
					return true;
				}
			}
			return false;
		}

		public void AddBuff(E_Buff_Type eBuffType)
		{
			foreach(Server_Game_Buff_Base Tmp in m_pBuffList)
			{
				if(Tmp.mBuffType == eBuffType)
				{	
					Tmp.StartBuff(mUser.GetProperty());
					return;
				}
			}
			
			Server_Game_Buff_Base TargetBuff = GetBuffWithType(eBuffType);
			TargetBuff.StartBuff (mUser.GetProperty());
			m_pBuffList.Add(TargetBuff);
		}
		
		public void RemoveBuff(E_Buff_Type BuffType)
		{
			foreach (Server_Game_Buff_Base Tmp in m_pBuffList) 
			{
				if(BuffType == Tmp.mBuffType)
				{	
					Tmp.EndBuff(mUser.GetProperty());
					m_pBuffList.Remove(Tmp);
					return;
				}
			}
		}
		
		public void RemoveAllBuff()
		{
			for (int i=0;i<m_pBuffList.Count;++i) 
			{
				m_pBuffList[i].EndBuff(mUser.GetProperty());
			}
			m_pBuffList.Clear();	
		}
		
		protected void UpdateBuff()
		{
			for (int i=0;i<m_pBuffList.Count;++i) 
			{
				m_pBuffList[i].RunBuff(this);
				
				if(m_pBuffList[i].GetBuffIsOver())
					m_pTmpRemoveBuffList.Add(m_pBuffList[i]);
			}
			
			if (m_pTmpRemoveBuffList.Count <= 0)
				return;
			
			for (int i=0;i<m_pTmpRemoveBuffList.Count;++i) 
			{
				RemoveBuff(m_pTmpRemoveBuffList[i].mBuffType);
			}
			m_pTmpRemoveBuffList.Clear();
		}

		Server_Game_Buff_Base GetBuffWithType(E_Buff_Type eBuffType)
		{
			switch(eBuffType)
			{
			case E_Buff_Type.Buff_Armored_Value:return new Server_Player_Buff_Armored_Value();
			case E_Buff_Type.Buff_Assault_Value:return new Server_Player_Buff_Assault_Value();
			case E_Buff_Type.Buff_AttackGrow_Value:return new Server_Buff_BodyGrow_Value();
			case E_Buff_Type.Buff_AttackMarking_Value:return new Server_Player_Buff_AttackMarking_Value();
			case E_Buff_Type.Buff_Bang_Value:return new Server_Player_Buff_Bang_Value();
			case E_Buff_Type.Buff_Blindness_Value:return new Server_Player_Buff_Blindness_Value();
			case E_Buff_Type.Buff_BodyGrow_Value:return new Server_Buff_BodyGrow_Value();
			default:return null;
			}
		}

		public void ApplyBuff()
		{

		}
	}
}
