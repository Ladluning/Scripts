using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
    public class Server_Game_NPC_Trader : Server_Game_NPC_Base
    {
        public List<Struct_Item_Base> mItemList = new List<Struct_Item_Base>();
        public override void Init()
        {
            mController = gameObject.AddComponent<Server_Game_FSM_NPC_Trader_Controller>();
            mController.Init(this);

            base.Init();

            this.SendEvent(GameEvent.FightingEvent.EVENT_FIGHT_NEW_NPC, this);
        }
    }
}
