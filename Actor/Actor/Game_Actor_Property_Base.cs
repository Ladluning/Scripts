using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Actor_Property_Base : Controller
{
	public Struct_Base_Property_Info mBaseProperty = new Struct_Base_Property_Info();

	public int mLosePropertyCount;//Save
	public int mPropertyAdd_Strength;//Save
	public int mPropertyAdd_Intelligence;//Save
	public int mPropertyAdd_Agility;//Save
	public int mPropertyAdd_Constitution;//Save
	public int mExp;//Save
	public int mLevel;//Save

	public int mMaxLevel;//Load In Lua

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

	//public bool m_bIsLeader = false;

	void Awake()
	{
		mLevel = GetLevel ();
		ResetProperty();
		ResetBuff();
	}
	
	public int GetLevel()
	{
		int tmpLevel = 1;
		int tmpExp = mExp - 50;
		while ((tmpExp/2)>0) 
		{
			tmpLevel += 1;
			tmpExp = tmpExp/2;	
		}
		return tmpLevel;
	}

	public float GetAnimationSpeed()
	{
		return 1;
	}

	public void AddExp(int Value)
	{
		if (mLevel >= mMaxLevel)
			return;

		mExp += Value;
		if (GetLevel () != mLevel) 
		{
			mLevel = GetLevel ();
			ResetProperty();
			ResetBuff();
		}
	}

	public void ResetProperty()
	{
		mMoveSpeed = mBaseProperty.mMoveSpeed;
		mAttackSpeed = mBaseProperty.mAttackValue;
		mDefend = mBaseProperty.mDefendValue;
		mAttack = mBaseProperty.mAttackValue;

		mMaxMP = mPropertyAdd_Intelligence*100+mBaseProperty.mMPValue+mBaseProperty.mMPValue*mLevel/3;
		mMP = mMaxMP;
		mMaxMPRecover = mPropertyAdd_Intelligence*2+mBaseProperty.mMPRecoverValue+mBaseProperty.mMPRecoverValue*mLevel/2;
		mHPRecover = mMaxMPRecover;

		mMaxHP = mPropertyAdd_Constitution*50+mBaseProperty.mHPValue+mBaseProperty.mHPValue*mLevel/3;
		mHP = mMaxHP;
		mMaxHPRecover = mPropertyAdd_Constitution*1+mBaseProperty.mHPRecoverValue+mBaseProperty.mHPRecoverValue*mLevel/2;
		mMPRecover = mMaxHPRecover;
	}

	public void ResetBuff()
	{

	}
}
