﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
	[System.Serializable]
	public class Server_Struct_User_Property : Server_Game_User_Component
	{
		public int mLosePropertyCount;
		public int mPropertyAdd_Strength;
		public int mPropertyAdd_Intelligence;
		public int mPropertyAdd_Agility;
		public int mPropertyAdd_Constitution;
		public int mHP;
		public int mMaxHP;
		public int mHPRecover;
		public int mMaxHPRecover;
		public int mDefend;
		public int mMaxDefend;
		public int mAttack;
		public int mMaxAttack;
		public int mMP;
		public int mMaxMP;
		public int mMPRecover;
		public int mMaxMPRecover;
		public float mAttackSpeed;
		public float mDodge;
		public float mHit;
		public float mCrit;
		public float mMoveSpeed;

		public void CopyTo(Server_Struct_User_Property Target)
		{
			Target.mLosePropertyCount = mLosePropertyCount;
			Target.mPropertyAdd_Strength = mPropertyAdd_Strength;
			Target.mPropertyAdd_Intelligence = mPropertyAdd_Intelligence;
			Target.mPropertyAdd_Agility = mPropertyAdd_Agility;
			Target.mPropertyAdd_Constitution = mPropertyAdd_Constitution;
			Target.mHP = mHP;
			Target.mMaxHP = mMaxHP;
			Target.mHPRecover = mHPRecover;
			Target.mMaxHPRecover = mMaxHPRecover;
			Target.mDefend = mDefend;
			Target.mMaxDefend = mMaxDefend;
			Target.mAttack = mAttack;
			Target.mMaxAttack = mMaxAttack;
			Target.mMP = mMP;
			Target.mMaxMP = mMaxMP;
			Target.mMPRecover = mMPRecover;
			Target.mMaxMPRecover = mMaxMPRecover;
			Target.mAttackSpeed = mAttackSpeed;
			Target.mDodge = mDodge;
			Target.mHit = mHit;
			Target.mCrit = mCrit;
			Target.mMoveSpeed = mMoveSpeed;
		}
	}

	public class Server_Game_User_Property : Server_Game_User_Component {

		public Server_Struct_User_Property mOriginProperty = new Server_Struct_User_Property();
		public Server_Struct_User_Property mCurrentProperty = new Server_Struct_User_Property();
		public string mName;
		public string mMeshID;
		public ulong mExp;
		public int mLevel;
		public int mMaxLevel = 60;
		void OnEnable()
		{
			this.RegistListen(GameEvent.WebEvent.EVENT_WEB_SEND_SWIP_PSTORAGE_ITEM,OnListenSwipItem);
		}

		void OnDisable()
		{
			this.UnRegistListen(GameEvent.WebEvent.EVENT_WEB_SEND_SWIP_PSTORAGE_ITEM,OnListenSwipItem);
		}

		public override void Init (Server_Game_User Father)
		{
			base.Init (Father);

			mOriginProperty.mLosePropertyCount = mUser.mDataInfo.LosePropertyCount;
			mOriginProperty.mPropertyAdd_Strength = mUser.mDataInfo.PropertyAdd_Strength;
			mOriginProperty.mPropertyAdd_Intelligence = mUser.mDataInfo.PropertyAdd_Intelligence;
			mOriginProperty.mPropertyAdd_Agility = mUser.mDataInfo.PropertyAdd_Agility;
			mOriginProperty.mPropertyAdd_Constitution = mUser.mDataInfo.PropertyAdd_Constitution;
			mMeshID = mUser.mDataInfo.MeshID;
			mExp = mUser.mDataInfo.EXP;
			mLevel = GetLevel();
			mMaxLevel = mUser.mDataInfo.MaxLevel;

			ApplyCurrentProperty();
		}

		private void ApplyOriginProperty()
		{
			mOriginProperty.mHP = 100+mOriginProperty.mPropertyAdd_Constitution*20+mLevel*10;
			mOriginProperty.mMaxHP = mOriginProperty.mHP;
			mOriginProperty.mHPRecover = 10+mOriginProperty.mPropertyAdd_Constitution;
			mOriginProperty.mMaxHPRecover = mOriginProperty.mHPRecover;

			mOriginProperty.mMP = 50+mOriginProperty.mPropertyAdd_Intelligence*10+mLevel*5;
			mOriginProperty.mMaxMP = mOriginProperty.mMP;
			mOriginProperty.mMPRecover = 5+mOriginProperty.mPropertyAdd_Intelligence/2;
			mOriginProperty.mMaxMPRecover = mOriginProperty.mMPRecover;

			mOriginProperty.mDefend = (int)(10+mOriginProperty.mPropertyAdd_Constitution*2+mLevel+mOriginProperty.mPropertyAdd_Strength*0.5f);
			mOriginProperty.mMaxDefend = mOriginProperty.mDefend;

			mOriginProperty.mAttack = (int)(10+mLevel+mOriginProperty.mPropertyAdd_Strength*2.5f);
			mOriginProperty.mMaxAttack = mOriginProperty.mAttack;

			mOriginProperty.mAttackSpeed = 1+mLevel+mOriginProperty.mPropertyAdd_Agility*0.05f;
			mOriginProperty.mMoveSpeed = 1;
			mOriginProperty.mDodge = 0.03f+mLevel+mOriginProperty.mPropertyAdd_Agility*0.0005f;
			mOriginProperty.mHit = 0.7f+mLevel+mOriginProperty.mPropertyAdd_Agility*0.003f;
			mOriginProperty.mCrit = 0.05f+mLevel+mOriginProperty.mPropertyAdd_Agility*0.0005f;
		}

		private void ApplyCurrentProperty()
		{
			ApplyOriginProperty();
			mOriginProperty.CopyTo(mCurrentProperty);

			mUser.GetPackage().ApplyEquipment();
			//mUser.GetBuff().ApplyBuff();
		}

		public override void UpdateData()
		{
			mUser.mDataInfo.LosePropertyCount = mOriginProperty.mLosePropertyCount;
			mUser.mDataInfo.PropertyAdd_Strength = mOriginProperty.mPropertyAdd_Strength;
			mUser.mDataInfo.PropertyAdd_Intelligence = mOriginProperty.mPropertyAdd_Intelligence;
			mUser.mDataInfo.PropertyAdd_Agility = mOriginProperty.mPropertyAdd_Agility;
			mUser.mDataInfo.PropertyAdd_Constitution = mOriginProperty.mPropertyAdd_Constitution;
			mUser.mDataInfo.MeshID = mMeshID;
			mUser.mDataInfo.EXP = mExp;
			mUser.mDataInfo.MaxLevel = mMaxLevel;
		}

		public int GetLevel()
		{
			int tmpLevel = 1;
			ulong tmpExp = mExp - 50;
			while ((tmpExp / 2) > 0)
			{
				tmpLevel += 1;
				tmpExp = tmpExp / 2;
			}
			return tmpLevel;
		}

		public Dictionary<string, object> RequireUserData()
		{
			Dictionary<string, object> tmpSend = SerializeUserData();
			((Dictionary<string, object>)tmpSend["results"]).Add("id", mUser.mID);
			this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_DATA, tmpSend);
			Debug.Log (Facade.Singleton().GetHashCode()+" "+GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_DATA);
			return tmpSend;
		}

		Dictionary<string, object> SerializeUserData()
		{
			Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_DATA);

			((Dictionary<string, object>)tmpSend["results"]).Add("propertyCount",mCurrentProperty.mLosePropertyCount);
			((Dictionary<string, object>)tmpSend["results"]).Add("property_Str",mCurrentProperty.mPropertyAdd_Strength);
			((Dictionary<string, object>)tmpSend["results"]).Add("property_Int",mCurrentProperty.mPropertyAdd_Intelligence);
			((Dictionary<string, object>)tmpSend["results"]).Add("property_Agi",mCurrentProperty.mPropertyAdd_Agility);
			((Dictionary<string, object>)tmpSend["results"]).Add("property_Con",mCurrentProperty.mPropertyAdd_Constitution);
			((Dictionary<string, object>)tmpSend["results"]).Add("exp",mExp);
			((Dictionary<string, object>)tmpSend["results"]).Add("maxLv",mMaxLevel);
			((Dictionary<string, object>)tmpSend["results"]).Add("hp",mCurrentProperty.mHP);
			((Dictionary<string, object>)tmpSend["results"]).Add("maxHP",mCurrentProperty.mMaxHP);
			((Dictionary<string, object>)tmpSend["results"]).Add("hpRecover",mCurrentProperty.mHPRecover);
			((Dictionary<string, object>)tmpSend["results"]).Add("mesh",mMeshID);
			((Dictionary<string, object>)tmpSend["results"]).Add("defend",mCurrentProperty.mDefend);
			((Dictionary<string, object>)tmpSend["results"]).Add("maxDefend",mCurrentProperty.mMaxDefend);
			((Dictionary<string, object>)tmpSend["results"]).Add("attack",mCurrentProperty.mAttack);
			((Dictionary<string, object>)tmpSend["results"]).Add("maxAttack",mCurrentProperty.mMaxAttack);
			((Dictionary<string, object>)tmpSend["results"]).Add("mp",mCurrentProperty.mMP);
			((Dictionary<string, object>)tmpSend["results"]).Add("maxMP",mCurrentProperty.mMaxMP);
			((Dictionary<string, object>)tmpSend["results"]).Add("mpRecover",mCurrentProperty.mMPRecover);
			((Dictionary<string, object>)tmpSend["results"]).Add("attackSpeed",mCurrentProperty.mAttackSpeed);
			((Dictionary<string, object>)tmpSend["results"]).Add("dodge",mCurrentProperty.mDodge);
			((Dictionary<string, object>)tmpSend["results"]).Add("hit",mCurrentProperty.mHit);
			((Dictionary<string, object>)tmpSend["results"]).Add("crit",mCurrentProperty.mCrit);
			((Dictionary<string, object>)tmpSend["results"]).Add("moveSpeed",mCurrentProperty.mMoveSpeed);
			return tmpSend;
		}

		object OnListenSwipItem(object pSender)
		{
			JsonData tmpJson = new JsonData(pSender);
			if((string)tmpJson["results"]["id"]!=mUser.mID)
				return null;

			if((int)tmpJson["results"]["current"]<100||(int)(int)tmpJson["results"]["target"]<100)
			{
				ApplyCurrentProperty();
				RequireUserData();
			}

			return null;
		}

		public override void SerializeVisibleData(ref Dictionary<string, object> Father)
		{
			if(!GetIsChanged())
				return;
			
			Dictionary<string, object> tmpSend = new Dictionary<string, object>();
			tmpSend.Add("hp", mCurrentProperty.mHP);
			tmpSend.Add("maxHP", mCurrentProperty.mMaxHP);
			tmpSend.Add("mp", mCurrentProperty.mMP);
			tmpSend.Add("maxMP", mCurrentProperty.mMaxMP);
			tmpSend.Add("exp", mExp);
			tmpSend.Add("name", mName);
			tmpSend.Add("id", mUser.mID);
			tmpSend.Add("mesh", mMeshID);
			Father.Add("property",tmpSend);
		}

		public override void Update()
		{

		}

		float mLastTime = 0;
		private void UdpateRecover()
		{
			if(Time.realtimeSinceStartup-mLastTime<5f)
				return;


			if(mCurrentProperty.mHP<mCurrentProperty.mMaxHP)
			{
				mCurrentProperty.mHP = Mathf.Clamp(mCurrentProperty.mHP+(int)((float)mCurrentProperty.mHPRecover*((Time.realtimeSinceStartup-mLastTime)/5f)),0,mCurrentProperty.mMaxHP);
			}

			if(mCurrentProperty.mMP<mCurrentProperty.mMaxMP)
			{
				mCurrentProperty.mMP = Mathf.Clamp(mCurrentProperty.mMP+(int)((float)mCurrentProperty.mMPRecover*((Time.realtimeSinceStartup-mLastTime)/5f)),0,mCurrentProperty.mMaxMP);
			}
			mLastTime = Time.realtimeSinceStartup;

			Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_DATA);
			((Dictionary<string, object>)tmpSend["results"]).Add("id", mUser.mID);
			((Dictionary<string, object>)tmpSend["results"]).Add("hp",mCurrentProperty.mHP);
			((Dictionary<string, object>)tmpSend["results"]).Add("mp",mCurrentProperty.mMP);
			this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UDPATE_USER_DATA, tmpSend);
		}

	}
}
