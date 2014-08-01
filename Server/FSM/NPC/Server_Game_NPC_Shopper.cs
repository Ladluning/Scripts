using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_NPC_Shopper : Server_Game_NPC_Base
    {
        protected List<Server_Game_User> mConnectList = new List<Server_Game_User>();
        public List<Struct_Item_Base> mItemList = new List<Struct_Item_Base>();
        public List<Struct_Item_Equip> mEquipList = new List<Struct_Item_Equip>();
        public override void Init()
        {
            mController = gameObject.AddComponent<Server_Game_FSM_NPC_Shopper_Controller>();
            mController.Init(this);
			InitNPC ();

            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_NPC, this);
        }

        protected virtual void OnEnable()
        {
            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_REQUEST_NPC_DATA,OnHandleRequestNPCData);
            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_BUY_ITEM,OnHandleBuyItem);
            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_EXIT_NPC, OnHandleExitNPC);
            this.RegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_PLAYER,OnHandleDestroyPlayer);
        }

        protected virtual void OnDisable()
        {
            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_REQUEST_NPC_DATA, OnHandleRequestNPCData);
            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_BUY_ITEM, OnHandleBuyItem);
            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_EXIT_NPC, OnHandleExitNPC);
            this.UnRegistEvent(GameEvent.FightingEvent.EVENT_FIGHT_DESTROY_PLAYER, OnHandleDestroyPlayer);
        }

        object OnHandleRequestNPCData(object pSender)
        {
			JsonData tmpJson = (JsonData)pSender;
			if ((string)tmpJson ["results"] ["npc"] != mNPCID)
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
            ((Dictionary<string, object>)tmpSend["results"]).Add("target", mNPCID);
            ((Dictionary<string, object>)tmpSend["results"]).Add("packages", tmpItemData);

            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_REQUEST_NPC_DATA, tmpItemData);

			Server_Game_User tmpUser = mGameManager.GetServerUserWithID ((string)tmpJson["results"]["id"]);
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

        object OnHandleBuyItem(object pSender)
        {
            JsonData tmpJson = (JsonData)pSender;

            if ((string)tmpJson["results"]["npc"] != mNPCID)
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
                Debug.LogWarning(mNPCID + "--Buy Count: 0");
                return null;
            }

            if (tmpTarget.mCurrentCount < tmpBuyCount)
            {
                this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR, 702));//Item not Enough
                return null;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
            }

			Server_Game_User tmpUser = mGameManager.GetServerUserWithID ((string)tmpJson["results"]["id"]);
			if (tmpUser == null) 
			{	
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR, 100));//Error User ID
				return null;    
			}

			if (tmpUser.GetEmptySlotWithStorage () == -1) 
			{
				this.SendEvent(GameEvent.SysEvent.EVENT_SYS_ERROR, ServerCommand.NewError(GameEvent.SysEvent.EVENT_SYS_ERROR, 105));//package full
				return null;   					
			}

			Struct_Item_Base tmpInster = tmpTarget.Copy ();
			tmpInster.mCurrentCount = tmpBuyCount;
			tmpUser.InsertItemWithInStorage (tmpInster);
            //InsertItemWithInStorage();

            tmpTarget.mCurrentCount -= tmpBuyCount;

            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_Add_USER_STORAGE);

            return null;
        }

        protected virtual object OnHandleExitNPC(object pSender)
        {
			JsonData tmpJson = (JsonData)pSender;
			Server_Game_User tmpUser = mGameManager.GetServerUserWithID ((string)tmpJson["results"]["id"]);
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
