using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Server
{
    public class Server_Game_NPC_Shopper : Server_Game_NPC_Base
    {

        public List<Struct_Item_Base> mItemList = new List<Struct_Item_Base>();
        public List<Struct_Item_Equip> mEquipList = new List<Struct_Item_Equip>();
        public override void Init()
        {
            mController = gameObject.AddComponent<Server_Game_FSM_NPC_Shopper_Controller>();
            mController.Init(this);

            base.Init();

            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_NPC, this);
        }

        protected virtual void OnEnable()
        {
            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_REQUEST_NPC_DATA,OnHandleRequestNPCData);
            this.RegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_BUY_ITEM,OnHandleBuyItem);
        }

        protected virtual void OnDisable()
        {
            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_REQUEST_NPC_DATA, OnHandleRequestNPCData);
            this.UnRegistEvent(GameEvent.WebEvent.EVENT_WEB_SEND_BUY_ITEM, OnHandleBuyItem);
        }

        object OnHandleRequestNPCData(object pSender)
        {
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
            ((Dictionary<string, object>)tmpSend["results"]).Add("id", mNPCID);
            ((Dictionary<string, object>)tmpSend["results"]).Add("packages", tmpItemData);
            this.SendEvent(GameEvent.WebEvent.EVENT_WEB_RECEIVE_REQUEST_NPC_DATA, tmpItemData);
            return null;
        }

        object OnHandleBuyItem(object pSender)
        {
            Dictionary<string, object> tmpSend = ServerCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_RECEIVE_Add_USER_STORAGE);

            return null;
        }

    }
}
