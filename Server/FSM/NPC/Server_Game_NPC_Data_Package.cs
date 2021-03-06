﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
	public class Server_Game_NPC_Data_Package : Server_Game_NPC_Data_Base {

		protected List<Server_Game_User> mConnectList = new List<Server_Game_User>();
		public List<Struct_Item_Base> mItemList = new List<Struct_Item_Base>();
		public List<Struct_Item_Equip> mEquipList = new List<Struct_Item_Equip>();
		public int mPackageSize=20;
		public int mPackageMaxSize=20;
		public override void Init(Server_Game_NPC_Base Father)
		{
			base.Init(Father);

		}
		
		protected virtual void OnEnable()
		{
			this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_REQUEST_NPC_DATA,OnHandleRequestNPCData);
			this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_BUY_ITEM,OnHandleBuyItem);
			this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_SELL_ITEM, OnHandleSellItem);
			this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_EXIT_NPC, OnHandleExitNPC);
			this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_PLAYER,OnHandleDestroyPlayer);
		}
		
		protected virtual void OnDisable()
		{
			this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_REQUEST_NPC_DATA, OnHandleRequestNPCData);
			this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_BUY_ITEM, OnHandleBuyItem);
			this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_SELL_ITEM, OnHandleSellItem);
			this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_EXIT_NPC, OnHandleExitNPC);
			this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_PLAYER, OnHandleDestroyPlayer);
		}
		
		object OnHandleRequestNPCData(object pSender)
		{
			JsonData tmpJson = new JsonData(pSender);
			if ((string)tmpJson ["results"] ["npc"] != mFather.mID)
				return null;
			
			Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_REQUEST_NPC_DATA);
			List<object> tmpItemData = new List<object>();
			for (int i = 0; i < mItemList.Count; i++)
			{
				tmpItemData.Add(Server_Item_Serialize.ConvertItemToJson(mItemList[i]));
			}
			for (int i = 0; i < mEquipList.Count; i++)
			{
				tmpItemData.Add(Server_Item_Serialize.ConvertItemToJson(mEquipList[i]));
			}
			((Dictionary<string, object>)tmpSend["results"]).Add("id", mFather.mID);
			((Dictionary<string, object>)tmpSend["results"]).Add("size",mPackageSize);
			((Dictionary<string, object>)tmpSend["results"]).Add("maxSize", mPackageMaxSize);
			((Dictionary<string, object>)tmpSend["results"]).Add("packages", tmpItemData);

			this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_NPC_PACKAGE, tmpSend);
			
			Server_Game_User tmpUser = mFather.mGameManager.GetServerUserWithID ((string)tmpJson["results"]["id"]);
			if (tmpUser == null) 
			{	
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR, 100));//Error User ID
				return null;    
			}
			if (!mConnectList.Contains (tmpUser))
			{
				mConnectList.Add(tmpUser);
			}
			
			return null;
		}

		object OnHandleSellItem(object pSender)
		{
			JsonData tmpJson = (JsonData)pSender;

			if ((string)tmpJson["results"]["npc"] != mFather.mID)
				return null;

			Server_Game_User tmpUser = mFather.mGameManager.GetServerUserWithID ((string)tmpJson["results"]["id"]);
			if (tmpUser == null) 
			{	
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR, 100));//Error User ID
				return null;    
			}

			Struct_Item_Base tmpTarget = tmpUser.GetPackage().GetItemWithID((long)tmpJson["results"]["target"]);
			if (tmpTarget == null)
			{
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR,701));//Not find Shop Item
				return null;
			}

			int tmpSellCount = (int)tmpJson["results"]["count"];
			if(tmpSellCount > tmpTarget.mCurrentCount)
			{
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR, 702));//Item not Enough
				return null;
			}

			if(tmpSellCount == tmpTarget.mCurrentCount)
			{
				tmpUser.GetPackage().RemoveItemWithStorage(tmpTarget);
			}

			return null;
		}

		object OnHandleBuyItem(object pSender)
		{
			JsonData tmpJson = new JsonData(pSender);
			
			if ((string)tmpJson["results"]["npc"] != mFather.mID)
				return null;
			
			Struct_Item_Base tmpTarget = FindTargetItemWithID((long)tmpJson["results"]["target"]);
			if (tmpTarget == null)
			{
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR,701));//Not find Shop Item
				return null;
			}
			
			int tmpBuyCount = (int)tmpJson["results"]["count"];
			if(tmpBuyCount <= 0)
			{
				Debug.LogWarning(mFather.mID + "--Buy Count: 0");
				return null;
			}
			
			if (tmpTarget.mCurrentCount < tmpBuyCount)
			{
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR, 702));//Item not Enough
				return null;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
			}
			
			Server_Game_User tmpUser = mFather.mGameManager.GetServerUserWithID ((string)tmpJson["results"]["id"]);
			if (tmpUser == null) 
			{	
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR, 100));//Error User ID
				return null;    
			}
			
			if (tmpUser.GetPackage().GetEmptySlotWithStorage () == -1) 
			{
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR, 105));//package full
				return null;   					
			}
			
			Struct_Item_Base tmpInster = tmpTarget.Copy ();
			tmpInster.mCurrentCount = tmpBuyCount;
			tmpUser.GetPackage().InsertItemWithInStorage (tmpInster);
			//InsertItemWithInStorage();
			
			tmpTarget.mCurrentCount -= tmpBuyCount;
			
			Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_NPC_PACKAGE);
			((Dictionary<string, object>)tmpSend["results"]).Add("id", mFather.mID);

			List<object> tmpItemData = new List<object>();
			tmpItemData.Add(Server_Item_Serialize.ConvertItemToJson(tmpTarget));
			((Dictionary<string, object>)tmpSend["results"]).Add("packages", tmpItemData);
			this.SendEvent (GameEvent.WebEvent.EVENT_WEB_RECEIVE_UPDATE_NPC_PACKAGE,tmpSend);
			return null;
		}
		
		protected virtual object OnHandleExitNPC(object pSender)
		{
			JsonData tmpJson = (JsonData)pSender;
			Server_Game_User tmpUser = mFather.mGameManager.GetServerUserWithID ((string)tmpJson["results"]["id"]);
			if (tmpUser == null) 
			{	
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR, 100));//Error User ID
				return null;    
			}
			OnHandleDestroyPlayer (tmpUser);
			return null;
		}
		
		object OnHandleDestroyPlayer(object pSender)
		{
			Server_Game_User tmpUser = (Server_Game_User)pSender;
			if (mConnectList.Contains(tmpUser))
			{
				mConnectList.Remove(tmpUser);
			}
			return null;
		}
		
		Struct_Item_Base FindTargetItemWithID(long ID)
		{
			for (int i = 0; i < mItemList.Count; ++i)
			{
				if (mItemList[i].mItemID == ID)
					return mItemList[i];
			}
			for (int i = 0; i < mEquipList.Count; ++i)
			{
				if (mEquipList[i].mItemID == ID)
					return mEquipList[i];
			}
			return null;
		}
	}
}
