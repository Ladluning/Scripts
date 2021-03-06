﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client_Property : Controller,Client_Component
{
    public int mLosePropertyCount;
    public int mPropertyAdd_Strength;
    public int mPropertyAdd_Intelligence;
    public int mPropertyAdd_Agility;
    public int mPropertyAdd_Constitution;
    public int mExp;
    public int mLevel;
    public int mMaxLevel;
    public int mHP;
    public int mMaxHP;
    public int mHPRecover;
    public int mDefend;
    public int mMaxDefend;
    public int mAttack;
    public int mMaxAttack;
    public int mMP;
    public int mMaxMP;
    public int mMPRecover;
    public float mAttackSpeed;
    public float mDodge;
    public float mHit;
    public float mCrit;
    public float mMoveSpeed;
	public int mLuck = 10;

	void OnEnable()
	{
		this.RegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_DATA,OnHandleReceiveUpdateProperty);
		Debug.Log (Facade.Singleton().GetHashCode()+" "+GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_DATA);
	}

	void OnDisable()
	{
		this.UnRegistEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_INIT_USER_DATA,OnHandleReceiveUpdateProperty);
	}

	void Awake()
	{

	}

	public void Init()
	{
		Dictionary<string, object> tmpSend = SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_USER_DATA);
		((Dictionary<string, object>)tmpSend["results"]).Add("id", Client_User.Singleton().GetID());
		this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_INIT_USER_DATA,tmpSend);
	}

    public int GetLevel()
    {
        int tmpLevel = 1;
        int tmpExp = mExp - 50;
        while ((tmpExp / 2) > 0)
        {
            tmpLevel += 1;
            tmpExp = tmpExp / 2;
        }
        return tmpLevel;
    }

    public float GetAnimationSpeed()
    {
        return 1;
    }

    object OnHandleReceiveUpdateProperty(object pSender)
    {
        JsonData tmpJson = new JsonData(pSender);
        if ((string)tmpJson["results"]["id"] != gameObject.name)
            return null;

        Debug.Log(MiniJSON.Json.Serialize(pSender));
        mLosePropertyCount = (int)tmpJson["results"]["propertyCount"];
        mPropertyAdd_Strength = (int)tmpJson["results"]["property_Str"];
        mPropertyAdd_Intelligence = (int)tmpJson["results"]["property_Int"];
        mPropertyAdd_Agility = (int)tmpJson["results"]["property_Agi"];
        mPropertyAdd_Constitution = (int)tmpJson["results"]["property_Con"];
        mExp = (int)tmpJson["results"]["exp"];
        mLevel = GetLevel();
        mMaxLevel = (int)tmpJson["results"]["maxLv"];
        mHP = (int)tmpJson["results"]["hp"];
        mMaxHP = (int)tmpJson["results"]["maxHP"];
        mHPRecover = (int)tmpJson["results"]["hpRecover"];
        mDefend = (int)tmpJson["results"]["defend"];
        mMaxDefend = (int)tmpJson["results"]["maxDefend"];
        mAttack = (int)tmpJson["results"]["attack"];
        mMaxAttack = (int)tmpJson["results"]["maxAttack"];
        mMP = (int)tmpJson["results"]["mp"];
        mMaxMP = (int)tmpJson["results"]["maxMP"];
        mMPRecover = (int)tmpJson["results"]["mpRecover"];
        mAttackSpeed = (float)tmpJson["results"]["attackSpeed"];
        mDodge = (float)tmpJson["results"]["dodge"];
        mHit = (float)tmpJson["results"]["hit"];
        mCrit = (float)tmpJson["results"]["crit"];
        mMoveSpeed = (float)tmpJson["results"]["moveSpeed"];
        return null;
    }
}
