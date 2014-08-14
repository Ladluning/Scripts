using UnityEngine;
using System.Collections;
namespace Server
{
	public enum E_Buff_State
	{

	}
	[System.Serializable]
	public class Server_Game_Buff_Base {

		static public uint BuffID = 0;

		public E_Buff_Type mBuffType;
		public int    mBuffID;
		public float  mBuffTime;
		public float  mTimer;
		public bool   mIsDebuff;

		public bool GetBuffIsOver()
		{
			mTimer -= Time.deltaTime;
			return mTimer < 0;
		}

		public virtual void StartBuff(object pTarget)
		{
			mBuffID = (int)Server_Game_Buff_Base.BuffID++;
			mBuffTime  = 5f;
			mIsDebuff = false;

			ApplyBuff(pTarget);
		}	

		public virtual void ApplyBuff(object pTarget)
		{

		}

		public virtual void RunBuff(object pTarget)
		{

		}
		
		public virtual void EndBuff(object pTarget)
		{
			
		}
	}
}