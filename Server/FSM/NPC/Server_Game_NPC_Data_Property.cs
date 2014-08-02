using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
	public class Server_Game_NPC_Data_Property : Server_Game_NPC_Data_Base
	{
		public string mMeshID;
		public int mHP;
		public int mMaxHP;
		public int mMP;
		public int mMaxMP;
		public float mIdleStandTime;
		public float mIdleMoveSpeed;
		public float mMoveSpeed;
		public int mAttack;
		public int mDefend;
		public float mAttackRange;
		public int mEscapeHP;
		public int mEXP;

		public override void Init(Server_Game_NPC_Base Father)
		{
			base.Init(Father);

			tmpSend.Add("id", mFather.mID);
			tmpSend.Add("mesh", mMeshID);
			tmpSend.Add("hp", mHP);
			tmpSend.Add("maxHP", mMaxHP);
			tmpSend.Add("mp", mMP);
			tmpSend.Add("maxMP", mMaxMP);
			tmpSend.Add("attack", mAttack);
			tmpSend.Add("defend", mDefend);
			tmpSend.Add("exp", mEXP);
		}
		
		void LateUpdate()
		{
			UpdateData();
		}
		
		public void UpdateData()
		{
			tmpSend.Clear();
			
			tmpSend["id"]=mFather.mID;
			tmpSend["mesh"]=mMeshID;
			tmpSend["hp"]=mHP;
			tmpSend["maxHP"]=mMaxHP;
			tmpSend["mp"]=mMP;
			tmpSend["maxMP"]=mMP;
			tmpSend["attack"]=mAttack;
			tmpSend["defend"]=mDefend;
			tmpSend["exp"]=mEXP;
		}
	}
}
